using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SeedInventory.Models;
using SeedInventory.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SeedInventory.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class InventoryController : ControllerBase
  {
    private InventoryContext Context { get; }

    public InventoryController(InventoryContext context)
    {
      Context = context;     
    }

    [HttpGet("GetCurrentInventory")]
    public async Task<IActionResult> GetCurrentInventory()
    {
      try
      {
        var result = await Context.Inventories.Include(i => i.InventoryRequests).OrderBy(i => i.Id).ToListAsync();
        var resultAvailable = await Context.Inventories.Include(i => i.InventoryRequests).OrderBy(i => i.Id).ToListAsync();

        return Ok(result);
      }
      catch (Exception ex)
      {
        return BadRequest(new ApiResponse<string> { Status = false, Message = ex.Message });
      }
    }

    [HttpPost("GetInventoryRequest")]
    public async Task<IActionResult> GetInventoryRequest(int id)
    {
      var item = await Context.InventoryRequest.Include(i => i.Inventory).SingleOrDefaultAsync(c => c.Id == id);

      if (item == null)
        return NotFound(new ApiResponse<InventoryRequest>
        { Status = false, Message = "InventoryRequest Not Found.", Item = new InventoryRequest() { Id = id } });

      return Ok(item);
    }

    [HttpGet("GetInventoryRequests")]
    public async Task<IActionResult> GetInventoryRequests()
    {
      var items = await Context.InventoryRequest.Include(i => i.Inventory).ToListAsync(); ;
      return Ok(items);
    }

    [HttpPost("RequestCreateUpdate")]
    public async Task<IActionResult> RequestCreate([FromBody] InventoryRequest inventoryRequest)
    {
     if (!ModelState.IsValid)
      {
        return BadRequest(new ApiResponse<string> { Status = false, ModelState = ModelState });
      }
      if (!inventoryRequest.Locked)
      {
        return BadRequest(new ApiResponse<string> { Status = false, Message = "Request is Process andcannot be modified" });
      }
      int id = inventoryRequest.Id;
      try
      {     

        var item = await Context.InventoryRequest.Include(i => i.Inventory).SingleOrDefaultAsync(c => c.Id == id);

        if (item != null)
        {
          if (item.InventoryId != inventoryRequest.InventoryId
              || item.RequestedKernels != inventoryRequest.RequestedKernels
              || item.Approved != inventoryRequest.Approved)
          {
            item.InventoryId = inventoryRequest.InventoryId;
            item.RequestedKernels = inventoryRequest.RequestedKernels;
            item.Approved = inventoryRequest.Approved;
            item.Locked = inventoryRequest.Locked;
            Context.Entry(item).State = EntityState.Modified;
          }
        }
        else
          Context.Entry(inventoryRequest).State = inventoryRequest.Id == 0 ? EntityState.Added : EntityState.Modified;

        await Context.SaveChangesAsync();

        if (inventoryRequest == null)
          return BadRequest(new ApiResponse<string> { Status = false });

        return Ok(new ApiResponse<InventoryRequest>
        {
          Status = true,
          Item = inventoryRequest,
          Message = string.Format("Request for {0} Kernels of  '{1}' Successfully {2}.", inventoryRequest.RequestedKernels, inventoryRequest.Inventory.Name, id == 0 ? "Created" : "Updated")
        });
      }
      catch (Exception ex)
      {
        return BadRequest(new ApiResponse<string> { Status = false, Message = string.Format("Message :{0}; StackTrace {1}.", ex.Message, ex.StackTrace) });
      }
    }
  }
}

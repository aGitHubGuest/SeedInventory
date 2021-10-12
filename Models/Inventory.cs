using Microsoft.AspNetCore.Mvc.ModelBinding;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace SeedInventory.Models
{
  public class Inventory
  {
    public int Id { get; set; }
    public string Name { get; set; }
    public int Kernels { get; set; }
    public int KernelsUsed { get; set; }
    public DateTime DateCreated { get; set; }
    public DateTime DateUpdated { get; set; }
    public List<InventoryRequest> InventoryRequests { get; set; }
    public Inventory GetCopy(Inventory obj)
    {
      var item = new Inventory()
      {
        Id = obj.Id,

        Name = obj.Name,
        Kernels = obj.Kernels
      };

      return item;
    }
  }

  public class InventoryRequest
  {
    public int Id { get; set; }
    public int InventoryId { get; set; }
    public int RequestedKernels { get; set; }
    public bool Approved { get; set; } = false;
    public bool Locked { get; set; } = false;
    public DateTime DateCreated { get; set; }
    public DateTime DateUpdated { get; set; }
    [ForeignKey("InventoryId")]
    public virtual Inventory Inventory { get; set; }
  }

  public class InventoryData
  {
    public List<Inventory> Inventory { get; set; }
    [JsonProperty("requests")]
    public List<InventoryRequest> InventoryRequests { get; set; } 
  }

  public class ApiResponse<T>
  {
    public bool Status { get; set; }
    public T Item { get; set; }
    public ModelStateDictionary ModelState { get; set; }
    public string Message { get; set; }
  }
}

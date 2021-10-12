using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using SeedInventory.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SeedInventory.Repository
{
  public static class Seeder
  {
    public static void Seedit(string jsonData, IServiceProvider serviceProvider)
    {
      InventoryData idata = JsonConvert.DeserializeObject<InventoryData>(jsonData);

      List<Inventory> invs = idata.Inventory.GroupBy(i => i.Id)
        .Select(i => i.First()).OrderBy(i => i.Id).ToList();

      List<InventoryRequest> invRequests = idata.InventoryRequests
        .GroupBy(i => i.Id)
        .Select(i => i.First())
        .OrderBy(i => i.Id)
        .ToList();

      invRequests.ForEach(ir => ir.Inventory = invs.FirstOrDefault(i => i.Id == ir.InventoryId));

      using (var serviceScope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope())
      {
        var context = serviceScope.ServiceProvider.GetService<InventoryContext>();

        if (!context.Inventories.Any())
        {
          invs.ForEach(ir => ir.Id = 0);
          context.Inventories.AddRange(invs);
        }
        context.SaveChanges();

        var dictInventories = context.Inventories.ToDictionary(x => x.Name, x => x.Id);
        invRequests.ForEach(i => { i.Id = 0; i.InventoryId = dictInventories[i.Inventory.Name]; i.Inventory = null; });

        if (!context.InventoryRequest.Any())
          context.InventoryRequest.AddRange(invRequests);
        context.SaveChanges();
      }
    }
  }
}



using AutoMapper;
using Inventory.Core;
using Inventory.Core.Domain;
using Inventory.Infrastructure.Models;

namespace Inventory.Infrastructure.Automapper;

public class InventoryItemProfile: Profile  
{
    public InventoryItemProfile()  
    {  
        CreateMap<InventoryItem, InventoryItemDto>()
            .ReverseMap();
        
        
    }  
}
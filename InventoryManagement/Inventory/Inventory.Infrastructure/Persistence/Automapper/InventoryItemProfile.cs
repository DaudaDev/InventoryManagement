using AutoMapper;
using Inventory.Core.Domain;
using Inventory.Infrastructure.Persistence.Models;

namespace Inventory.Infrastructure.Persistence.Automapper;

public class InventoryItemProfile: Profile  
{
    public InventoryItemProfile()  
    {  
        CreateMap<InventoryItem, InventoryItemDto>()
            .ReverseMap();
        
        
    }  
}
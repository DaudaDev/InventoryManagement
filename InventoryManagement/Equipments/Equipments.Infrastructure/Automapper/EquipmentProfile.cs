using AutoMapper;
using Equipments.Infrastructure.Models;

namespace Equipments.Infrastructure.Automapper;

public class EquipmentProfile: Profile  
{
    public EquipmentProfile()  
    {  
        CreateMap<EquipmentDto, EquipmentDto>()
            .ReverseMap();
        
        
    }  
}
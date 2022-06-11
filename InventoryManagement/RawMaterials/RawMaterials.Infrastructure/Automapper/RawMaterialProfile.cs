using AutoMapper;
using RawMaterials.Core.Domain.RawMaterials;
using RawMaterials.Infrastructure.Models;

namespace RawMaterials.Infrastructure.Automapper;

public class RawMaterialProfile: Profile  
{
    public RawMaterialProfile()  
    {  
        CreateMap<RawMaterial, RawMaterialDto>()
            .ReverseMap();

    }  
}
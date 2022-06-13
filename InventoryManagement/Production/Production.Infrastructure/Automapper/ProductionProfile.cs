using AutoMapper;
using Production.Core.Domain;
using Production.Infrastructure.Models;

namespace Production.Infrastructure.Automapper;

public class ProductionProfile: Profile  
{
    public ProductionProfile()  
    {  
        CreateMap<ProductionEntity, ProductionDto>()
            .ReverseMap();
        
        
    }  
}
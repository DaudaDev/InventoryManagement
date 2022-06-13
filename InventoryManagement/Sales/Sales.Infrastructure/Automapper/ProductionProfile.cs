using AutoMapper;
using Sales.Core.Domain;
using Sales.Infrastructure.Models;

namespace Sales.Infrastructure.Automapper;

public class ProductionProfile: Profile  
{
    public ProductionProfile()  
    {  
        CreateMap<SalesEntity, SalesDto>()
            .ReverseMap();
        
        
    }  
}
﻿using CSharpFunctionalExtensions;
using Equipments.Core.Domain.Equipment;
using MediatR;

namespace Equipments.Application.Queries;

public abstract class GetAllEquipmentsQuery : IRequest<Result<IEnumerable<Equipment>>> 
{
    
}
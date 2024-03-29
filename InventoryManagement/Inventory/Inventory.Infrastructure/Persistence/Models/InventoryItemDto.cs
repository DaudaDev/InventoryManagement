﻿using Blocks.MongoDb.Models;
using Blocks.Shared.ValueObjects;
using Inventory.Core.Domain;

namespace Inventory.Infrastructure.Persistence.Models;

public class InventoryItemDto : MongoDbBaseDocument
{
    public EntityName Name { get; set; }
    public IList<InventoryItemEntry> CurrentStock { get; set; } =new List<InventoryItemEntry>();
}
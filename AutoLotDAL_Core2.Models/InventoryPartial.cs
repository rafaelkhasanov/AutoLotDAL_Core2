using AutoLotDAL_Core2.Models.MetaData;
using Microsoft.AspNetCore.Mvc;

namespace AutoLotDAL_Core2.Models
{
    [ModelMetadataType(typeof(InventoryMetaData))]
    public partial class Inventory
    {
        public override string ToString() => $"{PetName ?? "NoName"} is a {Color} {Make} with ID {Id}";
    }
}

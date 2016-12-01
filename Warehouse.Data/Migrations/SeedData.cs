using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Warehouse.Data.Models;

namespace Warehouse.Data.Migrations
{
    public static class SeedData
    {
        public static List<StockItem> StockItems()
        {
            return new List<StockItem>
            {
                new StockItem() {Id = "StockItems/1", Code = "AB-23-476", SupplierCode="PK-123", SupplierDescription="Pumpkin", Weight=2},
                new StockItem() {Id = "StockItems/2", Code = "EF-45-456", SupplierCode="B-3", SupplierDescription="Banana Trolley", Weight=.2m},
                new StockItem() {Id = "StockItems/3", Code = "RT-00-990", SupplierCode="0H-NMU", SupplierDescription="USB-C Univ. Power", Weight=4},
                new StockItem() {Id = "StockItems/4", Code = "HM-11-345", SupplierCode="457633123", SupplierDescription="Editor:MS-Press|Title:Learn ServiceFabric|Author:Alessandro Melchiori", Weight=1},
                new StockItem() {Id = "StockItems/5", Code = "ZZ-45-000", SupplierCode="LH740", SupplierDescription="LH740 Airplane Model", Weight=.5m}
            };
        }
    }
}

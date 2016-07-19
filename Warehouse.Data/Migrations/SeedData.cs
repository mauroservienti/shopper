using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Warehouse.Data.Models;

namespace Warehouse.Data.Migrations
{
    static class SeedData
    {
        public static StockItem[] StockItems()
        {
            return new StockItem[]
            {
                new StockItem() {Id = 1, Code = "AB-23-476", SupplierCode="PK-123", SupplierDescription="Pumpkin"},
                new StockItem() {Id = 2, Code = "EF-45-456", SupplierCode="B-3", SupplierDescription="Banana Trolley"},
                new StockItem() {Id = 3, Code = "RT-00-990", SupplierCode="0H-NMU", SupplierDescription="USB-C Univ. Power"},
                new StockItem() {Id = 4, Code = "HM-11-345", SupplierCode="457633123", SupplierDescription="Editor:MS-Press|Title:Learn ServiceFabric|Author:Alessandro Melchiori"},
                new StockItem() {Id = 5, Code = "ZZ-45-000", SupplierCode="LH740", SupplierDescription="LH740 Airplane Model"}
            };
        }
    }
}

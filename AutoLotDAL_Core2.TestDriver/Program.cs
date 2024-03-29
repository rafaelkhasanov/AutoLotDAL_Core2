﻿using System;
using AutoLotDAL_Core2.DataInitialization;
using AutoLotDAL_Core2.EF;
using AutoLotDAL_Core2.Models;
using AutoLotDAL_Core2.Repos;

namespace AutoLotDAL_Core2.TestDriver
{
    class Program
    {
        static void Main()
        {
            Console.WriteLine("FUN with ADO.NET EF Core 2\n");
            using (var context = new AutoLotContext())
            {
                MyDataInitializer.RecreateDatabase(context);
                MyDataInitializer.InitializeData(context);
                foreach (Inventory c in context.Cars) Console.WriteLine(c);
            }

            Console.WriteLine("Using a Repository\n");
            using (var repo = new InventoryRepo())
            {
                foreach (Inventory c in repo.GetAll())
                {
                    Console.WriteLine(c);
                }
            }

            Console.ReadLine();
        }
    }
}

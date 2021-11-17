using System;
using System.Collections.Generic;
using FamiliesWebAPI.DataGenerator;
using FamiliesWebAPI.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace FamiliesWebAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Hello");
            var families = new DataGenerator.DataGenerator().GenerateFamilies(500);
            foreach (var family in families)
            {
                Console.WriteLine(family.Id);
            }
            DbSeeder.Seed(families);
            CreateHostBuilder(args).Build().Run();
            
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); });
    }
}
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FamiliesWebAPI.Data.Impl;
using FamiliesWebAPI.DataGenerator;
using FamiliesWebAPI.Models;
using FamiliesWebAPI.Persistence;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace FamiliesWebAPI
{
    public class Program
    {
        public async static Task Main(string[] args)
        {
            Console.WriteLine("Hello");
            var families = new DataGenerator.DataGenerator().GenerateFamilies(500);
            DbSeeder.Seed(families);

            var familiesWebService = new FamiliesWebService();
            var allFamilies = await familiesWebService.GetFamiliesAsync();
          /*  foreach (var family in allFamilies)
            {
                Console.WriteLine("FamilyId");
                Console.WriteLine(family.Id);
                foreach (var adult in family.Adults)
                {
                    Console.WriteLine("AdultIds");
                    Console.WriteLine(adult.Id);
                }
            }*/
            
            CreateHostBuilder(args).Build().Run();
            
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); });
    }
}
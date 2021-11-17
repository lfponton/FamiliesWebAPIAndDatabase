using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FamiliesWebAPI.Models;
using FamiliesWebAPI.Persistence;
using Microsoft.EntityFrameworkCore;

namespace FamiliesWebAPI.DataGenerator
{
    public class DbSeeder
    {
        private class CI
        {
            public Child Child { get; set; }
            public IList<Interest> Interests { get; set; }
        }

        public static async Task Seed(IList<Family> families)
        {
            await using var familiesContext = new FamiliesContext();
            // CleanInterestObjects(families);
            var testUser = await familiesContext.Users.FirstOrDefaultAsync(u => u.Id == 1);
            // If it hasn't, then it will add the data to the database
            if (testUser == null)
            {
                
                var user =
                    new User
                    {
                        Id = 1,
                        Username = "Luis",
                        Password = "1234"

                    };
                familiesContext.Users.Add(user);

                familiesContext.Entry(user).State = EntityState.Added;
                familiesContext.SaveChanges();


                Console.WriteLine("Inserting Interests...");
                AddInterests(families);
                Console.WriteLine("Done!");

                Console.WriteLine("Caching child interests..");
                List<CI> childInterests = CollectAllChildInterests(families);
                Console.WriteLine("Done!");

                Console.WriteLine("Inserting families..");
                AddFamilies(families);
                Console.WriteLine("Done!");

                Console.WriteLine("Setting child <-> interest relations..");
                SetupChildInterestRelations(childInterests);
                Console.WriteLine("Done!");
            }
        }

        private static void SetupChildInterestRelations(List<CI> childInterests)
        {
            foreach (CI ci in childInterests)
            {
                using FamiliesContext ctx = new FamiliesContext();
                Child toUpdate = ctx.Set<Child>().First(c => c.Id == ci.Child.Id);
                List<Interest> interests = new();
                foreach (Interest interest in ci.Interests)
                {
                    interests.Add(ctx.Set<Interest>().First(i => i.Type.Equals(interest.Type)));
                }

                toUpdate.Interests = interests;
                ctx.Update(toUpdate);
                ctx.SaveChanges();
            }
        }

        private static void AddFamilies(IList<Family> families)
        {
            foreach (Family family in families)
            {
                using (FamiliesContext fctxt = new FamiliesContext())
                {
                    fctxt.Families.Add(family);

                    fctxt.Entry(family).State = EntityState.Added;
                    // fctxt.Entry(family).State = EntityState.Detached;
                    fctxt.SaveChanges();
                }
            }
        }

        private static List<CI> CollectAllChildInterests(IList<Family> families)
        {
            List<CI> childInterests = new();

            foreach (Family family in families)
            {
                foreach (Child child in family.Children)
                {
                    childInterests.Add(new()
                    {
                        Child = child,
                        Interests = child.Interests
                    });

                    child.Interests = null;
                }
            }

            return childInterests;
        }


        private static void AddInterests(IEnumerable<Family> families)
        {
            foreach (Family family in families)
            {
                List<Interest> interests = family.Children.SelectMany(x => x.Interests).ToList();

                foreach (Interest entity in interests)
                {
                    using (FamiliesContext familyContext = new FamiliesContext())
                    {
                        try
                        {
                            if (!familyContext.Set<Interest>().Any(e => e.Type.Equals(entity.Type)))
                            {
                                familyContext.Set<Interest>().Add(entity);
                                familyContext.SaveChanges();
                            }
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e);
                            throw;
                        }

                    }
                }
            }
        }
    }
}
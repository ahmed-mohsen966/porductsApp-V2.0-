using porductsApp.Data;
using porductsApp.Models;
using System;

namespace porductsApp.Seeds
{
    public static class DefaultCatalogs
    {
        public static Task SeedCatalogs(ApplicationDbContext context)
        {
            try
            {
                var catalogs = new[]
                {
                    new Catalog{ Id = new Guid() , Name = "shoes"},
                    new Catalog{ Id = new Guid() ,Name ="shirts"}
                };
                var isExistedCatalogs = context.Catalogs.Any();

                if (!isExistedCatalogs)
                {
                    context.Catalogs.AddRange(catalogs);
                    context.SaveChanges();
                }

                return Task.CompletedTask;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}

using System.Security.Cryptography;
using System.Text;
using whostpos.Entitys.Entites;

namespace whostpos.Domain.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<whostpos.Domain.Context.Dbesm>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        #region Test
        public static string Sha256Encrypt(string phrase)
        {
            var encoder = new UTF8Encoding();
            var sha256Hasher = new SHA256Managed();
            byte[] hashedDataBytes = sha256Hasher.ComputeHash(encoder.GetBytes(phrase));
            return ByteArrayToString(hashedDataBytes);
        }

        #region Private Methods
        private static string ByteArrayToString(byte[] inputArray)
        {
            var output = new StringBuilder("");
            for (int i = 0; i < inputArray.Length; i++)
            {
                output.Append(inputArray[i].ToString("X2"));
            }
            return output.ToString();
        }
        #endregion
        #endregion

        protected override void Seed(whostpos.Domain.Context.Dbesm context)
        {

            if (context.UserAccounts.FirstOrDefault(x => x.Username.Equals("Admin")) == null)
            {
                context.UserAccounts.AddOrUpdate(new UserAccount
                {
                    Username = "Admin",
                    Password = Sha256Encrypt("123456")
                });
            }

            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
        }
    }
}

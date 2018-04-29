using System;
using System.Linq;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Logging;
using SamuraiApp.Data;
using SamuraiApp.Domain;

namespace SomeUI
{
    class Program
    {
        static void Main(string[] args)
        {
            //InsertSamurai();
        }

        private static void InsertSamurai()
        {
            var samurai = new Samurai { Name = "Ihsan" };
            using (var context = new SamuraiContext())
            {
                context.GetService<ILoggerFactory>().AddProvider(new MyLoggerProvider());
                context.Samurais.Add(samurai);
                context.SaveChanges();
            }
        }
    }
}

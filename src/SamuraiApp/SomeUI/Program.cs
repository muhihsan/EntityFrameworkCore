using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Logging;
using SamuraiApp.Data;
using SamuraiApp.Domain;

namespace SomeUI
{
    class Program
    {
        private static SamuraiContext _context = new SamuraiContext();

        static void Main(string[] args)
        {
            _context.GetService<ILoggerFactory>().AddProvider(new MyLoggerProvider());
            //InsertSamurai();
            //InsertMultipleSamurais();
            //SimpleSamuraiQuery();
            //MoreQueries();
            //RetrieveAndUpdateSamurai();
            //RetrieveAndUpdateMultipleSamurais();
            //MultipleOperations();
            //QueryAndUpdateSamuraiDisconnected();
            //QuerAndUpdateDisconnectedBattle();
            //RawQuery();
            //RawSqlQuery();
            //QueryWithNoSql();
        }

        /// <summary>
        /// EF core smart enough to get all samurais and perform reverse after that
        /// </summary>
        private static void QueryWithNoSql()
        {
            var samurais = _context.Samurais
                .Select(samurai => new { newName = ReverseString(samurai.Name) })
                .ToList();
            samurais.ForEach(s => Console.WriteLine(s.newName));
        }

        private static string ReverseString(string value)
        {
            var stringChar = value.AsEnumerable();
            return string.Concat(stringChar.Reverse());
        }

        /// <summary>
        /// Stored Procedure aren't composable
        /// </summary>
        private static void RawSqlQuery()
        {
            var namePart = "San";
            var samurais = _context.Samurais
                .FromSql("EXEC FilterSamuraiByNamePart {0}", namePart)
                .OrderByDescending(s => s.Name)
                .ToList();

            samurais.ForEach(samurai => Console.WriteLine(samurai.Name));
        }

        /// <summary>
        /// This OrderBy will be composed into SQL query
        /// </summary>
        private static void RawQuery()
        {
            var samurais = _context.Samurais.FromSql("Select * from Samurais")
                .OrderBy(samurai => samurai.Name)
                .ToList();
            samurais.ForEach(samurai => Console.WriteLine(samurai.Name));
            Console.WriteLine();
        }

        private static void QuerAndUpdateDisconnectedBattle()
        {
            var battle = _context.Battles.FirstOrDefault();
            battle.EndDate = new DateTime(1754, 12, 31);
            using (var contextNewAppInstance = new SamuraiContext())
            {
                contextNewAppInstance.Battles.Update(battle);
                contextNewAppInstance.SaveChanges();
            }
        }

        private static void QueryAndUpdateSamuraiDisconnected()
        {
            var samurai = _context.Samurais.FirstOrDefault(s => s.Name == "Nina");
            samurai.Name += "San";
            using (var contextNewAppInstance = new SamuraiContext())
            {
                contextNewAppInstance.Samurais.Update(samurai);
                contextNewAppInstance.SaveChanges();
            }
        }

        private static void MultipleOperations()
        {
            var samurai = _context.Samurais.FirstOrDefault();
            samurai.Name += "San";
            _context.Samurais.Add(new Samurai { Name = "Qurrota" });
            _context.SaveChanges();
        }

        private static void RetrieveAndUpdateMultipleSamurais()
        {
            var samurais = _context.Samurais.ToList();
            samurais.ForEach(samurai => samurai.Name += "San");
            _context.SaveChanges();
        }

        private static void RetrieveAndUpdateSamurai()
        {
            var samurai = _context.Samurais.FirstOrDefault();
            samurai.Name += "San";
            _context.SaveChanges();
        }

        private static void MoreQueries()
        {
            var samurais = _context.Samurais.Where(s => s.Name == "Ihsan").ToList();
        }

        private static void SimpleSamuraiQuery()
        {
            var samurais = _context.Samurais.ToList();
        }

        private static void InsertMultipleSamurais()
        {
            var samuraiHerfi = new Samurai { Name = "Herfi" };
            var samuraiNina = new Samurai { Name = "Nina" };
            _context.Samurais.AddRange(samuraiHerfi, samuraiNina);
            _context.SaveChanges();
        }

        private static void InsertSamurai()
        {
            var samurai = new Samurai { Name = "Ihsan" };
            _context.GetService<ILoggerFactory>().AddProvider(new MyLoggerProvider());
            _context.Samurais.Add(samurai);
            _context.SaveChanges();
        }
    }
}

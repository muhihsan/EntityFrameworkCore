﻿using System.Linq;
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
            MoreQueries();
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

﻿using SimpleApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleApp.Repositories
{
    public class ProductRepository
    {
        private readonly SimpleAppContext _context;

        public ProductRepository(SimpleAppContext context)
        {
            _context = context;
            _context.Database.EnsureCreated();

            if (_context.Products.Count() == 0)
            {
                _context.Products.AddRange(
                    new Product
                    {
                        IsDiscontinued = true,
                        Name = "Learning ASP.NET Core",
                        Description = "A best-selling book covering the fundamentals of ASP.NET Core"
                    },
                    new Product
                    {
                        Name = "Learning EF Core",
                        Description = "A best-selling book covering the fundamentals of Entity Framework Core"
                    });
                _context.SaveChanges();
            }
        }

        public List<Product> GetDiscontinuedProducts()
        {
            var products = (from p in _context.Products
                            where p.IsDiscontinued
                            select p).ToList();

            return products;
        }

        public List<Product> GetProducts()
        {
            return _context.Products.ToList();
        }

        public bool TryGetProduct(int id, out Product product)
        {
            product = _context.Products.Find(id);

            return (product != null);
        }

        public async Task<int> AddProductAsync(Product product)
        {
            int rowsAffected = 0;

            _context.Products.Add(product);
            rowsAffected = await _context.SaveChangesAsync();

            return rowsAffected;
        }
    }
}

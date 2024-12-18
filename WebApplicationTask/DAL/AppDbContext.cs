﻿using Microsoft.EntityFrameworkCore;
using WebApplicationTask.Models;

namespace WebApplicationTask.DAL
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
    }
}
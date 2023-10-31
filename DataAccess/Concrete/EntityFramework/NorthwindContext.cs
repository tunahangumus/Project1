﻿using Core.Entity.Concrete;
using Entity.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EntityFramework
{
	public class NorthwindContext : DbContext
	{
		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			optionsBuilder.UseSqlServer("server=DESKTOP-5FSH2VU\\SQLEXPRESS;database=Northwind; integrated security= true;TrustServerCertificate=True;");
		}

		public DbSet<Product> Products { get; set; }
		public DbSet<Category> Categories { get; set; }
		public DbSet<OperationClaim> OperationClaims { get; set; }
		public DbSet<User> Users { get; set; }
		public DbSet<UserOperationClaim> UserOperationClaims { get; set; }

	}
}

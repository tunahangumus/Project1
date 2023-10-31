using Business.Concrete;
using DataAccess.Concrete.EntityFramework;
using DataAccess.Concrete.InMemory;
using System;

namespace ConsoleUI
{
	class Program
	{
		static void Main(string[] args)
		{
			ProductTest();
			//CategoryTest();
		}

		private static void CategoryTest()
		{
			CategoryManager cm = new CategoryManager(new EfCategoryDal());
			foreach (var category in cm.GetAll().Data)
			{
				Console.WriteLine(category.CategoryName);
			}
		}

		private static void ProductTest()
		{
			ProductManager productmanager = new ProductManager(new EfProductDal(),new CategoryManager(new EfCategoryDal()));
			var result = productmanager.GetAll();
			if (result.Success == true)
			{
				foreach (var product in result.Data)
				{
					Console.WriteLine(product.ProductName);
				}
			}
			else
			{
				Console.WriteLine("asdsad");
			}
		}
	}
}
using Business.Abstract;
using Business.BusinessAspect.Autofac;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Validation;
using Core.CrossCuttingConcerns.Validation;
using Core.Utilities.Business;
using Core.Utilities.Results;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using DataAccess.Concrete.InMemory;
using Entity.Concrete;
using Entity.DTOs;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
	public class ProductManager : IProductService
	{
		IProductDal _productDal;
		ICategoryService _categoryService;

		public ProductManager(IProductDal productDal, ICategoryService categoryService)
		{
			_productDal = productDal;
			_categoryService = categoryService;
		}


		[SecuredOperation("admin,product.add")]
		[ValidationAspect(typeof(ProductValidator))]
		public IResult Add(Product product)
		{
			IResult result = BusinessRules.Run(
				CheckIfProductCountOfCategoryCorrect(product.CategoryID),
				CheckIfProductNameExists(product.ProductName),
				CheckIfCategoryLimitExceeded()
				);
			if (result != null)
			{
				return result;
			}
			_productDal.Add(product);
			return new SuccessResult();
		}


			public IDataResult<List<Product>> GetAll()
		{
			if(DateTime.Now.Hour == 1)
			{
				return new ErrorDataResult<List<Product>>("Eklenemedi");
			}
			return new SuccessDataResult<List<Product>>(_productDal.GetAll(), "Eklendi");
			
		}

		public IDataResult<List<Product>> GetAllByCategoryId(int id)
		{
			return new DataResult<List<Product>>(_productDal.GetAll(p=>p.CategoryID == id),true);   
		}

		public IDataResult<Product> GetById(int id)
		{
			return new DataResult<Product>(_productDal.Get(p => p.ProductID == id), true);
		}

		public IDataResult<List<Product>> GetByUnitPrice(decimal min, decimal max)
		{
			return new DataResult<List<Product>>(_productDal.GetAll(p=>p.UnitPrice>=min && p.UnitPrice<=max),true,"Ürün eklendi");
		}

		public IDataResult<List<ProductDetailDto>> GetProductDetails()
		{
			return new DataResult<List<ProductDetailDto>>(_productDal.GetProductDetails(),true);
		}

		[ValidationAspect(typeof(ProductValidator))]
		public IResult Update(Product product)
		{
			throw new NotImplementedException();
		}

		private IResult CheckIfProductCountOfCategoryCorrect(int categoryid)
		{
			var result = _productDal.GetAll(p => p.CategoryID == categoryid).Count;
			if (result >= 10)
			{
				return new ErrorResult(Messages.ProductCountOfCategoryError);
			}
			return new SuccessResult();
		}
		private IResult CheckIfProductNameExists(string name)
		{
			var result = _productDal.GetAll(p=>p.ProductName == name).Any();
			if (result)
			{
				return new ErrorResult(Messages.ProductNameAlreadyExists);
			}
			return new SuccessResult();
		}

		private IResult CheckIfCategoryLimitExceeded()
		{
			var result = _categoryService.GetAll();
			if(result.Data.Count > 15)
			{
				return new ErrorResult(Messages.CategoryLimitExceeded);
			}
			return new SuccessResult();
		}
	}
}

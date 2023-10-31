using Autofac;
using Autofac.Extras.DynamicProxy;
using Business.Abstract;
using Business.Concrete;
using Castle.DynamicProxy;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Core.Utilities.Interceptors.Class1;

namespace Business.DependencyResolvers.Autofac
{
	public class AutofacBusinessModule : Module
	{
		protected override void Load(ContainerBuilder builder)
		{
			//builder.RegisterType<ProductManager>().As<IProductService>().SingleInstance();
			//builder.RegisterType<EfProductDal>().As<IProductDal>().SingleInstance();
			builder.RegisterType<HttpContextAccessor>().As<IHttpContextAccessor>().SingleInstance();
			var assembly = System.Reflection.Assembly.GetExecutingAssembly();

			builder.RegisterAssemblyTypes(assembly).AsImplementedInterfaces()
				.EnableInterfaceInterceptors(new ProxyGenerationOptions()
				{
					Selector = new AspectInterceptorSelector()
				}).SingleInstance();
		}
	}
}

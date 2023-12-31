﻿using Business.Constants;
using Castle.DynamicProxy;
using Core.Utilities.IoC;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System;
using Core.Extentions;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Core.Utilities.Interceptors.Class1;

namespace Business.BusinessAspect.Autofac
{
	public class SecuredOperation : MethodInterception
	{
		private string[] _roles;
		private IHttpContextAccessor _httpContextAccessor;

		public SecuredOperation(string roles)
		{
			_roles = roles.Split(',');
			_httpContextAccessor = ServiceTool.ServiceProvider.GetService<IHttpContextAccessor>();

		}

		protected override void OnBefore(IInvocation invocation)
		{
			var roleClaims = _httpContextAccessor.HttpContext.User.ClaimRoles();
			foreach (var role in _roles)
			{
				if (roleClaims.Contains(role))
				{
					return;
				}
			}
			throw new Exception(Messages.AuthorizationDenied);
		}
	}
}

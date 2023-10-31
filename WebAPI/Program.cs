using Autofac;
using Autofac.Core;
using Autofac.Extensions.DependencyInjection;
using Business.Abstract;
using Business.BusinessAspect.Autofac;
using Business.Concrete;
using Core.Extentions;
using Business.DependencyResolvers.Autofac;
using Core.Utilities.IoC;
using Core.Utilities.Security.Encryption;
using Core.Utilities.Security.JWT;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.IdentityModel.Tokens;
using Core.DependencyResolvers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

//builder.Services.AddSingleton<IProductService,ProductManager>();
//builder.Services.AddSingleton<IProductDal,EfProductDal>();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Host.UseServiceProviderFactory(services => new AutofacServiceProviderFactory()).ConfigureContainer<ContainerBuilder>(builder => { builder.RegisterModule(new AutofacBusinessModule()); });




var tokenOptions = builder.Configuration.GetSection("TokenOptions").Get<TokenOptions>();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
				.AddJwtBearer(options =>
				{
					options.TokenValidationParameters = new TokenValidationParameters
					{
						ValidateIssuer = true,
						ValidateAudience = true,
						ValidateLifetime = true,
						ValidIssuer = tokenOptions.Issuer,
						ValidAudience = tokenOptions.Audience,
						ValidateIssuerSigningKey = true,
						IssuerSigningKey = SecurityKeyHelper.CreateSecurityKey(tokenOptions.SecurityKey)
					};
				});

builder.Services.AddHttpContextAccessor();
builder.Services.AddTransient<IHttpContextAccessor, HttpContextAccessor>();

builder.Services.AddDependencyResolvers(new ICoreModule[]
{
	new CoreModule()
});

builder.Services.AddScoped<IProductDal,EfProductDal>();
builder.Services.AddScoped<ICategoryService,CategoryManager>();
builder.Services.AddScoped<ICategoryDal,EfCategoryDal>();
builder.Services.AddScoped<IProductService,ProductManager>();
builder.Services.AddScoped<IUserService,UserManager>();
builder.Services.AddScoped<IUserDal,EfUserDal>();
builder.Services.AddScoped<IAuthService,AuthManager>();
builder.Services.AddScoped<ITokenHelper,JwtHelper>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

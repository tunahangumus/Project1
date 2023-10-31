using Core.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Core.DataAccess.EntityFramework
{
	public class EfEntityRepositoryBase<TEntity, TContext>:IGenericDal<TEntity>
		where TEntity : class, IEntity, new() 
		where TContext : DbContext,new()
	{
		public void Add(TEntity entity)
		{
			using (TContext context = new TContext())
			{
				var addedEntity = context.Entry(entity);
				addedEntity.State = EntityState.Added;
				context.SaveChanges();
			}
		}

		public void Delete(TEntity entity)
		{
			using (TContext context = new TContext())
			{
				var _entity = context.Entry(entity);
				_entity.State = EntityState.Deleted;
				context.SaveChanges();
			}
		}

		public TEntity Get(Expression<Func<TEntity, bool>> filter)
		{
			using (TContext context = new TContext())
			{
				return context.Set<TEntity>().SingleOrDefault(filter);
			}
		}

		public List<TEntity> GetAll(Expression<Func<TEntity, bool>> filter = null)
		{
			using (TContext context = new TContext())
			{
				return filter == null ?
					context.Set<TEntity>().ToList() :
					context.Set<TEntity>().Where(filter).ToList();
			}
		}

		public List<TEntity> GetAllByCategory(int categoryId)
		{
			throw new NotImplementedException();
		}

		public void Update(TEntity entity)
		{
			using (TContext context = new TContext())
			{
				var _entity = context.Entry(entity);
				_entity.State = EntityState.Modified;
				context.SaveChanges();
			}
		}
	}
}

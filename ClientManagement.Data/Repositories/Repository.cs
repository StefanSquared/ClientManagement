using System;
using System.Linq;
using ClientManagement.Models;

namespace ClientManagement.Data.Repositories
{
	public class Repository<T> : IRepository<T> where T : class, IEntity, new()
	{
		private readonly ClientDbContext _context;
		public Repository(ClientDbContext context)
		{
			_context = context;
		}

		public IQueryable<T> Get()
		{
			return _context.Set<T>();
		}

		public T Get(int id)
		{
			return Get().FirstOrDefault(x => x.Id == id);
		}

		private T SaveNew(T entity)
		{
			_context.Set<T>().Add(entity);
			_context.SaveChanges();
			return entity;
		}

		public T Save(T entity)
		{
			if (entity != null && entity.Id == 0)
			{
				return SaveNew(entity);
			}

			var databaseEntity = Get(entity.Id);
			_context.Entry(databaseEntity).CurrentValues.SetValues(entity);
			return entity;
		}

		public void Delete(int id)
		{
			var databaseEntity = Get(id);
			if (databaseEntity == null)
			{
				throw new InvalidOperationException("Database Error: That client doesn't exist in the database");
			}

			_context.Set<T>().Remove(databaseEntity);
			_context.SaveChanges();
		}
	}
}
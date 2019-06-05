using System.Linq;
using ClientManagement.Models;

namespace ClientManagement.Data.Repositories
{
	public interface IRepository<T> where T : class, IEntity, new()
	{
		IQueryable<T> Get();

		T Get(int Id);

		T Save(T entity);

		void Delete(int Id);

	}
}
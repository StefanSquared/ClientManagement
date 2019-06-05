using System.Data.Entity;
using ClientManagement.Models;

namespace ClientManagement.Data
{
	public class ClientDbContext:DbContext
	{
		public DbSet<Client> Clients { get; set; }
	}
}
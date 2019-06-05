using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientManagement.Models
{
	public class ClientDto
	{
		public ClientDto()
		{
		}

		public ClientDto(Client client)
		{
			Id = client.Id;
			Name = client.Name;
			BirthDate = client.BirthDate;
		}

		public int Id { get; set; }
		public string Name { get; set; }
		public DateTime BirthDate { get; set; }
	}
}

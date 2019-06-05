using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;

namespace ClientManagement.Models
{
	[Table("Client")]
	public class Client : ModelBase, IDataErrorInfo
	{
		[Required, StringLength(50)]
		public string Name { get; set; }

		[Required, Column(TypeName = "datetime2"),
		 DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd.MM.yyyy}")]
		public DateTime BirthDate { get; set; }

		public string Addresses { get; set; }


		public string this[string propertyName]
		{
			get
			{
				switch (propertyName)
				{
					case "Name":
						if (string.IsNullOrWhiteSpace(Name))
							return "Name is a required field.";
						break;
					case "BirthDate":
						if (BirthDate == null || BirthDate > DateTime.Now ||
						    BirthDate < DateTime.Now.Subtract(TimeSpan.FromDays(130 * 365)))
							return "Invalid date";
						break;

				}
				return null;
			}
		}

		public string Error
		{
			get { return null; }
		}
	}
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DogGo.Models
{
    public class Owner
    {
		public int Id { get; set; }
		public string Name { get; set; }
		public string Email { get; set; }

		public string address { get; set; }
		public 
		public Neighborhood neighborhood { get; set; }

		Id INTEGER NOT NULL PRIMARY KEY IDENTITY,
	Email VARCHAR(255) NOT NULL,
	[Name] VARCHAR(55) NOT NULL,
	[Address] VARCHAR(255) NOT NULL,
	NeighborhoodId INTEGER,
	Phone VARCHAR(55) NOT NULL,

	}
}

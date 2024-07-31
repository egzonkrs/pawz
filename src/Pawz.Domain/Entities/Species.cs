using System;

namespace Pawz.Domain.Entities
{
	public class Species
	{

		public int ID { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public DateTime CreatedAt { get; set; }

	}
}

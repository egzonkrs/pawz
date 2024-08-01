using System;
using System.Collections.Generic;

namespace Pawz.Domain.Entities;

	public class Species
	{

		public int Id { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public DateTime CreatedAt { get; set; }
        public ICollection<Breeds> Breeds { get; set; }
        public ICollection<Pets> Pets { get; set; }

    }

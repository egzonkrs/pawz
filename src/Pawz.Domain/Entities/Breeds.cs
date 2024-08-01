using System.Collections.Generic;

namespace Pawz.Domain.Entities;

    public class Breeds
    {

        public int Id { get; set; }
        public Species Species { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public ICollection<Pets> Pets { get; set; }

    }

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pawz.Application.QueryFilters;

public class PetQueryParameters
{
    /// <summary>
    /// Represents a term or keyword used to search for specific pets based on their name, breed, or other related fields.
    /// </summary>
    public string SearchTerm { get; set; }
}

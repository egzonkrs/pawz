using System.ComponentModel.DataAnnotations;

namespace Pawz.Web.Models;

public class AdoptionRequestCreateModel
{
    /// <summary>
    /// Gets or sets the Country of the user that is requesting to adopt.
    /// </summary>
    public string Country { get; set; }

    /// <summary>
    /// Gets or sets the City of the user that is requesting to adopt.
    /// </summary>
    public string City { get; set; }

    /// <summary>
    /// Gets or sets the Address of the user that is requesting to adopt.
    /// </summary>
    public string Address { get; set; }

}

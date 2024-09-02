namespace Pawz.Application.Models;

public class AdoptionRequestCreateRequest
{
    /// <summary>
    /// The Country of the user that is trying to send an Adoption Request.
    /// </summary>
    public string Country { get; set; }

    /// <summary>
    /// The City of the user that is trying to send an Adoption Request.
    /// </summary>
    public string City { get; set; }

    /// <summary>
    /// The Address of the user that is trying to send an Adoption Request.
    /// </summary>
    public string Address { get; set; }
}

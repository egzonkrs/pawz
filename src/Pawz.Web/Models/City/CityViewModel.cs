namespace Pawz.Web.Models.City;

public class CityViewModel
{
    /// <summary>
    /// The Id of the country.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// The name of the country.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// The Id of the country to which this city belongs
    /// </summary>
    public int CountryId { get; set; }
}

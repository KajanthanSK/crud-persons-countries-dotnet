namespace Entities
{
    /// <summary>
    /// Domain model class representing a country. It has two properties: CountryId and Country
    /// </summary>
    public class Country
    {
        public Guid CountryId { get; set; }
        public string? CountryName { get; set; }

    }
}

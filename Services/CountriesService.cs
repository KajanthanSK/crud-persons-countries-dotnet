using ServiceContracts;
using ServiceContracts.DTO;
using Entities;

namespace Services
{
    public class CountriesService : ICountriesService
    {
        private readonly List<Country> countries;

        public CountriesService()
        {
            countries = new List<Country>();
        }
        public CountryResponse AddCountry(CountryAddRequest? countryAddRequest)
        {
            // Validate the input
            if (countryAddRequest == null)
            {
                throw new ArgumentNullException(nameof(countryAddRequest), "CountryAddRequest cannot be null.");
            }

            // Validate CountryName
            if (countryAddRequest.CountryName == null)
            {
                throw new ArgumentException("CountryName cannot be null.", nameof(countryAddRequest.CountryName));
            }

            // Check for duplicate country name (case-insensitive)
            if (countries.Any(c => c.CountryName.Equals(countryAddRequest.CountryName, StringComparison.OrdinalIgnoreCase)))
            {
                throw new ArgumentException($"Country with name '{countryAddRequest.CountryName}' already exists.", nameof(countryAddRequest.CountryName));
            }

            // Convert CountryAddRequest to Country
            Country country =countryAddRequest.ToCountry();

            // Add the country to the list
            countries.Add(country);

            return country.ToCountryResponse();
        }

        public List<CountryResponse> GetAllCountries()
        {            // Convert the list of Country to a list of CountryResponse
            return countries.Select(c => c.ToCountryResponse()).ToList();
        }

        public CountryResponse? GetCountryByCountryID(Guid? countryID)
        {
            if (countryID == null)
            {
                return null;
            }

            Country? country = countries.FirstOrDefault(c => c.CountryId == countryID.Value);

            if(country == null)
            {
                return null;
            }
            return country?.ToCountryResponse();
        }
    }
}

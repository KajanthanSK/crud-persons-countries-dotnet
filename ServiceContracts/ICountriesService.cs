using ServiceContracts.DTO;

namespace ServiceContracts
{
    /// <summary>
    /// Represents the service contract for managing countries. It will have methods for adding a country, getting all countries, getting a country by id, updating a country and deleting a country.
    /// </summary>
    public interface ICountriesService
    {
        /// <summary>
        /// Adds a new country to the system. It takes a CountryAddRequest object as input and returns a CountryResponse object representing the added country. If the input is null or invalid, it may return null or throw an exception based on the implementation.
        /// </summary>
        /// <param name="countryAddRequest">The request object containing the details of the country to be added.</param>
        /// <returns>A CountryResponse object representing the added country.</returns>
        CountryResponse AddCountry(CountryAddRequest? countryAddRequest);

        /// <summary>
        /// Retrieves a list of countries available in the system.
        /// </summary>
        /// <returns>A list of <see cref="CountryResponse"/> objects representing the available countries. The list is empty if
        /// no countries are found.</returns>
        List<CountryResponse> GetAllCountries();

        /// <summary>
        /// Retrieves a country based on the provided country ID. It takes a Guid representing the country ID as input and returns a CountryResponse object representing the country with the specified ID. If no country is found with the given ID, it may return null or throw an exception based on the implementation.
        /// </summary>
        /// <param name="countryID">The unique identifier of the country to retrieve.</param>
        /// <returns>A CountryResponse object representing the country with the specified ID, or null if not found.</returns>
        CountryResponse? GetCountryByCountryID(Guid? countryId);
    }
}

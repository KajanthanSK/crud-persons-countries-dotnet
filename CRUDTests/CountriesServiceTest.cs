using Entities;
using ServiceContracts.DTO;
using ServiceContracts;
using Services;

namespace CRUDTests
{
    public class CountriesServiceTest
    {
        private readonly ICountriesService _countriesService;

        public CountriesServiceTest()
        {
            _countriesService = new CountriesService();
        }

        #region AddCountry Tests[Fact]
        //When the countryAddRequest is null, then the AddCountry method should throw an ArgumentNullException
        [Fact]
        public void AddCountry_NullCountry()
        {
            //Arrange
            CountryAddRequest? countryAddRequest = null;

            //Assert and Act
            Assert.Throws<ArgumentNullException>(() => _countriesService.AddCountry(countryAddRequest));

            //Act
            //_countriesService.AddCountry(countryAddRequest);
        }

        //When the CountryName is null, then the AddCountry method should throw an ArgumentException
        [Fact]
        public void AddCountry_CountryNameIsNull()
        {
            //Arrange
            CountryAddRequest? countryAddRequest = new CountryAddRequest() { CountryName = null };

            //Assert and Act
            Assert.Throws<ArgumentException>(() => _countriesService.AddCountry(countryAddRequest));
        }

        //When the CountryName is duplicate, then the AddCountry method should throw an ArgumentException
        [Fact]
        public void AddCountry_DuplicateCountryName()
        {
            //Arrange
            CountryAddRequest? countryAddRequest1 = new CountryAddRequest() { CountryName = "SriLanka" };
            CountryAddRequest? countryAddRequest2 = new CountryAddRequest() { CountryName = "SriLanka" };

            //Assert and Act
            Assert.Throws<ArgumentException>(() =>
            {
                _countriesService.AddCountry(countryAddRequest1);
                _countriesService.AddCountry(countryAddRequest2);
            });
        }

        //When you supply proper CountryName, then the AddCountry method should return a CountryResponse object with the same countryName and a new countryId
        [Fact]
        public void AddCountry_ProperCountryName()
        {
            //Arrange
            CountryAddRequest? countryAddRequest = new CountryAddRequest() { CountryName = "SriLanka" };
            
            //Act
            CountryResponse countryResponse=_countriesService.AddCountry(countryAddRequest);

            //Assert
            Assert.True(countryResponse.CountryId != Guid.Empty);
        }
        #endregion

        #region GetAllCountries Tests[Fact]
        [Fact]
        //The list of countries should be empty by default (before adding any countries)
        public void GetAllCountries_EmptyList()
        {
            //Act
            List<CountryResponse> actual_country_response_list = _countriesService.GetAllCountries();

            //Assert
            Assert.Empty(actual_country_response_list);
        }

        [Fact]
        public void GetAllCountries_AddFewCountries()
        {
            //Arrange
            List<CountryAddRequest> country_request_list = new List<CountryAddRequest>() {
        new CountryAddRequest() { CountryName = "USA" },
        new CountryAddRequest() { CountryName = "UK" }
      };

            //Act
            List<CountryResponse> countries_list_from_add_country = new List<CountryResponse>();

            foreach (CountryAddRequest country_request in country_request_list)
            {
                countries_list_from_add_country.Add(_countriesService.AddCountry(country_request));
            }

            List<CountryResponse> actualCountryResponseList = _countriesService.GetAllCountries();

            //read each element from countries_list_from_add_country
            foreach (CountryResponse expected_country in countries_list_from_add_country)
            {
                Assert.Contains(expected_country, actualCountryResponseList);
            }
        }



        #endregion

        #region GetCountryByCountryID Tests[Fact]
        [Fact]
        public void GetCountryByCountryID_NullCountryID()
        {
            //Arrange
            Guid? countryId = null;

            //Act
            CountryResponse? country_response_from_get_method=_countriesService.GetCountryByCountryID(countryId);

            //Assert
            Assert.Null(country_response_from_get_method);
        }

        [Fact]
        public void GetCountryByCountryID_ValidCountryId()
        {
            //Arrange
            CountryAddRequest? countryAddRequest=new CountryAddRequest() { CountryName = "SriLanka" };
            CountryResponse? country_response_from_add = _countriesService.AddCountry(countryAddRequest);

            //Act
            CountryResponse? country_response_from_get=_countriesService.GetCountryByCountryID(country_response_from_add.CountryId);

            //Assert
            Assert.Equal(country_response_from_add, country_response_from_get);
        }

        #endregion
    }
}

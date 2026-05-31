using Entities;
using ServiceContracts;
using ServiceContracts.DTO;
using ServiceContracts.Enums;
using Services;
using System;
using Xunit;
using Xunit.Abstractions;

namespace CRUDTests
{
    public class PersonsServiceTest
    {
        private readonly IPersonsService _personService;
        private readonly ICountriesService _countriesService;
        private readonly ITestOutputHelper _testOutputHelper;

        public PersonsServiceTest(ITestOutputHelper testOutputHelper)
        {
            _personService = new PersonsService();
            _countriesService=new CountriesService();
            _testOutputHelper = testOutputHelper;
        }

        #region AddPerson Tests[Fact]
        /// <summary>
        /// AddPerson method should throw ArgumentNullException when the input PersonAddRequest is null.
        /// </summary>
        [Fact]
        public void AddPerson_NullPerson()
        {
            //Arrange
            PersonAddRequest? personAddRequest = null;
            //Assert and Act
            Assert.Throws<ArgumentNullException>(() => _personService.AddPerson(personAddRequest));
        }

        /// <summary>
        /// Verifies that the AddPerson method throws an ArgumentException when the PersonName property of the request
        /// is null.
        /// </summary>
        /// <remarks>This test ensures that the AddPerson method enforces the requirement that PersonName
        /// must not be null, helping to validate input parameter checks.</remarks>
        [Fact]
        public void AddPerson_PersonNameIsNull()
        {
            //Arrange
            PersonAddRequest? personAddRequest = new PersonAddRequest() { PersonName = null };
            //Assert and Act
            Assert.Throws<ArgumentException>(() => _personService.AddPerson(personAddRequest));
        }

        [Fact]
        public void AddPerson_ProperPersonDetails()
        {
            //Arrange
            PersonAddRequest? personAddRequest = new PersonAddRequest()
            {
                PersonName = "John Doe",
                Email = "johndoe@gmail.com",
                DateOfBirth = new DateTime(1998, 5, 15),
                Gender = GenderOptions.Male,
                CountryID = Guid.Parse("d2719f8a-3f7c-4f2f-8d91-123456789abc"),
                Address = "123 Main Street, New York",
                ReceiveNewsLetters = true
            };
            //Act
            PersonResponse? personResponse = _personService.AddPerson(personAddRequest);
            List<PersonResponse> persons_list = _personService.GetAllPersons();
            //Assert
            Assert.NotNull(personResponse);
            //Assert.Equal(personAddRequest.PersonName, personResponse.PersonName);
            Assert.NotEqual(Guid.Empty, personResponse.PersonID);
            Assert.Contains(personResponse, persons_list);
        }
        #endregion

        #region GetPersonByPersonID Tests[Fact]
        /// <summary>
        /// If the GetPersonByPersonID method is called with a null PersonID, it should return null, indicating that no person can be retrieved without a valid identifier.
        /// </summary>
        [Fact]
        public void GetPersonByPersonID_PersonIDIsNull()
        {
            //Arrange
            Guid? personID = null;
            //Act
            PersonResponse? personResponse = _personService.GetPersonByPersonID(personID);
            //Assert
            Assert.Null(personResponse);
        }

        [Fact]
        public void GetPersonByPersonID_WithPersonID()
        {
            // Arrange
            CountryAddRequest? countryAddRequest = new CountryAddRequest() { CountryName = "India" };
            CountryResponse? countryResponse = _countriesService.AddCountry(countryAddRequest);

            PersonAddRequest? personAddRequest = new PersonAddRequest()
            {
                PersonName = "Jane Doe",
                Email = "sample@gmail.com",
                DateOfBirth = new DateTime(1995, 10, 20),
                Gender = GenderOptions.Female,
                CountryID = countryResponse.CountryId,
                Address = "456 Elm Street, Los Angeles",
                ReceiveNewsLetters = false

            };
            PersonResponse? personResponse = _personService.AddPerson(personAddRequest);
            // Act
            PersonResponse? retrievedPerson = _personService.GetPersonByPersonID(personResponse.PersonID);
            // Assert
            Assert.NotNull(retrievedPerson);
            Assert.Equal(personResponse, retrievedPerson);
        }
        #endregion

        #region GetAllPersons Tests[Fact]
        /// <summary>
        /// GetAllPersons method should return an empty list when there are no persons added to the service, indicating that the service correctly handles the case of an empty data set.
        /// </summary>
        [Fact]
        public void GetAllPersons_EmptyList()
        {
            // Act
            List<PersonResponse>persons_from_get=_personService.GetAllPersons();
            // Assert
            Assert.Empty(persons_from_get);
        }

        /// <summary>
        /// First,we will add a few persons to the service using the AddPerson method. Then, we will call the GetAllPersons method to retrieve the list of all persons. Finally, we will assert that the retrieved list contains the added persons and that the count of persons in the list matches the number of added persons.
        /// </summary>
        [Fact]
        public void GetAllPersons_AddFewPersons()
        {
            // Arrange
            CountryAddRequest? countryAddRequest1 = new CountryAddRequest() { CountryName = "USA" };
            CountryAddRequest? countryAddRequest2 = new CountryAddRequest() { CountryName = "UK" };

            CountryResponse? countryResponse1 = _countriesService.AddCountry(countryAddRequest1);
            CountryResponse? countryResponse2 = _countriesService.AddCountry(countryAddRequest2);

            PersonAddRequest? personAddRequest1 = new PersonAddRequest()
            {
                PersonName = "Jane Doe",
                Email = "sample@gmail.com",
                DateOfBirth = new DateTime(1995, 10, 20),
                Gender = GenderOptions.Female,
                CountryID = countryResponse1.CountryId,
                Address = "456 Elm Street, Los Angeles",
                ReceiveNewsLetters = false

            };
            PersonAddRequest? personAddRequest2 = new PersonAddRequest()
            {
                PersonName = "John Smith",
                Email = "random@gmail.com",
                DateOfBirth = new DateTime(1990, 3, 5),
                Gender = GenderOptions.Female,
                CountryID = countryResponse2.CountryId,
                Address = "789 Oak Avenue, Chicago",
                ReceiveNewsLetters = false
            };

            List<PersonAddRequest> person_add_requests_list = new List<PersonAddRequest>() { personAddRequest1, personAddRequest2 };

            List<PersonResponse> expected_persons_list = new List<PersonResponse>();

            foreach (PersonAddRequest person_add_request in person_add_requests_list)
            {
                PersonResponse personResponse=_personService.AddPerson(person_add_request);
                expected_persons_list.Add(personResponse);
            }

            // Log the expected persons for debugging purposes
            _testOutputHelper.WriteLine("Expected Persons:");
            foreach (PersonResponse expected_person in expected_persons_list)
            {
                _testOutputHelper.WriteLine(expected_person.ToString());
            }

            // Act
            List<PersonResponse> person_response_from_get = _personService.GetAllPersons();

            // Log the retrieved persons for debugging purposes
            _testOutputHelper.WriteLine("Persons Retrieved from GetAllPersons:");
            foreach (PersonResponse person_response in person_response_from_get)
            {
                _testOutputHelper.WriteLine(person_response.ToString());
            }

            // Assert
            foreach (PersonResponse expected_person in expected_persons_list)
            {
                Assert.Contains(expected_person, person_response_from_get);
            }
        }
        #endregion

        #region GetFilteredPersons Tests[Fact]
        /// <summary>
        /// This test verifies that the GetFilteredPersons method returns all persons when an empty search text is provided. It first adds a few persons to the service, then calls GetFilteredPersons with an empty string, and finally asserts that all added persons are included in the returned list, confirming that the filtering logic correctly handles empty search criteria.
        /// </summary>
        [Fact]
        public void GetAllPersons_EmptySearchText()
        {
            // Arrange
            CountryAddRequest? countryAddRequest1 = new CountryAddRequest() { CountryName = "USA" };
            CountryAddRequest? countryAddRequest2 = new CountryAddRequest() { CountryName = "UK" };

            CountryResponse? countryResponse1 = _countriesService.AddCountry(countryAddRequest1);
            CountryResponse? countryResponse2 = _countriesService.AddCountry(countryAddRequest2);

            PersonAddRequest? personAddRequest1 = new PersonAddRequest()
            {
                PersonName = "Jane Doe",
                Email = "sample@gmail.com",
                DateOfBirth = new DateTime(1995, 10, 20),
                Gender = GenderOptions.Female,
                CountryID = countryResponse1.CountryId,
                Address = "456 Elm Street, Los Angeles",
                ReceiveNewsLetters = false

            };
            PersonAddRequest? personAddRequest2 = new PersonAddRequest()
            {
                PersonName = "John Smith",
                Email = "random@gmail.com",
                DateOfBirth = new DateTime(1990, 3, 5),
                Gender = GenderOptions.Female,
                CountryID = countryResponse2.CountryId,
                Address = "789 Oak Avenue, Chicago",
                ReceiveNewsLetters = false
            };

            List<PersonAddRequest> person_add_requests_list = new List<PersonAddRequest>() { personAddRequest1, personAddRequest2 };

            List<PersonResponse> expected_persons_list = new List<PersonResponse>();

            foreach (PersonAddRequest person_add_request in person_add_requests_list)
            {
                PersonResponse personResponse = _personService.AddPerson(person_add_request);
                expected_persons_list.Add(personResponse);
            }

            // Log the expected persons for debugging purposes
            _testOutputHelper.WriteLine("Expected Persons:");
            foreach (PersonResponse expected_person in expected_persons_list)
            {
                _testOutputHelper.WriteLine(expected_person.ToString());
            }

            // Act
            List<PersonResponse> person_response_from_search = _personService.GetFilteredPersons(nameof(PersonResponse.PersonName),"");

            // Log the retrieved persons for debugging purposes
            _testOutputHelper.WriteLine("Persons Retrieved from GetFilteredPersons:");
            foreach (PersonResponse person_response in person_response_from_search)
            {
                _testOutputHelper.WriteLine(person_response.ToString());
            }

            // Assert
            foreach (PersonResponse expected_person in expected_persons_list)
            {
                Assert.Contains(expected_person, person_response_from_search);
            }
        }

        /// <summary>
        /// First we will add a few persons to the service using the AddPerson method. Then, we will call the GetFilteredPersons method with an empty search text to retrieve all persons. Finally, we will assert that the retrieved list contains all the added persons, confirming that the GetFilteredPersons method correctly returns all entries when no specific filter criteria are provided.
        /// </summary>

        [Fact]
        public void GetAllPersons_SearchByPersonName()
        {
            // Arrange
            CountryAddRequest? countryAddRequest1 = new CountryAddRequest() { CountryName = "USA" };
            CountryAddRequest? countryAddRequest2 = new CountryAddRequest() { CountryName = "UK" };

            CountryResponse? countryResponse1 = _countriesService.AddCountry(countryAddRequest1);
            CountryResponse? countryResponse2 = _countriesService.AddCountry(countryAddRequest2);

            PersonAddRequest? personAddRequest1 = new PersonAddRequest()
            {
                PersonName = "Jane Doe",
                Email = "sample@gmail.com",
                DateOfBirth = new DateTime(1995, 10, 20),
                Gender = GenderOptions.Female,
                CountryID = countryResponse1.CountryId,
                Address = "456 Elm Street, Los Angeles",
                ReceiveNewsLetters = false

            };
            PersonAddRequest? personAddRequest2 = new PersonAddRequest()
            {
                PersonName = "John Smith",
                Email = "random@gmail.com",
                DateOfBirth = new DateTime(1990, 3, 5),
                Gender = GenderOptions.Female,
                CountryID = countryResponse2.CountryId,
                Address = "789 Oak Avenue, Chicago",
                ReceiveNewsLetters = false
            };

            List<PersonAddRequest> person_add_requests_list = new List<PersonAddRequest>() { personAddRequest1, personAddRequest2 };

            List<PersonResponse> expected_persons_list = new List<PersonResponse>();

            foreach (PersonAddRequest person_add_request in person_add_requests_list)
            {
                PersonResponse personResponse = _personService.AddPerson(person_add_request);
                expected_persons_list.Add(personResponse);
            }

            // Log the expected persons for debugging purposes
            _testOutputHelper.WriteLine("Expected Persons:");
            foreach (PersonResponse expected_person in expected_persons_list)
            {
                _testOutputHelper.WriteLine(expected_person.ToString());
            }

            // Act
            List<PersonResponse> person_response_from_search = _personService.GetFilteredPersons(nameof(PersonResponse.PersonName), "s");

            // Log the retrieved persons for debugging purposes
            _testOutputHelper.WriteLine("Persons Retrieved from GetFilteredPersons:");
            foreach (PersonResponse person_response in person_response_from_search)
            {
                _testOutputHelper.WriteLine(person_response.ToString());
            }

            // Assert
            foreach (PersonResponse expected_person in expected_persons_list)
            {
                if(expected_person.PersonName != null && expected_person.PersonName.Contains("s", StringComparison.OrdinalIgnoreCase))
                {
                    Assert.Contains(expected_person, person_response_from_search);
                }
            }
        }
        #endregion

        #region GetSortedPersons Tests[Fact]

        //When we sort based on PersonName in DESC, it should return persons list in descending on PersonName
        [Fact]
        public void GetSortedPersons()
        {
            //Arrange
            CountryAddRequest country_request_1 = new CountryAddRequest() { CountryName = "USA" };
            CountryAddRequest country_request_2 = new CountryAddRequest() { CountryName = "India" };

            CountryResponse country_response_1 = _countriesService.AddCountry(country_request_1);
            CountryResponse country_response_2 = _countriesService.AddCountry(country_request_2);

            PersonAddRequest person_request_1 = new PersonAddRequest() { PersonName = "Smith", Email = "smith@example.com", Gender = GenderOptions.Male, Address = "address of smith", CountryID = country_response_1.CountryId, DateOfBirth = DateTime.Parse("2002-05-06"), ReceiveNewsLetters = true };

            PersonAddRequest person_request_2 = new PersonAddRequest() { PersonName = "Mary", Email = "mary@example.com", Gender = GenderOptions.Female, Address = "address of mary", CountryID = country_response_2.CountryId, DateOfBirth = DateTime.Parse("2000-02-02"), ReceiveNewsLetters = false };

            PersonAddRequest person_request_3 = new PersonAddRequest() { PersonName = "Rahman", Email = "rahman@example.com", Gender = GenderOptions.Male, Address = "address of rahman", CountryID = country_response_2.CountryId, DateOfBirth = DateTime.Parse("1999-03-03"), ReceiveNewsLetters = true };
            List<PersonAddRequest> person_requests = new List<PersonAddRequest>() { person_request_1, person_request_2, person_request_3 };

            List<PersonResponse> person_response_list_from_add = new List<PersonResponse>();

            foreach (PersonAddRequest person_request in person_requests)
            {
                PersonResponse person_response = _personService.AddPerson(person_request);
                person_response_list_from_add.Add(person_response);
            }

            //print person_response_list_from_add
            _testOutputHelper.WriteLine("Expected:");
            foreach (PersonResponse person_response_from_add in person_response_list_from_add)
            {
                _testOutputHelper.WriteLine(person_response_from_add.ToString());
            }
            List<PersonResponse> allPersons = _personService.GetAllPersons();

            //Act
            List<PersonResponse> persons_list_from_sort = _personService.GetSortedPersons(allPersons, nameof(Person.PersonName), SortOrderOptions.DESC);

            //print persons_list_from_get
            _testOutputHelper.WriteLine("Actual:");
            foreach (PersonResponse person_response_from_get in persons_list_from_sort)
            {
                _testOutputHelper.WriteLine(person_response_from_get.ToString());
            }
            person_response_list_from_add = person_response_list_from_add.OrderByDescending(temp => temp.PersonName).ToList();

            //Assert
            for (int i = 0; i < person_response_list_from_add.Count; i++)
            {
                Assert.Equal(person_response_list_from_add[i], persons_list_from_sort[i]);
            }
        }
        #endregion

        #region UpdatePerson

        //When we supply null as PersonUpdateRequest, it should throw ArgumentNullException
        [Fact]
        public void UpdatePerson_NullPerson()
        {
            //Arrange
            PersonUpdateRequest? person_update_request = null;

            //Assert
            Assert.Throws<ArgumentNullException>(() => {
                //Act
                _personService.UpdatePerson(person_update_request);
            });
        }


        //When we supply invalid person id, it should throw ArgumentException
        [Fact]
        public void UpdatePerson_InvalidPersonID()
        {
            //Arrange
            PersonUpdateRequest? person_update_request = new PersonUpdateRequest() { PersonID = Guid.NewGuid() };

            //Assert
            Assert.Throws<ArgumentException>(() => {
                //Act
                _personService.UpdatePerson(person_update_request);
            });
        }


        //When PersonName is null, it should throw ArgumentException
        [Fact]
        public void UpdatePerson_PersonNameIsNull()
        {
            //Arrange
            CountryAddRequest country_add_request = new CountryAddRequest() { CountryName = "UK" };
            CountryResponse country_response_from_add = _countriesService.AddCountry(country_add_request);

            PersonAddRequest person_add_request = new PersonAddRequest() { PersonName = "John", Email = "john@example.com", Gender = GenderOptions.Male, CountryID = country_response_from_add.CountryId };
            PersonResponse person_response_from_add = _personService.AddPerson(person_add_request);

            PersonUpdateRequest person_update_request = person_response_from_add.ToPersonUpdateRequest();
            person_update_request.PersonName = null;


            //Assert
            Assert.Throws<ArgumentException>(() => {
                //Act
                _personService.UpdatePerson(person_update_request);
            });

        }


        //First, add a new person and try to update the person name and email
        [Fact]
        public void UpdatePerson_PersonFullDetailsUpdation()
        {
            //Arrange
            CountryAddRequest country_add_request = new CountryAddRequest() { CountryName = "UK" };
            CountryResponse country_response_from_add = _countriesService.AddCountry(country_add_request);

            PersonAddRequest person_add_request = new PersonAddRequest() { PersonName = "John", CountryID = country_response_from_add.CountryId, Address = "Abc road", DateOfBirth = DateTime.Parse("2000-01-01"), Email = "abc@example.com", Gender = GenderOptions.Male, ReceiveNewsLetters = true };

            PersonResponse person_response_from_add = _personService.AddPerson(person_add_request);

            PersonUpdateRequest person_update_request = person_response_from_add.ToPersonUpdateRequest();
            person_update_request.PersonName = "William";
            person_update_request.Email = "william@example.com";

            //Act
            PersonResponse person_response_from_update = _personService.UpdatePerson(person_update_request);

            PersonResponse? person_response_from_get = _personService.GetPersonByPersonID(person_response_from_update.PersonID);

            //Assert
            Assert.Equal(person_response_from_get, person_response_from_update);

        }
        #endregion

        #region DeletePerson

        //If you supply an valid PersonID, it should return true
        [Fact]
        public void DeletePerson_ValidPersonID()
        {
            //Arrange
            CountryAddRequest country_add_request = new CountryAddRequest() { CountryName = "USA" };
            CountryResponse country_response_from_add = _countriesService.AddCountry(country_add_request);

            PersonAddRequest person_add_request = new PersonAddRequest() { PersonName = "Jones", Address = "address", CountryID = country_response_from_add.CountryId, DateOfBirth = Convert.ToDateTime("2010-01-01"), Email = "jones@example.com", Gender = GenderOptions.Male, ReceiveNewsLetters = true };

            PersonResponse person_response_from_add = _personService.AddPerson(person_add_request);


            //Act
            bool isDeleted = _personService.DeletePerson(person_response_from_add.PersonID);

            //Assert
            Assert.True(isDeleted);
        }


        //If you supply an invalid PersonID, it should return false
        [Fact]
        public void DeletePerson_InvalidPersonID()
        {
            //Act
            bool isDeleted = _personService.DeletePerson(Guid.NewGuid());

            //Assert
            Assert.False(isDeleted);
        }

        #endregion
    }
}

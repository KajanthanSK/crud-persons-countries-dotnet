using ServiceContracts.DTO;
using ServiceContracts.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace ServiceContracts
{
    public interface IPersonsService
    {
        /// <summary>
        /// AddPerson method is used to add a new person to the system. It takes a PersonAddRequest object as input and returns a PersonResponse object containing the details of the added person. If the input is null or invalid, it may return null or throw an exception based on the implementation.
        /// </summary>
        /// <param name="personAddRequest"></param>
        /// <returns></returns>
        PersonResponse AddPerson(PersonAddRequest? personAddRequest);

        /// <summary>
        /// Retrieves all persons as a list of response objects.
        /// </summary>
        /// <returns>A list of <see cref="PersonResponse"/> objects representing all persons. The list is empty if no persons are
        /// found.</returns>
        List<PersonResponse> GetAllPersons();

        /// <summary>
        /// Retrieves a person by their unique identifier.
        /// </summary>
        /// <param name="personID">The unique identifier of the person.</param>
        /// <returns>A <see cref="PersonResponse"/> object representing the person, or null if not found.</returns>
        PersonResponse? GetPersonByPersonID(Guid? personID);

        /// <summary>
        /// Returns a list of persons filtered based on the specified criteria. The filtering is performed based on the provided searchBy parameter, which indicates the field to filter by (e.g., "Name", "Email", "Country"). The searchString parameter contains the value to match against the specified field. If searchString is null or empty, the method may return all persons or an empty list, depending on the implementation.
        /// </summary>
        /// <param name="searchBy">Search field to search</param>
        /// <param name="searchString">Search string to search</param>
        /// <returns>Returns all matching persons based on the given search field and search string</returns>
        List<PersonResponse> GetFilteredPersons(string searchBy, string? searchString);

        /// <summary>
        /// Returns a list of persons sorted based on the specified criteria. The sorting is performed based on the provided sortBy parameter, which indicates the field to sort by (e.g., "Name", "Email", "Country"). The sortOrder parameter specifies the order of sorting, which can be either ascending (ASC) or descending (DESC). If sortBy is null or empty, the method may return the list of persons without sorting or throw an exception, depending on the implementation.
        /// </summary>
        /// <param name="allPersons">The list of persons to be sorted.</param>
        /// <param name="sortBy">The field to sort by.</param>
        /// <param name="sortOrder">The order of sorting (ascending or descending).</param>
        /// <returns>A list of sorted <see cref="PersonResponse"/> objects.</returns>
        List<PersonResponse> GetSortedPersons(List<PersonResponse> allPersons, string sortBy, SortOrderOptions sortOrder);

        /// <summary>
        /// Updates the specified person details based on the given person ID
        /// </summary>
        /// <param name="personUpdateRequest">Person details to update, including person id</param>
        /// <returns>Returns the person response object after updation</returns>
        PersonResponse UpdatePerson(PersonUpdateRequest? personUpdateRequest);

        /// <summary>
        /// Deletes a person based on the given person id
        /// </summary>
        /// <param name="PersonID">PersonID to delete</param>
        /// <returns>Returns true, if the deletion is successful; otherwise false</returns>
        bool DeletePerson(Guid? PersonID);
    }
}

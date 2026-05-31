using Entities;
using ServiceContracts.Enums;
using System;

namespace ServiceContracts.DTO
{
    public class PersonResponse
    {
        public Guid PersonID { get; set; }
        public string? PersonName { get; set; }
        public string? Email { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string? Gender { get; set; }
        public Guid? CountryID { get; set; }
        public string? Country { get; set; }
        public string? Address { get; set; }
        public bool ReceiveNewsLetters { get; set; }
        public double? Age { get; set; }

        public override bool Equals(object? obj)
        {
            if (obj == null)
            {
                return false;
            }
            if (obj.GetType() != typeof(PersonResponse))
            {
                return false;
            }
            PersonResponse person_to_compare = (PersonResponse)obj;
            return PersonID == person_to_compare.PersonID && PersonName == person_to_compare.PersonName && Email == person_to_compare.Email
                && DateOfBirth == person_to_compare.DateOfBirth && Gender == person_to_compare.Gender && CountryID == person_to_compare.CountryID
                && Country == person_to_compare.Country && Address == person_to_compare.Address && ReceiveNewsLetters == person_to_compare.ReceiveNewsLetters
                && Age == person_to_compare.Age;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string ToString()
        {
            return $"PersonID: {PersonID}, PersonName: {PersonName}, Email: {Email}, DateOfBirth: {DateOfBirth}, Gender: {Gender}, CountryID: {CountryID}, Country: {Country}, Address: {Address}, ReceiveNewsLetters: {ReceiveNewsLetters}, Age: {Age}";
        }

        public PersonUpdateRequest ToPersonUpdateRequest()
        {
            return new PersonUpdateRequest() { PersonID = PersonID, PersonName = PersonName, Email = Email, DateOfBirth = DateOfBirth, Gender = (GenderOptions)Enum.Parse(typeof(GenderOptions), Gender, true), Address = Address, CountryID = CountryID, ReceiveNewsLetters = ReceiveNewsLetters };
        }
    }

    public static class PersonResponseExtension
    {
        public static PersonResponse ToPersonResponse(this Person person)
        {
            return new PersonResponse()
            {
                PersonID = person.PersonID,
                PersonName = person.PersonName,
                Email = person.Email,
                DateOfBirth = person.DateOfBirth,
                Gender = person.Gender,
                Address = person.Address,
                ReceiveNewsLetters = person.ReceiveNewsLetters,
                CountryID= person.CountryID,
                Age = (person.DateOfBirth != null) ? Math.Round((DateTime.Now - person.DateOfBirth.Value).TotalDays / 365.25) : null
            };
        }
    }
}

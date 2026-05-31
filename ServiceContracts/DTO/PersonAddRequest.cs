using ServiceContracts.Enums;
using Entities;
using System.ComponentModel.DataAnnotations;
using System;

namespace ServiceContracts.DTO
{
    /// <summary>
    /// Represents a request to add a new person, including personal and contact information.
    /// </summary>
    /// <remarks>This class is typically used as a data transfer object when creating a new person record in
    /// an application. All properties are optional unless otherwise required by the consuming API or service.</remarks>
    public class PersonAddRequest
    {
        [Required(ErrorMessage = "Person name is required.")]
        public string? PersonName { get; set; }
        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email format.")]
        public string? Email { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public GenderOptions? Gender { get; set; }
        public Guid? CountryID { get; set; }
        public string? Address { get; set; }
        public bool ReceiveNewsLetters { get; set; }

        /// <summary>
        /// Converts the current <see cref="PersonAddRequest"/> instance to a <see cref="Person"/> entity.
        /// </summary>
        /// <returns>A <see cref="Person"/> entity with properties mapped from the current request.</returns>
        public Person ToPerson()
        {
            return new Person 
            {
                PersonID = Guid.NewGuid(), // Generate a new unique identifier for the person
                PersonName = this.PersonName,
                Email = this.Email,
                DateOfBirth = this.DateOfBirth,
                Gender = this.Gender?.ToString(),
                CountryID = this.CountryID,
                Address = this.Address,
                ReceiveNewsLetters = this.ReceiveNewsLetters
            };
        }
    }
}

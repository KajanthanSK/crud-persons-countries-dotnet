using System;
using System.Collections.Generic;
using System.Text;
using Entities;

namespace ServiceContracts.DTO
{
    /// <summary>
    /// DTO for adding a country. It has a method to convert it to Country domain model
    /// </summary>
    public class CountryAddRequest
    {
        public string? CountryName { get; set; }

        public Country ToCountry()
        {
            return new Country()
            {
                CountryId = Guid.NewGuid(),
                CountryName = this.CountryName
            };
        }
    }
}

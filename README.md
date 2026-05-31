# crud-persons-countries-dotnet

A clean-architecture CRUD application built with **C# / .NET** that manages **Persons** and **Countries** through a service layer pattern, backed by a comprehensive **xUnit** test suite following TDD principles.

---

## Project Structure

```
├── Entities/                  # Domain models (Person, Country)
├── ServiceContracts/
│   ├── DTO/                   # Data Transfer Objects (Add/Update/Response)
│   ├── Enums/                 # GenderOptions, SortOrderOptions
│   ├── ICountriesService.cs   # Countries service contract
│   └── IPersonsService.cs     # Persons service contract
├── Services/
│   ├── CountriesService.cs    # Countries business logic
│   └── PersonsService.cs      # Persons business logic
└── CRUDTests/
    ├── CountriesServiceTest.cs
    └── PersonsServiceTest.cs
```

---

## Features

### Countries
- Add a new country (with duplicate name validation)
- Retrieve all countries
- Retrieve a country by ID

### Persons
- Add a new person with full details (name, email, DOB, gender, address, country)
- Retrieve all persons
- Retrieve a person by ID
- Filter persons by any field (e.g. search by name)
- Sort persons by any field in ascending or descending order
- Update person details
- Delete a person by ID

---

## Tech Stack

| Layer | Technology |
|---|---|
| Language | C# (.NET) |
| Testing | xUnit |
| Test Output | `ITestOutputHelper` |
| Architecture | Service Layer + DTOs |

---

﻿using PokemonReviewApp.Models;

namespace PokemonReviewApp.Interfaces;

public interface ICountryRepository
{
    ICollection<Country> GetCountries();
    Country GetCountry(int countryId);
    Country GetCountryByOwner(int ownerId);
    ICollection<Owner> GetOwnersFromCountry(int countryId);
    bool CountryExists(int countryId);
    bool CreateCountry(Country country);
    bool UpdateCountry(Country country);
    bool Save();
}
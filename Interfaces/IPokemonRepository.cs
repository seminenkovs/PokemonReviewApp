using PokemonReviewApp.Models;

namespace PokemonReviewApp.Interfaces;

public interface IPokemonRepository
{
    ICollection<Pokemon> GetPokemons();
    Pokemon GetPokemon(int pokemonId);
    Pokemon GetPokemon(string name);
    decimal GetPokemonRating(int pokemonId);
    bool PokemonExists(int pokemonId);
    bool CreatePokemon(int ownerId, int categoryId, Pokemon pokemon);
    bool UpdatePokemon(int ownerId, int categoryId, Pokemon pokemon);
    bool Save();
}
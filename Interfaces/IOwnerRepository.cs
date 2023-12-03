using PokemonReviewApp.Models;

namespace PokemonReviewApp.Interfaces;

public interface IOwnerRepository
{
    ICollection<Owner> GetOwners();
    Owner GetOwner(int ownerId);
    ICollection<Owner> GetOwnersOfPokemon(int pokemonId);
    ICollection<Pokemon> GetPokemonByOwner(int ownerId);
    bool OwnerExists(int ownerId);
    bool CreateOwner(Owner owner);
    bool UpdateOwner (Owner owner);
    bool Save();
}
using PokemonReviewApp.Models;

namespace PokemonReviewApp.Interfaces;

public interface ICategoryRepository
{
    ICollection<Category> GetCategories();
    Category GetCategory(int categoryid);
    ICollection<Pokemon> GetPokemonsByCategory(int categoryId);
    bool CategoryExists(int categoryid);
    bool CreateCategory(Category category);
    bool UpdateCategory(Category category);
    bool Save();
}
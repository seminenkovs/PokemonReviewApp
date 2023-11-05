using PokemonReviewApp.Data;
using PokemonReviewApp.Interfaces;
using PokemonReviewApp.Models;

namespace PokemonReviewApp.Repository;

public class CategoryRepository : ICategoryRepository
{
    private readonly DataContext _context;

    public CategoryRepository(DataContext context)
    {
        _context = context;
    }

    public bool CategoryExists(int categoryid)
    {
        return _context.Categories.Any(c => c.Id == categoryid);
    }

    public ICollection<Category> GetCategories()
    {
        return _context.Categories.ToList();
    }

    public Category GetCategory(int categoryid)
    {
        return _context.Categories.Where(c => c.Id == categoryid).FirstOrDefault();
    }

    public ICollection<Pokemon> GetPokemonsByCategory(int categoryId)
    {
        return _context.PokemonCategories.Where(c => c.CategoryId == categoryId).Select(c => c.Pokemon).ToList();
    }
}
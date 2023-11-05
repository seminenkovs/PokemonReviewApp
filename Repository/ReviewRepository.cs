using PokemonReviewApp.Data;
using PokemonReviewApp.Interfaces;
using PokemonReviewApp.Models;

namespace PokemonReviewApp.Repository;

public class ReviewRepository : IReviewRepository
{
    private readonly DataContext _context;

    public ReviewRepository(DataContext context)
    {
        _context = context;
    }

    public ICollection<Review> GetReviews()
    {
        return _context.Reviews.ToList();
    }

    public Review GetReview(int reviewId)
    {
        return _context.Reviews.Where(r => r.Id == reviewId).FirstOrDefault();
    }

    public ICollection<Review> GetReviewsOfPokemon(int pokemonId)
    {
        return _context.Reviews.Where(r => r.Pokemon.Id == pokemonId).ToList();
    }

    public bool ReviewExists(int reviewId)
    {
        return _context.Reviews.Any(r => r.Id == reviewId);
    }
}
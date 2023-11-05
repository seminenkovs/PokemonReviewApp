using Microsoft.EntityFrameworkCore;
using PokemonReviewApp.Data;
using PokemonReviewApp.Interfaces;
using PokemonReviewApp.Models;

namespace PokemonReviewApp.Repository;

public class ReviewerRepository : IReviewerRepository
{
    private readonly DataContext _context;

    public ReviewerRepository(DataContext context)
    {
        _context = context;
    }

    public ICollection<Reviewer> GetReviewers()
    {
        return _context.Reviewser.ToList();
    }

    public Reviewer GetReviewer(int reviewerId)
    {
        return _context.Reviewser.Where(r => r.Id == reviewerId)
            .Include(e => e.Reviews).FirstOrDefault();
    }

    public ICollection<Review> GetReviewsByReviewer(int reviewerId)
    {
        return _context.Reviews.Where(r => r.Reviewer.Id == reviewerId).ToList();
    }

    public bool ReviewerExists(int reviewerId)
    {
        return _context.Reviewser.Any(r => r.Id == reviewerId);
    }
}
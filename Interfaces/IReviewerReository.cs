using PokemonReviewApp.Models;

namespace PokemonReviewApp.Interfaces;

public interface IReviewerReository
{
    ICollection<Reviewer> GetReviewers();
    Reviewer GetReviewer(int reviewerId);
}
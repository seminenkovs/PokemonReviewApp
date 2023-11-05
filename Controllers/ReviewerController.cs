using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PokemonReviewApp.Dto;
using PokemonReviewApp.Interfaces;
using PokemonReviewApp.Models;
using PokemonReviewApp.Repository;

namespace PokemonReviewApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewerController : ControllerBase
    {
        private readonly ReviewerReository _reviewerReository;
        private readonly IMapper _mapper;

        public ReviewerController(ReviewerReository reviewerReository, IMapper mapper)
        {
            _reviewerReository = reviewerReository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Reviewer>))]
        public IActionResult GetReviewers()
        {
            var reviewers = _mapper.Map<List<ReviewerDto>>(_reviewerReository.GetReviewers());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(reviewers);
        }

        [HttpGet("{reviewerId}")]
        [ProducesResponseType(200, Type = typeof(Reviewer))]
        [ProducesResponseType(400)]
        public IActionResult GetReview(int reviewerId)
        {
            if (_reviewerReository.ReviewerExists(reviewerId))
            {
                return NotFound();
            }
            var reviewer = _mapper.Map<ReviewerDto>(_reviewerReository.GetReviewer(reviewerId));

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(reviewer);
        }

        [HttpGet("reviews/{reviewerId}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Review>))]
        [ProducesResponseType(400)]
        public IActionResult GetReviewsByReviewer(int reviewerId)
        {
            if (!_reviewerReository.ReviewerExists(reviewerId))
            {
                return NotFound();
            }
            var reviews = _mapper.Map<List<ReviewDto>>(_reviewerReository.GetReviewsByReviewer(reviewerId));

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(reviews);
        }
    }
}

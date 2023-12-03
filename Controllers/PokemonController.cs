using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PokemonReviewApp.Dto;
using PokemonReviewApp.Interfaces;
using PokemonReviewApp.Models;
using PokemonReviewApp.Repository;

namespace PokemonReviewApp.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PokemonController : ControllerBase
{
    private readonly IPokemonRepository _pokemonRepository;
    private readonly IOwnerRepository _ownerRepository;
    private readonly IReviewRepository _reviewRepository;
    private readonly IMapper _mapper;

    public PokemonController(IPokemonRepository pokemonRepository, IOwnerRepository ownerRepository,
        IReviewRepository reviewRepository, IMapper mapper)
    {
        _pokemonRepository = pokemonRepository;
        _ownerRepository = ownerRepository;
        _reviewRepository = reviewRepository;
        _mapper = mapper;
    }

    [HttpGet]
    [ProducesResponseType(200, Type = typeof(IEnumerable<Pokemon>))]
    public IActionResult GetPokemons()
    {
        var pokemons = _mapper.Map<List<PokemonDto>>(_pokemonRepository.GetPokemons());

        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        return Ok(pokemons);
    }

    [HttpGet("{pokemonId}")]
    [ProducesResponseType(200, Type = typeof(Pokemon))]
    [ProducesResponseType(400)]
    public IActionResult GetPokemon(int pokemonId)
    {
        if (!_pokemonRepository.PokemonExists(pokemonId))
        {
            return NotFound();
        }
        var pokemon = _mapper.Map<PokemonDto>(_pokemonRepository.GetPokemon(pokemonId));

        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        return Ok(pokemon);
    }

    [HttpGet("{pokemonId}/rating")]
    [ProducesResponseType(200, Type = typeof(decimal))]
    [ProducesResponseType(400)]
    public IActionResult GetPokemonRating(int pokemonId)
    {
        if (!_pokemonRepository.PokemonExists(pokemonId))
        {
            return NotFound();
        }

        var rating = _pokemonRepository.GetPokemonRating(pokemonId);

        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        return Ok(rating);
    }

    [HttpPost]
    [ProducesResponseType(204)]
    [ProducesResponseType(400)]
    public IActionResult CreatePokemon([FromQuery] int ownerId, [FromQuery] int categoryId, [FromBody] PokemonDto pokemonCreate)
    {
        if (pokemonCreate == null)
        {
            return BadRequest(ModelState);
        }

        var pokemon = _pokemonRepository.GetPokemons()
            .Where(p => p.Name.Trim().ToUpper() == pokemonCreate.Name.TrimEnd().ToUpper())
            .FirstOrDefault();

        if (pokemon != null)
        {
            ModelState.AddModelError("", "Pokemon already exists");
            return StatusCode(442, ModelState);
        }

        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var pokemonMap = _mapper.Map<Pokemon>(pokemonCreate);


        if (!_pokemonRepository.CreatePokemon(ownerId, categoryId, pokemonMap))
        {
            ModelState.AddModelError("", "Somethig went wrong while saving");
            return StatusCode(500, ModelState);
        }

        return Ok("Seccessfully created");
    }

    [HttpPut("{pokemonId}")]
    [ProducesResponseType(400)]
    [ProducesResponseType(204)]
    [ProducesResponseType(404)]
    public IActionResult UpdatePokemon([FromQuery] int pokemonId, [FromQuery] int owner,
        [FromQuery] int caegoryId , [FromBody] PokemonDto updatedPokemon)
    {
        if (updatedPokemon == null)
        {
            return BadRequest(ModelState);
        }

        if (pokemonId != updatedPokemon.Id)
        {
            return BadRequest(ModelState);
        }

        if (!_pokemonRepository.PokemonExists(pokemonId))
        {
            return NotFound();
        }

        if (!ModelState.IsValid)
        {
            return BadRequest();
        }

        var pokemonMap = _mapper.Map<Pokemon>(updatedPokemon);

        if (!_pokemonRepository.UpdatePokemon(owner, caegoryId, pokemonMap))
        {
            ModelState.AddModelError("", "Something went wrong updating pokemon");
            return StatusCode(500, ModelState);
        }

        return NoContent();
    }

}
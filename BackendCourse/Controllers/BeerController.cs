using BackendCourse.DTOs;
using BackendCourse.Models;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BackendCourse.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BeerController : ControllerBase
    {
        private readonly StoreContext _storeContext;
        private readonly IValidator<BeerInsertDTO> _beerInsertValidator;
        private readonly IValidator<BeerUpdateDTO> _beerUpdateValidator;

        public BeerController(StoreContext storeContext, 
                              IValidator<BeerInsertDTO> beerInsertValidator, 
                              IValidator<BeerUpdateDTO> beerUpdateValidator)

        {
            _storeContext = storeContext;
            _beerInsertValidator = beerInsertValidator;
            _beerUpdateValidator = beerUpdateValidator;
        }


        [HttpGet]
        public async Task<IEnumerable<BeerDTO>> GetBeer() =>

            await _storeContext.Beers.Select(x => new BeerDTO
            {
                Id = x.BeerId,
                Name = x.Name,
                Alcohol = x.Alcohol,
                BrandId = x.BrandId,


            }).ToListAsync();


        [HttpGet("{id}")]
        public async Task<ActionResult<BeerDTO>> GetById(int id)
        {
            var beer = await _storeContext.Beers.FindAsync(id);

            if (beer is null)
            {
                return NotFound();
            }

            var beerDto = new BeerDTO
            {
                Id = beer.BeerId,
                Name = beer.Name,
                Alcohol = beer.Alcohol,
                BrandId = beer.BrandId


            };

            return Ok(beerDto);

        }

        [HttpPost]

        public async Task<ActionResult<BeerDTO>> Add(BeerInsertDTO beerInsertDTO)
        {
            var validationResult = await _beerInsertValidator.ValidateAsync(beerInsertDTO);

            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            var beer = new Beer()
            {
                Name = beerInsertDTO.Name,
                BrandId = beerInsertDTO.BrandId,
                Alcohol = beerInsertDTO.Alcohol
            };

            await _storeContext.Beers.AddAsync(beer);
            await _storeContext.SaveChangesAsync();

            var beerDto = new BeerDTO
            {
                Id = beer.BeerId,
                Name = beer.Name,
                Alcohol = beer.Alcohol,
                BrandId = beer.BrandId
            };


            return CreatedAtAction(nameof(GetById), new {id = beer.BeerId}, beerDto);


        }

        [HttpPut("{id}")]
        public async Task<ActionResult<BeerDTO>> Update(int id, BeerUpdateDTO updateDTO)
        {

            var validationResult = await _beerUpdateValidator.ValidateAsync(updateDTO);


            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            var beer = await _storeContext.Beers.FindAsync(id);

            if (beer is null)
            {
                return NotFound();
            }

            beer.Name = updateDTO.Name;
            beer.BrandId = updateDTO.BrandId;
            beer.Alcohol = updateDTO.Alcohol;

            await _storeContext.SaveChangesAsync();

            var beerDto = new BeerDTO
            {
                Id = beer.BeerId,
                Name = beer.Name,
                Alcohol = beer.Alcohol,
                BrandId = beer.BrandId
            };

            return Ok(beerDto);

        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<BeerDTO>> Delete(int id)
        {
            var beer = await _storeContext.Beers.FindAsync(id);

            if (beer is null)
            {
                return NotFound();
            }

            _storeContext.Beers.Remove(beer);
            await _storeContext.SaveChangesAsync();

            var beerDto = new BeerDTO
            {
                Id = beer.BeerId,
                Name = beer.Name,
                Alcohol = beer.Alcohol,
                BrandId = beer.BrandId
            };

            return Ok(beerDto); ;

        }


    }
}


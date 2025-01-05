using BackendCourse.DTOs;
using BackendCourse.Models;
using BackendCourse.Services;
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
       
        private readonly IValidator<BeerInsertDTO> _beerInsertValidator;
        private readonly IValidator<BeerUpdateDTO> _beerUpdateValidator;
        private readonly ICommonService<BeerDTO, BeerInsertDTO, BeerUpdateDTO> _beerService;
        

        public BeerController( 
                              IValidator<BeerInsertDTO> beerInsertValidator, 
                              IValidator<BeerUpdateDTO> beerUpdateValidator,
                              [FromKeyedServices("beerService")] ICommonService<BeerDTO,BeerInsertDTO,BeerUpdateDTO> beerService)

        {
            
            _beerInsertValidator = beerInsertValidator;
            _beerUpdateValidator = beerUpdateValidator;
            _beerService = beerService;
        }


        [HttpGet]
        public async Task<IEnumerable<BeerDTO>> GetBeer() =>

            await _beerService.Get();



        [HttpGet("{id}")]
        public async Task<ActionResult<BeerDTO>> GetById(int id)
        {
            var beerDto = await _beerService.GetById(id);


            return beerDto is null ? NotFound(): Ok(beerDto);

        }

        [HttpPost]

        public async Task<ActionResult<BeerDTO>> Add(BeerInsertDTO beerInsertDTO)
        {
            var validationResult = await _beerInsertValidator.ValidateAsync(beerInsertDTO);

            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }


            if (!_beerService.Validate(beerInsertDTO))
            {
                return BadRequest(_beerService.Errors);
            }

            var beerDto = await _beerService.Add(beerInsertDTO);
          
            return CreatedAtAction(nameof(GetById), new {id = beerDto.Id}, beerDto);
          


        }


        [HttpPut("{id}")]
        public async Task<ActionResult<BeerDTO>> Update(int id, BeerUpdateDTO updateDTO)
        {

            var validationResult = await _beerUpdateValidator.ValidateAsync(updateDTO);


            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            if (!_beerService.Validate(updateDTO))
            {
                return BadRequest(_beerService.Errors);
            }


            var beerDto = await _beerService.Update(id, updateDTO);
           

            return beerDto is null ? NotFound(): Ok(beerDto);

        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<BeerDTO>> Delete(int id)
        {
           
            var beerDto = await _beerService.Delete(id);

            return beerDto == null ? NotFound() : Ok(beerDto);


        }


    }
}


using BackendCourse.DTOs;
using BackendCourse.Models;
using BackendCourse.Repository;
using Microsoft.EntityFrameworkCore;

namespace BackendCourse.Services
{
    public class BeerServices : ICommonService<BeerDTO, BeerInsertDTO, BeerUpdateDTO>

    {


        private readonly IRepository<Beer> _beerRepository;

        public BeerServices(IRepository<Beer> beerRepository)
        {

            _beerRepository = beerRepository;
        }

        public async Task<IEnumerable<BeerDTO>> Get()
        {

            var beers = await _beerRepository.Get();

            return beers.Select(b => new BeerDTO
            {

                Id = b.BeerId,
                Name = b.Name,
                Alcohol = b.Alcohol,
                BrandId = b.BrandId

            });

        }

        public async Task<BeerDTO> GetById(int id)
        {
            var beer = await _beerRepository.GetById(id);

            if (beer != null)
            {
                var beerDto = new BeerDTO
                {
                    Id = beer.BeerId,
                    Name = beer.Name,
                    Alcohol = beer.Alcohol,
                    BrandId = beer.BrandId


                };
                return beerDto;
            }

            return null;
        }



        public async Task<BeerDTO> Add(BeerInsertDTO beerInsertDTO)
        {
            var beer = new Beer()
            {
                Name = beerInsertDTO.Name,
                BrandId = beerInsertDTO.BrandId,
                Alcohol = beerInsertDTO.Alcohol
            };

            await _beerRepository.Add(beer);
            await _beerRepository.Save();

            var beerDto = new BeerDTO
            {
                Id = beer.BeerId,
                Name = beer.Name,
                Alcohol = beer.Alcohol,
                BrandId = beer.BrandId
            };

            return beerDto;

        }


        public async Task<BeerDTO> Update(int id, BeerUpdateDTO updateDTO)
        {
            var beer = await _beerRepository.GetById(id);

            if (beer != null)
            {

                beer.Name = updateDTO.Name;
                beer.BrandId = updateDTO.BrandId;
                beer.Alcohol = updateDTO.Alcohol;

                _beerRepository.Update(beer);
                await _beerRepository.Save();

                var beerDto = new BeerDTO
                {
                    Id = beer.BeerId,
                    Name = beer.Name,
                    Alcohol = beer.Alcohol,
                    BrandId = beer.BrandId
                };

                return beerDto;


            }
            return null;
        }



        public async Task<BeerDTO> Delete(int id)
        {
            var beer = await _beerRepository.GetById(id);

            if (beer != null)
            {
                var beerDto = new BeerDTO
                {
                    Id = beer.BeerId,
                    Name = beer.Name,
                    Alcohol = beer.Alcohol,
                    BrandId = beer.BrandId
                };

                _beerRepository.Delete(beer);
                await _beerRepository.Save();


                return beerDto;
            }

            return null;

        }
    }
}

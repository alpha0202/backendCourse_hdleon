using AutoMapper;
using BackendCourse.DTOs;
using BackendCourse.Models;
using BackendCourse.Repository;
using Microsoft.EntityFrameworkCore;

namespace BackendCourse.Services
{
    public class BeerServices : ICommonService<BeerDTO, BeerInsertDTO, BeerUpdateDTO>

    {
        private readonly IRepository<Beer> _beerRepository;
        private readonly IMapper _mapper;

        public List<string> Errors { get; }

        public BeerServices(IRepository<Beer> beerRepository, IMapper mapper)
        {

            _beerRepository = beerRepository;
            _mapper = mapper;
            Errors = new List<string>();
        }

        public async Task<IEnumerable<BeerDTO>> Get()
        {

            var beers = await _beerRepository.Get();

            return beers.Select(b => _mapper.Map<BeerDTO>(b));

        }

        public async Task<BeerDTO> GetById(int id)
        {
            var beer = await _beerRepository.GetById(id);

            if (beer != null)
            {
                var beerDto = _mapper.Map<BeerDTO>(beer);

                return beerDto;
            }

            return null;
        }



        public async Task<BeerDTO> Add(BeerInsertDTO beerInsertDTO)
        {
           var beer = _mapper.Map<Beer>(beerInsertDTO);

            await _beerRepository.Add(beer);
            await _beerRepository.Save();

           var beerDto = _mapper.Map<BeerDTO>(beer);

            return beerDto;

        }


        public async Task<BeerDTO> Update(int id, BeerUpdateDTO updateDTO)
        {
            var beer = await _beerRepository.GetById(id);

            if (beer != null)
            {

                beer = _mapper.Map<BeerUpdateDTO, Beer>(updateDTO,beer);

                _beerRepository.Update(beer);
                await _beerRepository.Save();

                var beerDto = _mapper.Map<BeerDTO>(beer);

                return beerDto;


            }
            return null;
        }



        public async Task<BeerDTO> Delete(int id)
        {
            var beer = await _beerRepository.GetById(id);

            if (beer != null)
            {
                var beerDto = _mapper.Map<BeerDTO>(beer);

                _beerRepository.Delete(beer);
                await _beerRepository.Save();


                return beerDto;
            }

            return null;

        }

        public bool Validate(BeerInsertDTO beerInsertDTO)
        {
            if (_beerRepository.Search(b=> b.Name == beerInsertDTO.Name).Count() > 0)
            {
                Errors.Add("No puede existir una cerveza con un nombre ya existente.");
                return false;
            }

            return true;
        }

        public bool Validate(BeerUpdateDTO beerUpdateDTO)
        {
            if (_beerRepository.Search(b => b.Name == beerUpdateDTO.Name && beerUpdateDTO.Id != b.BeerId).Count() > 0)
            {
                Errors.Add("No puede existir una cerveza con un nombre ya existente.");
                return false;
            }

            return true;
        }
    }
}

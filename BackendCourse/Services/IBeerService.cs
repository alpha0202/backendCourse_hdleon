using BackendCourse.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace BackendCourse.Services
{
    public interface IBeerService
    {

        Task<IEnumerable<BeerDTO>> Get();
        Task<BeerDTO> GetById(int id);
        Task<BeerDTO> Add(BeerInsertDTO beerInsertDTO);
        Task<BeerDTO> Update(int id, BeerUpdateDTO updateDTO);
        Task<BeerDTO> Delete(int id);

    }
}

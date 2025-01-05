using BackendCourse.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace BackendCourse.Services
{
    public interface ICommonService<T,TI,TU>
    {

        Task<IEnumerable<T>> Get();
        Task<T> GetById(int id);
        Task<T> Add(TI beerInsertDTO);
        Task<T> Update(int id, TU updateDTO);
        Task<T> Delete(int id);

    }
}

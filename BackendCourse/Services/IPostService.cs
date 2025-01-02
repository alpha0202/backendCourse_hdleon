using BackendCourse.DTOs;

namespace BackendCourse.Services
{
    public interface IPostService
    {

        public Task<IEnumerable<PostDTO>> Get();

    }
}

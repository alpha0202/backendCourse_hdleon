using BackendCourse.Models;
using Microsoft.EntityFrameworkCore;

namespace BackendCourse.Repository
{
    public class BeerRepository : IRepository<Beer>
    {
        private readonly StoreContext _storeContext;

        public BeerRepository(StoreContext storeContext)
        {
            _storeContext = storeContext;
        }

        public async Task<IEnumerable<Beer>> Get()
        => await _storeContext.Beers.ToListAsync();


        public async Task<Beer> GetById(int id)
         =>  await _storeContext.Beers.FindAsync(id);
        


        public async Task Add(Beer beer)
          => await _storeContext.Beers.AddAsync(beer);




        public void Update(Beer beer)
        {
            _storeContext.Beers.Attach(beer);
            _storeContext.Entry(beer).State = EntityState.Modified;
        }
        public void Delete(Beer beer)
            => _storeContext.Beers.Remove(beer);


        public async Task Save()
         => await _storeContext.SaveChangesAsync();


        public IEnumerable<Beer> Search(Func<Beer, bool> filter)
            => _storeContext.Beers.Where(filter).ToList(); 
    }
}

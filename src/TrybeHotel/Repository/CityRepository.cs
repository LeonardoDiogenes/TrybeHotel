using TrybeHotel.Models;
using TrybeHotel.Dto;

namespace TrybeHotel.Repository
{
    public class CityRepository : ICityRepository
    {
        protected readonly ITrybeHotelContext _context;
        public CityRepository(ITrybeHotelContext context)
        {
            _context = context;
        }

        public IEnumerable<CityDto> GetCities()
        {
            var cities = from city in _context.Cities
                         select new CityDto
                         {
                             CityId = city.CityId,
                             Name = city.Name,
                             State = city.State
                         };
            var cityList = cities.ToList();
            if (cityList.Count == 0)
                throw new Exception("No cities found");
            return cityList;
        }

        public CityDto AddCity(City city)
        {
            _context.Cities.Add(city);
            _context.SaveChanges();
            return new CityDto
            {
                CityId = city.CityId,
                Name = city.Name,
                State = city.State
            };
        }

        // 3. Desenvolva o endpoint PUT /city
        public CityDto UpdateCity(City city)
        {
            var cityToUpdate = _context.Cities.Find(city.CityId);
            if (cityToUpdate == null)
                throw new Exception("City not found");

            cityToUpdate!.Name = city.Name;
            cityToUpdate.State = city.State;
            _context.SaveChanges();
            
            return new CityDto
            {
                CityId = cityToUpdate.CityId,
                Name = cityToUpdate.Name,
                State = cityToUpdate.State
            };
        }

        public void DeleteCity(int id)
        {
            var city = _context.Cities.Find(id);
            if (city == null)
                throw new Exception("City not found");
                
            _context.Cities.Remove(city);
            _context.SaveChanges();
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WayToGo.Data;
using WayToGo.Models;

namespace WayToGo.Services
{
    public class CityService
    {
        private readonly Guid _userId;

        public CityService(Guid userId)
        {
            _userId = userId;
        }
        public bool CreateCity(CityCreate model)
        {
            var entity =
                new City()
                {
                    OwnerId = _userId,
                    Name = model.Name,
                    Longitude = model.Longitude,
                    Latitude = model.Latitude
                };

            using (var ctx = new ApplicationDbContext())
            {
                ctx.Cities.Add(entity);
                return ctx.SaveChanges() == 1;
            }
        }
        public IEnumerable<CityListItem> GetCities()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                        .Cities
                        .Where(e => e.OwnerId == _userId)
                        .Select(
                            e =>
                                new CityListItem
                                {
                                    CityId = e.CityId,
                                    Name = e.Name,
                                    Longitude = e.Longitude,
                                    Latitude = e.Latitude
                                }
                        );

                return query.ToArray();
            }
        }
        public CityDetail GetCityById(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Cities
                        .Single(e => e.CityId == id && e.OwnerId == _userId);
                return
                    new CityDetail
                    {
                        CityId = entity.CityId,
                        Name = entity.Name,
                        Latitude = entity.Latitude,
                        Longitude = entity.Longitude
                    };
            }
        }

        public bool UpdateCity(CityEdit model)
        {
            using(var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Cities
                        .Single(e => e.CityId == model.CityId && e.OwnerId == _userId);

                entity.Name = model.Name;
                entity.Longitude = model.Longitude;
                entity.Latitude = model.Latitude;

                return ctx.SaveChanges() == 1;
            }
        }
        public bool DeleteCity(int cityId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Cities
                        .Single(e => e.CityId == cityId && e.OwnerId == _userId);

                ctx.Cities.Remove(entity);

                return ctx.SaveChanges() == 1;
            }
        }
    }
}

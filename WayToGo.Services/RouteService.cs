using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WayToGo.Data;
using WayToGo.Models;
using WayToGo.Models.Routes;

namespace WayToGo.Services
{
    public class RouteService
    {
        private readonly Guid _userId;

        public RouteService(Guid userId)
        {
            _userId = userId;
        }

        public bool CreateRoute(RouteCreate model)
        {
            var entity =
                new Route()
                {
                    OwnerId = _userId,
                    Name = model.Name,
                    Origin = model.Origin,
                    Destination = model.Destination
                };

            using (var ctx = new ApplicationDbContext())
            {
                ctx.Routes.Add(entity);
                return ctx.SaveChanges() == 1;
            }
        }
        public IEnumerable<RouteListItem> GetRoutes()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                        .Routes
                        .Where(e => e.OwnerId == _userId)
                        .Select(
                            e =>
                            new RouteListItem
                            {
                                RouteId = e.RouteId,
                                Name = e.Name,
                                Origin = e.Origin,
                                Destination = e.Destination
                            }
                            );
                return query.ToArray();
            }
        }
        public RouteDetail GetRouteById(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Routes
                        .Single(e => e.RouteId == id && e.OwnerId == _userId);
                return
                    new RouteDetail
                    {
                        RouteId = entity.RouteId,
                        Name = entity.Name,
                        Origin = entity.Origin,
                        Destination = entity.Destination
                    };
            }
        }

        public bool UpdateRoute(RouteEdit model)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Routes
                        .Single(e => e.RouteId == model.RouteId && e.OwnerId == _userId);

                entity.Name = model.Name;
                entity.Origin = model.Origin;
                entity.Destination = model.Destination;

                return ctx.SaveChanges() == 1;
            }
        }
        public bool DeleteRoute(int routeId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Routes
                        .Single(e => e.RouteId == routeId && e.OwnerId == _userId);

                ctx.Routes.Remove(entity);

                return ctx.SaveChanges() == 1;
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WayToGo.Data;
using WayToGo.Models.Transports;

namespace WayToGo.Services
{
    public class TransportService
    {
        private readonly Guid _userId;

        public TransportService(Guid userId)
        {
            _userId = userId;
        }

        public bool CreateTransport(TransportCreate model)
        {
            var entity =
                new Transport()
                {
                    OwnerId = _userId,
                    Name = model.Name,
                    Description = model.Description,
                    Speed = model.Speed,
                    Cost = model.Cost
                };

            using (var ctx = new ApplicationDbContext())
            {
                ctx.Transports.Add(entity);
                return ctx.SaveChanges() == 1;
            }
        }
        public IEnumerable<TransportListItem> GetTransports()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                        .Transports
                        .Where(e => e.OwnerId == _userId)
                        .Select(
                            e =>
                            new TransportListItem
                            {
                                TransportId = e.TransportId,
                                Name = e.Name,
                                Description = e.Description,
                                Speed = e.Speed,
                                Cost = e.Cost
                            }
                            );
                return query.ToArray();
            }
        }
        public TransportDetail GetTransportById(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Transports
                        .Single(e => e.TransportId == id && e.OwnerId == _userId);
                return
                    new TransportDetail
                    {
                        TransportId = entity.TransportId,
                        Name = entity.Name,
                        Description = entity.Description,
                        Speed = entity.Speed,
                        Cost = entity.Cost
                    };
            }
        }

        public bool UpdateTransport(TransportEdit model)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Transports
                        .Single(e => e.TransportId == model.TransportId && e.OwnerId == _userId);

                entity.Name = model.Name;
                entity.Description = model.Description;
                entity.Speed = model.Speed;
                entity.Cost = model.Cost;

                return ctx.SaveChanges() == 1;
            }
        }
        public bool DeleteTransport(int transportId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Transports
                        .Single(e => e.TransportId == transportId && e.OwnerId == _userId);

                ctx.Transports.Remove(entity);

                return ctx.SaveChanges() == 1;
            }
        }
    }
}

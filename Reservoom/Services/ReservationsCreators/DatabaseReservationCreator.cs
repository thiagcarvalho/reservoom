using Reservoom.DbContexts;
using Reservoom.DTOs;
using Reservoom.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reservoom.Services.ReservationsCreators
{
    public class DatabaseReservationCreator : IReservationCreator
    {
        private readonly ReservoomDbContextFactory _dbContextFactory;

        public DatabaseReservationCreator(ReservoomDbContextFactory dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }

        public async Task CreateReservation(Reservation reservation)
        {
            using (ReservoomDbContext context = _dbContextFactory.CreateDbContext())
            {
                ReservationDTO reservationDTO = new ReservationDTO
                {
                    Id = Guid.NewGuid(),
                    FloorNumber = reservation.RoomID.FloorNumber,
                    RoomNumber = reservation.RoomID.RoomNumber,
                    StartDate = reservation.StartDate,
                    EndDate = reservation.EndDate,
                    Username = reservation.Username
                };

                context.Reservations.Add(reservationDTO);
                await context.SaveChangesAsync();
            }
        }
    }
}

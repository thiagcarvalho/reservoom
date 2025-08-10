using Microsoft.EntityFrameworkCore;
using Reservoom.DbContexts;
using Reservoom.DTOs;
using Reservoom.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reservoom.Services.ReservationProviders
{
    public class DataBaseReservationProvider : IReservationProvider
    {
        private readonly ReservoomDbContextFactory _dbContextFactory;

        public DataBaseReservationProvider(ReservoomDbContextFactory dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }

        public async Task<IEnumerable<Reservation>> GetAllReservations()
        {
            using(ReservoomDbContext context = _dbContextFactory.CreateDbContext())
            {
                IEnumerable<ReservationDTO> reservationDTOs =  await context.Reservations.ToListAsync();

                await Task.Delay(2000);

                return reservationDTOs.Select(reservation => new Reservation(
                    new RoomID(reservation.FloorNumber, reservation.RoomNumber),
                    reservation.StartDate,
                    reservation.EndDate,
                    reservation.Username));
            }
        }
    }
}

using Microsoft.EntityFrameworkCore;
using Reservoom.DbContexts;
using Reservoom.DTOs;
using Reservoom.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reservoom.Services.ReservationConflictValidators
{
    public class DatabaseReservationConflictValidator : IReservationConflictValidator
    {
        private readonly ReservoomDbContextFactory _dbContextFactory;

        public DatabaseReservationConflictValidator(ReservoomDbContextFactory dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }
        public async Task<Reservation?> GetConflictReservation(Reservation reservation)
        {
            using (ReservoomDbContext context = _dbContextFactory.CreateDbContext())
            {
                ReservationDTO? reservationDTO = await context.Reservations
                                                .Where(r => r.FloorNumber == reservation.RoomID.FloorNumber &&
                                                        r.RoomNumber == reservation.RoomID.RoomNumber &&
                                                        r.StartDate < reservation.EndDate &&
                                                        r.EndDate > reservation.StartDate)
                                                .FirstOrDefaultAsync();

                if (reservationDTO == null)
                    return null;

                return ToReservation(reservationDTO);
            }
        }

        private static Reservation ToReservation(ReservationDTO dto)
        {
            return new Reservation(
                new RoomID(dto.FloorNumber, dto.RoomNumber),
                dto.StartDate,
                dto.EndDate,
                dto.Username);
        }
    }
}

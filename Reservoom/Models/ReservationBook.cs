using Reservoom.Exceptions;
using Reservoom.Services.ReservationConflictValidators;
using Reservoom.Services.ReservationProviders;
using Reservoom.Services.ReservationsCreators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reservoom.Models
{
    public class ReservationBook
    {
        private readonly IReservationProvider _reservationProvider;
        private readonly IReservationCreator _reservationCreator;
        private readonly IReservationConflictValidator _reservationConflicValidator;

        public ReservationBook(IReservationProvider reservationProvider,
            IReservationCreator reservationCreator,
            IReservationConflictValidator reservationConflictValidator)
        {
            _reservationProvider = reservationProvider;
            _reservationCreator = reservationCreator;
            _reservationConflicValidator = reservationConflictValidator;
        }

        public async Task<IEnumerable<Reservation>> GetReservationsForUser(string username)
        {
            var allReservations = await _reservationProvider.GetAllReservations();
            return allReservations.Where(r => r.Username == username);
        }

        public async Task<IEnumerable<Reservation>> GetAllReservations()
        {
            return await _reservationProvider.GetAllReservations();
        }

        public async Task AddReservation(Reservation reservation)
        {
            Reservation conflictingReservation = await _reservationConflicValidator.GetConflictReservation(reservation);

            if(conflictingReservation != null)
            {
                throw new ReservationConflictException(conflictingReservation, reservation);
            }

            await _reservationCreator.CreateReservation(reservation);
        }
    }
}

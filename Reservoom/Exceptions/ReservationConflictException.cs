using Reservoom.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reservoom.Exceptions
{
    public class ReservationConflictException : Exception
    {
        public Reservation ExistingReservation { get; }
        public Reservation NewReservation { get; }

        public ReservationConflictException(Reservation existingReservation, Reservation newReservation)
        {
            ExistingReservation = existingReservation;
            NewReservation = newReservation;
        }

        public ReservationConflictException(string message, Reservation existingReservation, Reservation newReservation)
            : base(message)
        {
            ExistingReservation = existingReservation;
            NewReservation = newReservation;
        }
    }
}

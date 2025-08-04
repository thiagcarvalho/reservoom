using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reservoom.Models
{
    public class Reservation
    {
        public string Username { get; }
        public RoomID RoomID { get; }
        public DateTime StartDate { get; }
        public DateTime EndDate { get; }
        public TimeSpan Length => EndDate - StartDate;

        public Reservation(RoomID roomID, DateTime startDate, DateTime endDate, string userName)
        {
            RoomID = roomID;
            StartDate = startDate;
            EndDate = endDate;
            Username = userName;
        }

        internal bool Conflicts(Reservation reservation)
        {
            if(reservation.RoomID != RoomID)
            {
                return false;
            }

            return reservation.StartDate < EndDate || reservation.EndDate > StartDate;
        }
    }
}

using Reservoom.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reservoom.ViewModel
{
    public class ReservationViewModel : ViewModelBase
    {
        public string Username { get; } = string.Empty;
        public string RoomID { get; } = string.Empty;
        public DateTime StartDate { get; }
        public DateTime EndDate { get; }

        public ReservationViewModel(Reservation reservation)
        {
            Username = reservation.Username;
            RoomID = reservation.RoomID.ToString();
            StartDate = reservation.StartDate;
            EndDate = reservation.EndDate;
        }
    }
}

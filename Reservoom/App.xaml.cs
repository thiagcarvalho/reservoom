using Reservoom.Exceptions;
using Reservoom.Models;
using System.Configuration;
using System.Data;
using System.Windows;

namespace Reservoom
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            Hotel hotel = new Hotel("Copacabana Palace");

            try
            {
                hotel.MakeReservation(new Reservation(
                    new RoomID(1, 5),
                    new DateTime(2023, 10, 1),
                    new DateTime(2023, 10, 5),
                    "Thiago"
                ));

                hotel.MakeReservation(new Reservation(
                    new RoomID(5, 5),
                    new DateTime(2023, 10, 1),
                    new DateTime(2023, 10, 5),
                    "Thiago"
                ));

            }
            catch (ReservationConflictException ex)
            {

            }

            IEnumerable<Reservation> reservations = hotel.GetReservationsForUser("Thiago");

            base.OnStartup(e);
            // Initialize any required services or configurations here
        }
    }

}

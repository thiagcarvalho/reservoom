using Reservoom.Models;
using Reservoom.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Reservoom.Commands
{
    public class LoadReservationsCommand : AsyncCommandBase
    {
        private readonly Hotel _hotel;
        private readonly ReservationListingViewModel _reservationListingViewModel;

        public LoadReservationsCommand(Hotel hotel, ReservationListingViewModel reservationListingViewModel)
        {
            _hotel = hotel;
            _reservationListingViewModel = reservationListingViewModel;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            try
            {
                IEnumerable<Reservation> reservations = await _hotel.GetAllReservations();

                _reservationListingViewModel.UpdateReservation(reservations);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to load reservation", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}

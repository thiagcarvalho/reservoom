using Reservoom.Models;
using Reservoom.Stores;
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
        private readonly HotelStore _hotelStore;
        private readonly ReservationListingViewModel _reservationListingViewModel;

        public LoadReservationsCommand(HotelStore hotelStore, ReservationListingViewModel reservationListingViewModel)
        {
            _hotelStore = hotelStore;
            _reservationListingViewModel = reservationListingViewModel;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            _reservationListingViewModel.IsLoading = true;

            try
            {
                await _hotelStore.Load();

                _reservationListingViewModel.UpdateReservation(_hotelStore.Reservations);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to load reservation", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            _reservationListingViewModel.IsLoading = false;
        }
    }
}

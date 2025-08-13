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
            _reservationListingViewModel.ErrorMessage = string.Empty;
            _reservationListingViewModel.IsLoading = true;

            try
            {
                //throw new Exception("Simulated exception for testing purposes");

                await _hotelStore.Load();

                _reservationListingViewModel.UpdateReservation(_hotelStore.Reservations);
            }
            catch (Exception ex)
            {
                _reservationListingViewModel.ErrorMessage = $"An error occurred while loading reservations: {ex.Message}";
            }

            _reservationListingViewModel.IsLoading = false;
        }
    }
}

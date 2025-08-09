using Reservoom.Commands;
using Reservoom.Models;
using Reservoom.Services;
using Reservoom.Stores;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Reservoom.ViewModel
{
    public class ReservationListingViewModel : ViewModelBase
    {
        private readonly ObservableCollection<ReservationViewModel> _reservations;

        public IEnumerable<ReservationViewModel> Reservations => _reservations;

        public ICommand LoadReservationsCommand { get; }
        public ICommand MakeReservationCommand { get; }

        public ReservationListingViewModel(HotelStore hotelStore, NavigationService makeReservationNavigationService)
        {
            _reservations = new ObservableCollection<ReservationViewModel>();
            MakeReservationCommand = new NavigateCommand(makeReservationNavigationService);
            LoadReservationsCommand = new LoadReservationsCommand(hotelStore, this);

        }

        public override void Dispose()
        {
            base.Dispose();
            foreach (var reservation in _reservations)
            {
                reservation.Dispose();
            }
        }

        public static ReservationListingViewModel LoadViewModel(HotelStore hotelStore, NavigationService makeReservationNavigationService)
        {
            ReservationListingViewModel viewModel = new ReservationListingViewModel(hotelStore, makeReservationNavigationService);
            viewModel.LoadReservationsCommand.Execute(null);
            return viewModel;
        }


        public void UpdateReservation(IEnumerable<Reservation> reservations)
        {
            _reservations.Clear();

            if (reservations == null || !reservations.Any())
            {
                return;
            }

            foreach (var reservation in reservations)
            {
                ReservationViewModel reservationViewModel = new ReservationViewModel(reservation);
                _reservations.Add(reservationViewModel);
            }
        }
    }
}

using Reservoom.Exceptions;
using Reservoom.Models;
using Reservoom.Services;
using Reservoom.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Reservoom.Commands
{
    public class MakeReservationCommand : CommandBase
    {
        private readonly MakeReservationViewModel _makeReservationViewModel;
        private readonly NavigationService _reservationViewNavigationService;
        private readonly Hotel _hotel;

        public MakeReservationCommand(MakeReservationViewModel makeReservationViewModel, Hotel hotel, NavigationService reservationViewNavigationService)
        {
            _hotel = hotel;
            _makeReservationViewModel = makeReservationViewModel;
            _reservationViewNavigationService = reservationViewNavigationService;

            /**/
            _makeReservationViewModel.PropertyChanged += OnViewModelPropertyChanged;
        }

        private void OnViewModelPropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            if(e.PropertyName == nameof(MakeReservationViewModel.UserName))
            {
                OnCanExecuteChanged();
            }
        }

        public override bool CanExecute(object? parameter)
        {
            return !string.IsNullOrEmpty(_makeReservationViewModel.UserName) &&
                   base.CanExecute(parameter);
        }
        public override void Execute(object? parameter)
        {
            Reservation reservation = new Reservation(
                new RoomID(_makeReservationViewModel.FloorNumber, _makeReservationViewModel.RoomNumber),
                _makeReservationViewModel.StartDate,
                _makeReservationViewModel.EndDate,
                _makeReservationViewModel.UserName);

            try
            {
                _hotel.MakeReservation(reservation);
                MessageBox.Show("Successfully reserved room", "Success", MessageBoxButton.OK, MessageBoxImage.Information);

                _reservationViewNavigationService.Navigate();
            }
            catch (ReservationConflictException)
            {
                MessageBox.Show("This room is already taken", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }
    }
}

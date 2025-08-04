using Reservoom.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        public ICommand MakeReservationCommand { get; }

        public ReservationListingViewModel()
        {
            _reservations = new ObservableCollection<ReservationViewModel>();

            _reservations.Add(new ReservationViewModel(new Reservation(
                    new RoomID(1, 5),
                    new DateTime(2023, 10, 1),
                    new DateTime(2023, 10, 5),
                    "Thiago"
              )));

            _reservations.Add(new ReservationViewModel(new Reservation(
                    new RoomID(1, 5),
                    new DateTime(2023, 11, 1),
                    new DateTime(2023, 11, 5),
                    "Luis"
              )));

            _reservations.Add(new ReservationViewModel(new Reservation(
                    new RoomID(1, 5),
                    new DateTime(2023, 12, 1),
                    new DateTime(2023, 12, 5),
                    "Freitas"
              )));
        }
        

    }
}

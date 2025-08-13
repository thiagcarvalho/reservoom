using Reservoom.Commands;
using Reservoom.Models;
using Reservoom.Services;
using Reservoom.Stores;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Reservoom.ViewModel
{
    public class MakeReservationViewModel : ViewModelBase, INotifyDataErrorInfo
    {
        private string _userName = string.Empty;
        private int _roomNumber;
        private int _floorNumber;
        private DateTime _startDate = new DateTime(2025, 08, 04);
        private DateTime _endDate = new DateTime(2025, 08, 08);
        private readonly Dictionary<string, List<string>> _propertyNameToErrorsDictionary;

        public MakeReservationViewModel(HotelStore hotelStore, NavigationService reservationViewNavigationService)
        {
            SubmitCommand = new MakeReservationCommand(this, hotelStore, reservationViewNavigationService);
            CancelCommand = new NavigateCommand(reservationViewNavigationService);
            _propertyNameToErrorsDictionary = new Dictionary<string, List<string>>();
        }

        public string UserName
        {
            get => _userName;
            set
            {
                _userName = value;
                OnPropertyChanged(nameof(UserName));
            }
        }

        public int RoomNumber
        {
            get => _roomNumber;
            set
            {
                _roomNumber = value;
                OnPropertyChanged(nameof(RoomNumber));
            }
        }

        public int FloorNumber
        {
            get => _floorNumber;
            set
            {
                _floorNumber = value;
                OnPropertyChanged(nameof(FloorNumber));
            }
        }

        public DateTime StartDate
        {
            get => _startDate;
            set
            {
                _startDate = value;
                OnPropertyChanged(nameof(StartDate));

                ClearErrors(nameof(StartDate));
                ClearErrors(nameof(EndDate));

                if (EndDate < StartDate)
                {
                    AddError("Start date cannot be after end date.", nameof(StartDate));
                }
            }
        }

        public DateTime EndDate
        {   
            get => _endDate;
            set
            {
                _endDate = value;
                OnPropertyChanged(nameof(EndDate));

                ClearErrors(nameof(StartDate));
                ClearErrors(nameof(EndDate));

                if (EndDate < StartDate)
                {
                    AddError("End date cannot be before start date.", nameof(EndDate));
                }
            }
        }

        private void OnErrorsChanged(string propertyName)
        {
            ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
        }

        private void AddError(string errorMessage, string propertyName)
        {
            if (!_propertyNameToErrorsDictionary.ContainsKey(propertyName))
            {
                _propertyNameToErrorsDictionary[propertyName] = new List<string>();
            }

            _propertyNameToErrorsDictionary[propertyName].Add(errorMessage);
            
            OnErrorsChanged(propertyName);

        }

        private void ClearErrors(string v)
        {
            _propertyNameToErrorsDictionary.Remove(v);
            OnErrorsChanged(v);
        }

        public ICommand SubmitCommand { get; }
        public ICommand CancelCommand { get; }

        public bool HasErrors => _propertyNameToErrorsDictionary.Any();

        public event EventHandler<DataErrorsChangedEventArgs>? ErrorsChanged;

        public IEnumerable GetErrors(string? propertyName)
        {
            if (propertyName is null)
            {
                return Enumerable.Empty<string>();
            }
            return _propertyNameToErrorsDictionary.TryGetValue(propertyName, out var errors) ? errors : Enumerable.Empty<string>();
        }
    }
}

using Reservoom.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reservoom.Stores
{
    public class HotelStore
    {
        private readonly Hotel _hotel;
        private readonly List<Reservation> _reservations;
        private Lazy<Task> _initializeLazy;

        public HotelStore(Hotel hotel)
        {
            _hotel = hotel;
            _reservations = new List<Reservation>();
            _initializeLazy = new Lazy<Task>(Initialize);
        }

        public IEnumerable<Reservation> Reservations => _reservations;

        public async Task Load()
        {
            try
            {
                await _initializeLazy.Value;
            }
            catch (Exception ex)
            {
                _initializeLazy = new Lazy<Task>(Initialize);
                throw;
            }
        }

        public async Task MakeReservation(Reservation reservation)
        {
            await _hotel.MakeReservation(reservation);

            _reservations.Add(reservation);
        }

        public async Task Initialize()
        {
            IEnumerable<Reservation> reservations = await _hotel.GetAllReservations();

            _reservations.Clear();
            _reservations.AddRange(reservations);
        }
    }
}

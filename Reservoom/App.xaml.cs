using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Reservoom.DbContexts;
using Reservoom.Exceptions;
using Reservoom.Models;
using Reservoom.Services.ReservationConflictValidators;
using Reservoom.Services.ReservationProviders;
using Reservoom.Services.ReservationsCreators;
using Reservoom.Stores;
using Reservoom.ViewModel;
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
        private const string CONNECTION_STRING = "Data Source=reservoom.db";
        private readonly Hotel _hotel;
        private readonly HotelStore _hotelStore;
        private readonly NavigationStore _navigationStore;
        private ReservoomDbContextFactory _reservoomDbContextFactory;

        public App()
        {
            _reservoomDbContextFactory = new ReservoomDbContextFactory(CONNECTION_STRING);
            Services.ReservationProviders.IReservationProvider reservationProvider = new DataBaseReservationProvider(_reservoomDbContextFactory);
            Services.ReservationsCreators.IReservationCreator reservationCreator = new DatabaseReservationCreator(_reservoomDbContextFactory);
            Services.ReservationConflictValidators.IReservationConflictValidator reservationConflictValidator = new DatabaseReservationConflictValidator(_reservoomDbContextFactory);

            _hotel = new Hotel("Copacabana Palace", new ReservationBook(reservationProvider, reservationCreator, reservationConflictValidator));
            _hotelStore = new HotelStore(_hotel);
            _navigationStore = new NavigationStore();
        }
        protected override void OnStartup(StartupEventArgs e)
        {
            DbContextOptions options = new DbContextOptionsBuilder().UseSqlite(CONNECTION_STRING).Options;
            using (ReservoomDbContext dbContext = _reservoomDbContextFactory.CreateDbContext())
            {
                dbContext.Database.Migrate();
            };


            _navigationStore.CurrentViewModel = CreateReservationListingViewModel();

            MainWindow = new MainWindow()
            {
                DataContext = new MainViewModel(_navigationStore)
            };
            MainWindow.Show();

            base.OnStartup(e);
            // Initialize any required services or configurations here
        }

        private MakeReservationViewModel CreateMakeReservationViewModel()
        {
            return new MakeReservationViewModel(_hotelStore, new Services.NavigationService(_navigationStore, CreateReservationListingViewModel));
        }

        private ReservationListingViewModel CreateReservationListingViewModel()
        {
            return ReservationListingViewModel.LoadViewModel(_hotelStore, new Services.NavigationService(_navigationStore, CreateMakeReservationViewModel));
        }
    }

}

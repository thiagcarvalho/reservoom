using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Reservoom.DbContexts;
using Reservoom.Exceptions;
using Reservoom.Models;
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
        private readonly NavigationStore _navigationStore;

        public App()
        {
            _hotel = new Hotel("Copacabana Palace");
            _navigationStore = new NavigationStore();
        }
        protected override void OnStartup(StartupEventArgs e)
        {
            DbContextOptions options = new DbContextOptionsBuilder().UseSqlite(CONNECTION_STRING).Options;
            ReservoomDbContext dbContext = new ReservoomDbContext(options);

            dbContext.Database.Migrate();

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
            return new MakeReservationViewModel(_hotel, new Services.NavigationService(_navigationStore, CreateReservationListingViewModel));
        }

        private ReservationListingViewModel CreateReservationListingViewModel()
        {
            return new ReservationListingViewModel(_hotel, new Services.NavigationService(_navigationStore, CreateMakeReservationViewModel));
        }
    }

}

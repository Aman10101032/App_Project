using Plugin.BluetoothClassic.Abstractions;
using Xamarin.Essentials;
using System;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App_Project
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BluetoothPage : ContentPage
    {
        public BluetoothPage()
        {
            InitializeComponent();
            FillDevices();
        }

        private async void FillDevices()
        {
            var status = await Permissions.RequestAsync<Permissions.LocationWhenInUse>();
            if (status != PermissionStatus.Granted)
            {
                // Разрешение не было предоставлено
                return;
            }

            var adapter = DependencyService.Resolve<IBluetoothAdapter>();
            lvBoundedDevices.ItemsSource = adapter.BondedDevices;
        }

        private async void lvBoundedDevices_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var device = (BluetoothDeviceModel)e.SelectedItem;
            if (device != null)
            {
                await Navigation.PushAsync(new TransiverPage { BindingContext = device });
            }
            lvBoundedDevices.SelectedItem = null;
        }

        private void infoButton_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new InfoPage());
        }

        private void homeButton_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new ResultPage());
        }
    }
}

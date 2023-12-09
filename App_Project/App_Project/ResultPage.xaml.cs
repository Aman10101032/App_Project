using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App_Project
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ResultPage : ContentPage
    {
        public ResultPage() 
        {
            InitializeComponent();
        }

        private void bluetoothButton_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new BluetoothPage());
        }
        private void infoButton_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new InfoPage());
            string path = "C:\\Users\\Админ\\OneDrive\\Документы\\Phisyc_Project\\App\\Image\\bluetooth.png";
            bluetoothButton.Source = ImageSource.FromFile(path);

        }

        protected override bool OnBackButtonPressed()
        {
            // Ваша логика обработки кнопки "назад"
            return true; // если вернуть true, системное действие кнопки "назад" будет подавлено
        }
    }
}
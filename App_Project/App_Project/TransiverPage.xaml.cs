using Plugin.BluetoothClassic.Abstractions;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App_Project
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TransiverPage : ContentPage
    {
        public TransiverPage()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

#if __ANDROID__
            // Проверяем, включен ли Bluetooth при появлении страницы на Android
            bool isBluetoothEnabled = await DependencyService.Get<IBluetoothManager>().IsBluetoothEnabled();

            if (!isBluetoothEnabled)
            {
                // Если Bluetooth выключен, выводим предупреждение
                bool result = await DisplayAlert("Bluetooth Warning", "Bluetooth is not enabled. Do you want to enable it?", "Yes", "No");

                if (result)
                {
                    // Здесь вы можете добавить код для включения Bluetooth (если возможно в вашем приложении)
                }
                else
                {
                    // Здесь вы можете обработать случай, если пользователь не хочет включать Bluetooth
                }
            }
#endif
        }

        private async void btnReceive_Clicked(object sender, EventArgs e)
        {
            const int BufferSize = 4; // Установите размер буфера в соответствии с ожидаемым размером данных
            var adapter = DependencyService.Resolve<IBluetoothAdapter>();
            var device = (BluetoothDeviceModel)BindingContext;

            if (device != null)
            {
                var connection = adapter.CreateConnection(device);

                if (await connection.RetryConnectAsync(retriesCount: 2))
                {
                    byte[] buffer = new byte[BufferSize];
                    if (!(await connection.RetryReciveAsync(buffer, offset: 0, count: BufferSize)).Succeeded)
                    {
                        int receivedValue = BitConverter.ToInt32(buffer, 0); // Преобразование байтов в int

                        // Ваш код обработки переменной receivedValue (например, отображение на экране)
                        await DisplayAlert("Received Data", $"Received value: {receivedValue}", "OK");

                        // Обновление метки с полученным значением
                        voltLabel.Text = $"Voltage: {receivedValue}";
                    }
                    else
                    {
                        await DisplayAlert("Error!", "Can not receive data", "OK:(");
                    }
                }
                else
                {
                    await DisplayAlert("Error!", "Can not connect", "OK:(");
                }
            }
            else
            {
                // Обработка случая, если объект BluetoothDeviceModel равен null
            }
        }
    }
}

using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Runtime;
using Android.Bluetooth;
using Android.Content;

namespace App_Project.Droid
{
    [Activity(Label = "App_Project", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        // ...

        const int RequestBluetooth = 1;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            LoadApplication(new App());

            CheckBluetoothPermission();
        }

        private void CheckBluetoothPermission()
        {
            BluetoothAdapter bluetoothAdapter = BluetoothAdapter.DefaultAdapter;

            if (bluetoothAdapter == null || !bluetoothAdapter.IsEnabled)
            {
                // Bluetooth выключен или отсутствует
                BluetoothManager bluetoothManager = (BluetoothManager)GetSystemService(Context.BluetoothService);
                BluetoothAdapter adapter = bluetoothManager.Adapter;

                if (adapter == null || !adapter.IsEnabled)
                {
                    Intent enableBtIntent = new Intent(BluetoothAdapter.ActionRequestEnable);
                    StartActivityForResult(enableBtIntent, RequestBluetooth);
                }
            }
        }



        // Добавьте этот метод, чтобы обработать результат запроса разрешения Bluetooth
        protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);

            if (requestCode == RequestBluetooth)
            {
                if (resultCode == Result.Ok)
                {
                    // Bluetooth был включен
                }
                else
                {
                    // Пользователь отменил запрос на включение Bluetooth или произошла ошибка
                    // Можете добавить соответствующую обработку или уведомление здесь
                }
            }
        }

        // ...
    }
}

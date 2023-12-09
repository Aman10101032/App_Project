using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration;

namespace App_Project
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            // Скрываем стрелку назад


            // Отключаем жест "назад" для Android


            // Вызываем метод, который будет перенаправлять пользователя через 5 секунд
            Device.StartTimer(TimeSpan.FromSeconds(5), () =>
            {
                // Ваш код для перенаправления пользователя
                Navigation.PushAsync(new ResultPage());

                // Возвращаем false, чтобы таймер не повторялся
                return false;
            });
        }



    }
}
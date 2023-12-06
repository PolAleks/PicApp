using System;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PicApp.Pages
{
    public partial class LoginPage : ContentPage
    {
        public static string Pin { get; set; } = string.Empty;

        public LoginPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            
            if (Preferences.ContainsKey("pin"))
            {
                enterCodeMsg.Text = "Введите PIN-код для входа в приложение";
                LoginBtn.Text = "Войти";

                Pin = Preferences.Get("pin", string.Empty);
            }
        }

        /// <summary>
        /// Проверка на корректность ввода PIN кода
        /// </summary>
        private bool IsValidPinCode(string pin)
        {
            if (string.IsNullOrEmpty(pin))
            {
                DisplayAlert("Ошибка", "Вы не ввели значение Pin-кода!", "Повторить?");
                return false;
            }
            if (pin.Length != 4)
            {
                DisplayAlert("Ошибка", "Pin-код должен содержать 4 символа!", "Повторить?");
                pinEntry.Text = string.Empty;
                return false;
            }
            return true;
        }


        /// <summary>
        /// Переход в галерею с проверкой введенного PIN-кода
        /// </summary>
        private async void SavePinBtnClicked(object sender, EventArgs e)
        {
            var enteredPin = pinEntry.Text;
            if (IsValidPinCode(enteredPin))
            {
                // Запись PIN-кода при первичновм входе в приложение
                if(string.IsNullOrEmpty(Pin))
                {
                    Preferences.Set("pin", enteredPin);
                    await DisplayAlert("Внимание!", "PIN-код установлен.", "Войти в галерею");
                    pinEntry.Text = string.Empty;
                }
                else
                {
                    // Проверка PIN-кода с раннее записаным значением
                    if (!string.Equals(Pin, enteredPin, StringComparison.OrdinalIgnoreCase))
                    {
                        await DisplayAlert("Внимание!", $"Вы ввели неверный PIN-код! {Environment.NewLine}Повторите попытку...", "Ок");
                        pinEntry.Text = string.Empty;
                        return;
                    }
                }
                await Navigation.PushAsync(new GalleryPage());
            }
        }
    }
}
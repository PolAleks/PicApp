using System;
using System.IO;
using Xamarin.Forms;
using System.Collections.ObjectModel;
using PicApp.Models;

namespace PicApp.Pages
{
    public partial class GalleryPage : ContentPage
    {
        /// <summary>Коллекция изображений из галереи</summary>
        public ObservableCollection<ImageInfo> Images { get; set; }

        /// <summary>Ссылка на выбранное изображение</summary>
        ImageInfo _selectedImage;

        /// <summary>Путь к каталогу с изображениями</summary>
        const string PATH = "/storage/emulated/0/DCIM/Camera/";

        public GalleryPage()
        {
            InitializeComponent();

            Images = new ObservableCollection<ImageInfo>();
            
            string[] files = Directory.GetFiles(PATH);

            foreach (var file in files)
            {
                FileInfo imageFile = new FileInfo(file);
                Images.Add(new ImageInfo(imageFile.Name, file, imageFile.CreationTime));
            }

            imageList.ItemsSource = Images;
        }

        private async void OpenImageViewPage_Clicked(object sender, EventArgs e)
        {
            if (_selectedImage == null)
            {
                await DisplayAlert("Внимание!", "Вы не выбрали фото.", "Выбрать?");
                return;
            }

            await Navigation.PushAsync(new ImageViewPage(_selectedImage));
        }

        private async void DeleteImage_Clicked(object sender, EventArgs e)
        {
            if (_selectedImage == null)
            {
                await DisplayAlert("Внимание!", "Вы не выбрали фото.", "Выбрать?");
                return;
            }
            else
            {
                bool isDeletedImage = await DisplayAlert("Подтверждение действия", "Вы действительно хотите удалить выбранное изображение?", "Да", "Нет");
                if (isDeletedImage)
                {
                    // Удаляем изображение на носителе
                    try
                    {
                        File.Delete(_selectedImage.Path);
                    }
                    catch (Exception ex)
                    {
                        await DisplayAlert("Ошибка!", ex.Message, "Ok");
                    }

                    // Удаляем файл из коллекции
                    Images.Remove(_selectedImage);
                    _selectedImage = null;
                }
            }
        }

        private void ImageSelectedHandler(object sender, SelectedItemChangedEventArgs e)
        {
            _selectedImage = e.SelectedItem as ImageInfo;
        }

    }
    
}
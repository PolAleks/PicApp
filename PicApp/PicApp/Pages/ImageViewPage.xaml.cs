using PicApp.Models;
using Xamarin.Forms;

namespace PicApp.Pages
{
    public partial class ImageViewPage : ContentPage
    {
        public ImageViewPage(ImageInfo image)
        {
            this.BindingContext = image;

            InitializeComponent();
        }
    }
}
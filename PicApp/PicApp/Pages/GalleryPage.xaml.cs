using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Essentials;
using Xamarin.Forms.Xaml;

namespace PicApp.Pages
{
    public partial class GalleryPage : ContentPage
    {
        private string folderPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
        public GalleryPage()
        {
            InitializeComponent();
            string [] files = Directory.GetFiles(folderPath);
            fileLbl.Text = files.Length.ToString();

            
        }
    }
}
﻿using Plugin.Connectivity;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using VeloNSK.APIServise.Servise;
using VeloNSK.HelpClass.Connected;
using VeloNSK.HelpClass.Style;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace VeloNSK.View.Info
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class InfoUsersPage : ContentPage
    {
        links picture_lincs = new links();
        ConnectClass connectClass = new ConnectClass();
        HelpClass.Style.Size size_form = new HelpClass.Style.Size();
        HttpClient _client;
        public InfoUsersPage()
        {
            InitializeComponent();

            _client = new HttpClient();

            if (!connectClass.CheckConnection()) { Connect_ErrorAsync(); }//Проверка интернета при загрузке формы            
            CrossConnectivity.Current.ConnectivityChanged += (s, e) => { if (!connectClass.CheckConnection()) Connect_ErrorAsync(); };

            Fon.BackgroundImageSource = ImageSource.FromResource(picture_lincs.GetFon());
            Head_Image.Source = ImageSource.FromResource(picture_lincs.GetLogo());
            var pdfUrl = "http://90.189.158.10/folders/TrebovanieOfUsers.pdf";
            var googleUrl = "http://drive.google.com/viewerng/viewer?embedded=true&url=";
            if (Device.RuntimePlatform == Device.iOS)
            {
                InfoUser_WebView.Source = pdfUrl;
            }
            else if (Device.RuntimePlatform == Device.Android || Device.RuntimePlatform == Device.UWP)
            {
                InfoUser_WebView.Source = new UrlWebViewSource() { Url = googleUrl + pdfUrl };
            }
            Save_Button.Clicked += async (s, e) => { await DownloadAndSaveImage(pdfUrl); };
            Head_Button.Clicked += async (s, e) => { await Navigation.PopModalAsync(); };
        }
        public async Task Connect_ErrorAsync() { await Navigation.PopModalAsync(); } //Переход на страницу с ошибкой интернет соединения

        private async Task DownloadAndSaveImage(string get_path)
        {
            try
            {
                using (var response = await _client.GetStreamAsync(get_path))
                {
                    var filePath = await response.SaveToLocalFolderAsync($"Требования{DateTime.Now.ToString("dd.MM.yyyy_hh.mm.ss")}.jpg");
                    await DisplayAlert("", filePath, "Ok");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"DownloadAndSaveImage Exception: {ex}");
            }
        }

        private new void SizeChanged(object sender, EventArgs e)
        {
            if (size_form.GetHeightSize() < 600) Main_RowDefinition_One.Height = 0;
            if (size_form.GetHeightSize() > 600) Main_RowDefinition_One.Height = 60;
            if (size_form.GetHeightSize() < 600) Main_RowDefinition_Fore.Height = 0;
            if (size_form.GetHeightSize() > 600) Main_RowDefinition_Fore.Height = 60;
        }

    }
}
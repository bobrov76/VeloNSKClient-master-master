﻿using Plugin.Connectivity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VeloNSK.HelpClass.Connected;
using VeloNSK.HelpClass.Style;
using VeloNSK.View.Admin;
using VeloNSK.View.Admin.Participations;
using VeloNSK.View.Admin.Participations.Distanse;
using VeloNSK.View.Gateri;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace VeloNSK
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AdminPage : ContentPage
    {
        private links picture_lincs = new links();
        private ConnectClass connectClass = new ConnectClass();
        private Animations animations = new Animations();
        private bool animate;

        public AdminPage()
        {
            InitializeComponent();

            if (!connectClass.CheckConnection()) { Connect_ErrorAsync(); }//Проверка интернета при загрузке формы
            CrossConnectivity.Current.ConnectivityChanged += (s, e) => { if (!connectClass.CheckConnection()) Connect_ErrorAsync(); };

            Fon.BackgroundImageSource = ImageSource.FromResource(picture_lincs.GetFon()); //Устанавливаем фон
            Head_Image.Source = ImageSource.FromResource(picture_lincs.GetLogo());
            image_fon.Source = ImageSource.FromResource(picture_lincs.GetFon());

            Block_Button_Main_One.Clicked += async (s, e) =>
            {
                animations.Animations_Button(Block_Button_Main_One);
                await Task.Delay(700);
                await Navigation.PushModalAsync(new PartisipantMenuPage(), animate);
            };

            Block_Button_Main_Two.Clicked += async (s, e) =>
            {
                animations.Animations_Button(Block_Button_Main_Two);
                await Task.Delay(700);
                await Navigation.PushModalAsync(new UsersСontrolPage(), animate);
            };

            Block_Button_Main_Three.Clicked += async (s, e) =>
            {
                animations.Animations_Button(Block_Button_Main_Three);
                await Task.Delay(700);
                await Navigation.PushModalAsync(new PhotoGaleri(), animate);
            };

            Block_Button_Main_Fore.Clicked += async (s, e) =>
            {
                animations.Animations_Button(Block_Button_Main_Fore);
                await Task.Delay(700);
                await Navigation.PushModalAsync(new DistantionPage(), animate);
            };
            Head_Button.Clicked += async (s, e) =>
            {
                animations.Animations_Button(Head_Button);
                App.Current.Properties["token"] = "";
                await Task.Delay(700);
                await Navigation.PushModalAsync(new MainPage(), animate);
            };
        }

        public async Task Connect_ErrorAsync()
        {
            await Navigation.PushModalAsync(new ErrorConnectPage(), animate);
        }
    }
}
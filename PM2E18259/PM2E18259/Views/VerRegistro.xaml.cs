using Plugin.Media;
using PM2E18259.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PM2E18259.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class VerRegistro : ContentPage
    {
        String longitud, latitud;
        String pathImagen;
        public VerRegistro()
        {
            InitializeComponent();

            longitud = txtLon.Text;
            latitud = txtLat.Text;
        }

        private async void Eliminar_Clicked(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(lblCod.Text))
            {
                await DisplayAlert("Aviso", "No se puede actualizar si esta no es una vista", "Ok");
            }
            else
            {
                var emple = new Lugar
                {
                    id = Convert.ToInt32(lblCod.Text)
                };

                var resultado = await App.BaseDatos.EmpleadoBorrar(emple);
                if (resultado != 0)
                {
                    await DisplayAlert("Aviso", "Dato eliminado con exito", "Ok");
                    await Navigation.PopAsync();
                }
                else
                {
                    await DisplayAlert("Error", "Error al eliminar el dato", "Ok");
                }

            }
        }

        private async void Lista_Clicked(object sender, EventArgs e)
        {
            var newpage = new ListEmple();
            await Navigation.PushAsync(newpage);
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
        }

        private async void Enviar_Clicked(object sender, EventArgs e)
        {
            try
            {
                pathImagen = lblimage.Text;

                var sharephoto = pathImagen;
                await Share.RequestAsync(new ShareFileRequest
                {
                    Title = "Foto",
                    File = new ShareFile(sharephoto)
                });
            }
            catch(Exception ex)
            {
                await DisplayAlert("Error", ex.ToString(), "Ok");
            }
        }

    }
}
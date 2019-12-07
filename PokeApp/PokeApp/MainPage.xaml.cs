using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace PokeApp
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            ConsumirApilist();
        }

        //public void ConsumirApi()
        //{
        //    HttpClient cliente = new HttpClient();
        //    cliente.BaseAddress = new Uri("https://pokeapi.co");
        //    var request = cliente.GetAsync("/api/v2/pokemon/10").Result;
        //    if (request.IsSuccessStatusCode)
        //    {
        //        var respuestaJson = request.Content.ReadAsStringAsync().Result;
        //        var respuesta = JsonConvert.DeserializeObject<Pokemon>(respuestaJson);

        //        //nombre.Text = respuesta.name;
        //    }
            
        //}
        public void ConsumirApilist()
        {
            HttpClient cliente = new HttpClient();
            cliente.BaseAddress = new Uri("https://pokeapi.co");
            var request = cliente.GetAsync("/api/v2/pokemon").Result;
            if (request.IsSuccessStatusCode)
            {
                var respuestaJson = request.Content.ReadAsStringAsync().Result;
                var respuesta = JsonConvert.DeserializeObject<Respuesta>(respuestaJson);
                listaPokemon.ItemsSource = respuesta.results;
            }

        }

        private async void ListaPokemon_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            App.Current.Properties["url"] = e.SelectedItem;
            //pushModalAsync, inicia una new navigation page
            await Navigation.PushModalAsync(new NavigationPage(new Page1()));
        }
    }
}

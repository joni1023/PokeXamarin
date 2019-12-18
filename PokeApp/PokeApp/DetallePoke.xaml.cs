using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PokeApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DetallePoke : ContentPage
    {
        public DetallePoke(string nombre)
        {
            InitializeComponent();
            VerdetalleApi(nombre);
        }

        private async void VerdetalleApi(string nombre) 
        {
            HttpClient cliente = new HttpClient();
            cliente.BaseAddress = new Uri("https://pokeapi.co");
            var request = await cliente.GetAsync("/api/v2/pokemon/"+nombre+" ");
            if (request.IsSuccessStatusCode)
            {
                var respuestaJson = await request.Content.ReadAsStringAsync();
                var respuesta = JsonConvert.DeserializeObject<PokemonDetalle>(respuestaJson);
                //altura.Text =Convert.ToString(respuesta.weight);
                //Nombre.Text = respuesta.name;
                //Orden.Text =Convert.ToString(respuesta.id);
                //Img.Source = "https://raw.githubusercontent.com/PokeAPI/sprites/master/sprites/pokemon/"+Orden.Text+".png";
                var litaimagenes = new List<Item>
                {
                    new Item {
                    imagenuri= "https://upload.wikimedia.org/wikipedia/commons/thumb/f/f2/Xamarin-logo.svg/1200px-Xamarin-logo.svg.png"},
                    new Item {
                    imagenuri= "https://upload.wikimedia.org/wikipedia/commons/thumb/f/f2/Xamarin-logo.svg/1200px-Xamarin-logo.svg.png"},
                    new Item {
                    imagenuri= "https://upload.wikimedia.org/wikipedia/commons/thumb/f/f2/Xamarin-logo.svg/1200px-Xamarin-logo.svg.png"}
                };
                carrusel.ItemsSource = litaimagenes;
            }
        }
    }
    public class Item
    {
        public string imagenuri { get; set; }
    }
}
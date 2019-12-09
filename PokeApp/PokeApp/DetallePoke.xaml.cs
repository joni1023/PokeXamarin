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
                altura.Text =Convert.ToString(respuesta.weight);
                Nombre.Text = respuesta.name;
                Orden.Text =Convert.ToString(respuesta.order);
                Img.Source = "https://raw.githubusercontent.com/PokeAPI/sprites/master/sprites/pokemon/"+Orden.Text+".png";

            }
        }
    }
}
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

        private void VerdetalleApi(string nombre) 
        {
            HttpClient cliente = new HttpClient();
            cliente.BaseAddress = new Uri("https://pokeapi.co");
            var request = cliente.GetAsync("/api/v2/pokemon/"+nombre+" ").Result;
            if (request.IsSuccessStatusCode)
            {
                var respuestaJson = request.Content.ReadAsStringAsync().Result;
                var respuesta = JsonConvert.DeserializeObject<PokemonDetalle>(respuestaJson);
                altura.Text =Convert.ToString( respuesta.weight);
                Nombre.Text = respuesta.name;
                Orden.Text =Convert.ToString(respuesta.order);

            }
        }
    }
}
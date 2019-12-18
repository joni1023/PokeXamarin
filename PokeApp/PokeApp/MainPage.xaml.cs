using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        ObservableCollection<Pokemon> ListaMedia = new ObservableCollection<Pokemon>();
        string urinext = "";
        string nuevonumero = "";


        public MainPage()
        {
            InitializeComponent();
            ConsumirApilist();
        }

        public async void ConsumirApilist()
        {
            HttpClient cliente = new HttpClient();
            cliente.BaseAddress = new Uri("https://pokeapi.co");
            var request = await cliente.GetAsync("/api/v2/pokemon");
            if (request.IsSuccessStatusCode)
            {
                var respuestaJson = await request.Content.ReadAsStringAsync();
                var respuesta = JsonConvert.DeserializeObject<Respuesta>(respuestaJson);
                foreach(Pokemon p in respuesta.results)
                {
                    p.image = BuscarImagen(p.url);
                    ListaMedia.Add(p);
                }
                urinext = respuesta.next;
                listaPokemon.ItemsSource = ListaMedia;
            }

        }
        

        private async void ListaPokemon_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var select = e.SelectedItem as Pokemon;
            await Navigation.PushAsync(new DetallePoke(select.name));
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            HttpClient cliente = new HttpClient();
            cliente.BaseAddress = new Uri("https://pokeapi.co");
            string urlnext = urinext;
            string[] partir = urlnext.Split('?');
            string nuevanext = partir[1];
            var request = await cliente.GetAsync("/api/v2/pokemon?" + nuevanext + "");
            if (request.IsSuccessStatusCode)
            {
                var respuestaJson = await request.Content.ReadAsStringAsync();
                var respuesta = JsonConvert.DeserializeObject<Respuesta>(respuestaJson);
                foreach (Pokemon p in respuesta.results)
                {
                    p.image = BuscarImagen(p.url);
                    ListaMedia.Add(p);
                }
                urinext = respuesta.next;
                listaPokemon.ItemsSource = ListaMedia;
                
            }
        }

        public string BuscarImagen(string uripokemon)
        {
            string nuevonumero = "";
            string[] partiruri = uripokemon.Split('/');
            string orden = partiruri[6];
            int numero = orden.Count();
            switch (numero)
            {
                case 1: nuevonumero = "00" + orden + "";
                    break;
                case 2: nuevonumero = "0" + orden + "";
                    break;
                default: nuevonumero = orden;
                    break;
            }
            
            return "https://assets.pokemon.com/assets/cms2/img/pokedex/detail/" + nuevonumero+ ".png";

        }

        

        private async void SearchButtonPressed(object sender, EventArgs e)
        {
            ObservableCollection<Pokemon> nuevalist = new ObservableCollection<Pokemon>();            //string palabraB = "pi";
            string[] resultB = { "pikachu", "pichu" };
            //busca las palabras iguales
            HttpClient miclient = new HttpClient();
            miclient.BaseAddress = new Uri("https://pokeapi.co");
            foreach (string p in resultB)
            {
                var request = await miclient.GetAsync("/api/v2/pokemon/" + p + " ");
                var respuestaJson = await request.Content.ReadAsStringAsync();
                var convert = JsonConvert.DeserializeObject<Pokemon>(respuestaJson);
                convert.image = convert.sprites.front_default;
                nuevalist.Add(convert);
            }

            listaPokemon.ItemsSource = nuevalist;
        }

        private async void BarSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            ObservableCollection<Pokemon> nuevalist = new ObservableCollection<Pokemon>();            //string palabraB = "pi";
            string miletra = e.NewTextValue;
            if (miletra.Equals(""))
            {
                ConsumirApilist();
            }
            else
            {
                List<string> resultB = ServicioPokemon.BuscarNombresPokemon(miletra);
                //busca las palabras iguales
                HttpClient miclient = new HttpClient();
                miclient.BaseAddress = new Uri("https://pokeapi.co");
                foreach (string p in resultB)
                {
                    var request  = await miclient.GetAsync("/api/v2/pokemon/" + p + " ");
                    if (request.IsSuccessStatusCode) {
                        var respuestaJson = await request.Content.ReadAsStringAsync();
                        var convert = JsonConvert.DeserializeObject<Pokemon>(respuestaJson);
                        convert.image = BuscarImagen("https://pokeapi.co/api/v2/pokemon/" + convert.id + "");
                        nuevalist.Add(convert);
                    }
                        
                }

                listaPokemon.ItemsSource = nuevalist;
            }
        }
    }
}

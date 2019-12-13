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
                    ListaMedia.Add(p);
                }
                urinext = respuesta.next;
                listaPokemon.ItemsSource = ListaMedia;
            }

        }
        

        private async void ListaPokemon_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var select = e.SelectedItem as Pokemon;
            await Navigation.PushModalAsync(new NavigationPage(new DetallePoke(select.name)));
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
                    ListaMedia.Add(p);
                }
                urinext = respuesta.next;
                listaPokemon.ItemsSource = ListaMedia;
            }
        }
    }
}

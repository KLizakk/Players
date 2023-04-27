using Players.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using static Players.Model.PlayersModel;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;



namespace Players
{
    public class Funkcje : MainWindow 
    {
        public async void WczytajDane()
        {
            string WprowadzoneImie = WprowadzanieImienia.Text;
            var client = new HttpClient();
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri($"https://api-nba-v1.p.rapidapi.com/players?search={WprowadzoneImie}"),
                Headers =
    {
        { "X-RapidAPI-Key", "82e608ca0amshfb8be84d382c693p152ebejsn0ed3121d87c8" },
        { "X-RapidAPI-Host", "api-nba-v1.p.rapidapi.com" },
    },
            };
            using (var response = await client.SendAsync(request))
            {

                response.EnsureSuccessStatusCode();



                var body = await response.Content.ReadAsStringAsync();
                if (string.IsNullOrEmpty(body) == false)
                {
                   Testowa.Items.Clear();

                    var Wczytywanie = JsonConvert.DeserializeObject<Root>(body);

                    int LiczbaZawodników = Wczytywanie.response.Count();

                    // Dodajemy zawodników do tabeli
                    for (int i = 0; i < LiczbaZawodników; i++)
                    {
                        //tworzenie Zawodnika lepiej 
                        ZAWODNIK z = new ZAWODNIK();
                        z.firstname = Wczytywanie.response[i].lastname;
                        z.lastname = Wczytywanie.response[i].firstname;
                        z.nba = Wczytywanie.response[i].nba.start.ToString();
                        z.weight = Wczytywanie.response[i].weight.kilograms;
                        z.height = Wczytywanie.response[i].height.meters;
                        z.id = Wczytywanie.response[i].id;
                        z.college = Wczytywanie.response[i].college;
                        z.affiliation = Wczytywanie.response[i].affiliation;

                        //dodanie z do DataGridu
                        Testowa.Items.Add(z);
                    }
                }
    }
    }
}

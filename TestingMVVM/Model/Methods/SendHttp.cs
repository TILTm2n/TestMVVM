using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using TestingMVVM.Model.ModelOfCountry;

namespace TestingMVVM.Model.Methods
{
    public class SendHttp
    {
        public async Task<ObservableCollection<CountryModel>> HttpResponse(string URL)
        {
            if (String.IsNullOrEmpty(Convert.ToString(URL)))
            {
                throw new Exception("Пожалуйста введите название страны!!!");
            }

            if (!Regex.Match(URL, @"^[A-Za-z.\s_-]+$").Success)
            {
                throw new Exception($"Данные были введены некорректно! \nПожалуйста повторите попытку");
            }

            HttpClient client = new HttpClient();

            string responseString = await client.GetStringAsync("https://restcountries.eu/rest/v2/name/" + Convert.ToString(URL));

            List<CountryModel> restoredCountries = JsonSerializer.Deserialize<List<CountryModel>>(responseString);

            ObservableCollection<CountryModel> setOfCountries = new ObservableCollection<CountryModel>(restoredCountries);

            return setOfCountries;

        }
    }
}

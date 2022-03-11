

using fickle.Modelo;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace fickle.API
{
    public static class ApiService
    {
        public static string Url = "https://api.currencyapi.com/v3/convert"; //conexão com a API

        public static async Task<fickle.Modelo.Convert> GetConvert(string currentValue, string currency)
        {
            try
            {
                currentValue = GetOnlyValue(currentValue);
                HttpClient client = new HttpClient();

                var response = await client.GetAsync($"{Url}/?value={currentValue}&base_currency={currency}"); //conexão com o parâmetro da API

                // caso exceda o limte de requisições (TooManyRequests)
                if (!response.IsSuccessStatusCode)
                    return GetResponse();

                string r = response.Content.ReadAsStringAsync().Result;
                fickle.Modelo.Convert convert = JsonConvert.DeserializeObject<fickle.Modelo.Convert>(r);
                return convert;

            }
            catch (Exception e)
            {
                return GetResponse();
            }
        }

        private static Modelo.Convert GetResponse() //Método criado caso exceda o limite de requisições na API, testar com esses valores
        {
            return new Modelo.Convert
            {
                data = new Data
                {
                    BRL = new BRL
                    {
                        value = 2200
                    },
                    USD = new USD
                    {
                        value = 1500
                    },
                    EUR = new EUR
                    {
                        value = 3000
                    },
                    MXN = new MXN
                    {
                        value = 1100
                    },
                    CAD = new CAD
                    {
                        value = 5000
                    }
                }
            };
        }

        private static string GetOnlyValue(string currentValue) //Criando um replace para enviar apenas um número para API, sem o R$ 
        {
            if (string.IsNullOrEmpty(currentValue))
                return null;

            return currentValue.Replace("R$", "").Replace(",", ".");
        }
    }
}

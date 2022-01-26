using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using PancakeSwapAPI.Model;
using Newtonsoft;

namespace PancakeSwapAPI
{
    class Program
    {
        static HttpClient client = new HttpClient(new HttpClientHandler { UseCookies = false });

        public static async Task<Root> GetPrices(string token)
        {
            Root CryptoInfo = new Root();
            ///////////////////////////////GET//////////////////////////////////
            //LogsApplicatifs.LogInformation(string.Format(string.Format("Lancement de la requete GET")));
            using (HttpResponseMessage response = await client.GetAsync(ConfigurationManager.AppSettings["CheminUrl"].ToString()+token))
                if (response.IsSuccessStatusCode)
                {
                    //Site Utile : https://json2csharp.com/                   
                     CryptoInfo = await response.Content.ReadAsAsync<Root>();
                    //System.Net.Http.Formatting

                }
                else
                {
                    Console.WriteLine(string.Format(string.Format("Connexion a l'API impossible pour raison : {0}.", response.ReasonPhrase)));
                    //CryptoInfo;
                }
            return CryptoInfo;
        }
        static void Main(string[] args)
        {
            //Obligatoire pour l'asynchrone
            RunAsync().GetAwaiter().GetResult();
        }
        static async Task RunAsync()
        {
            try
            {              
                //specify to use TLS 1.2 as default connection
                System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;

                client.BaseAddress = new Uri(ConfigurationManager.AppSettings["Url"].ToString());
                client.DefaultRequestHeaders.Accept.Clear();

                string token = "0x3581a7b7be2ed2edf98710910fd05b0e8545f1db";

                Root CryptoInfo = await GetPrices(token);

                Console.WriteLine(CryptoInfo.data.name + "/n");
                Console.WriteLine(CryptoInfo.data.price);
                Console.WriteLine(CryptoInfo.data.price_BNB);
                Console.WriteLine(CryptoInfo.data.symbol);
                Console.WriteLine("ok");
            }
            catch (Exception e)
            {
                Console.WriteLine("Une erreur a interrompu le traitement. {0} \n {1}", e.Message, e.StackTrace);
                Environment.Exit(-1);
            }
        }
    }
}

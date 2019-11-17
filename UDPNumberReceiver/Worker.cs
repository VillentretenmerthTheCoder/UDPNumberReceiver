using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Http;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace UDPNumberReceiver
{
    public class Worker
    {
        private string URL = "https://sensorewebapi20191117112306.azurewebsites.net/api/SensorDatas";

        public async Task<IList<SensorData>> GetAllDataAsync()
        {
            using (HttpClient client = new HttpClient())
            {

                string content = await client.GetStringAsync(URL);
                IList<SensorData> cList = JsonConvert.DeserializeObject<IList<SensorData>>(content);
                return cList;
            }
        }

        public async Task PostDataAsync(string data)
        {
            using(HttpClient client = new HttpClient())
            {
                var content = new StringContent(data, Encoding.UTF8, "application/json");
                await client.PostAsync(URL, content);
            }
        }


    }
}
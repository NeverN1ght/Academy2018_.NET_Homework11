using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace AirportUWPClient.Abstractions
{
    public abstract class BaseDataLoadService<TEntity>
        where TEntity : class
    {
        protected string requestURL { get; set; }

        public async Task<List<TEntity>> LoadData()
        {
            try
            {
                using (var httpClient = new HttpClient())
                {
                    var data = await httpClient.GetStringAsync($"{requestURL}");
                    return JsonConvert.DeserializeObject<List<TEntity>>(data);
                }
            }
            catch (Exception)
            {
                return new List<TEntity>();
            }
        }

        public async Task Create(TEntity entity)
        {
            try
            {
                using (var httpClient = new HttpClient())
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        var serializer = new DataContractJsonSerializer(typeof(TEntity));
                        serializer.WriteObject(memoryStream, entity);
                        memoryStream.Position = 0;
                        var dataToPost = new StreamContent(memoryStream);
                        dataToPost.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                        var response = await httpClient.PostAsync(new Uri(requestURL), dataToPost);
                        var receivedBody = response.EnsureSuccessStatusCode();
                        var stream = await receivedBody.Content.ReadAsStreamAsync();
                        var readData = serializer.ReadObject(stream) as TEntity;
                    }
                }
            }
            catch (Exception)
            {
                // handle
            }
        }

        public async Task Update(string id, TEntity entity)
        {
            try
            {
                using (var httpClient = new HttpClient())
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        var serializer = new DataContractJsonSerializer(typeof(TEntity));
                        serializer.WriteObject(memoryStream, entity);
                        memoryStream.Position = 0;
                        var dataToPut = new StreamContent(memoryStream);
                        dataToPut.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                        var response = await httpClient.PutAsync(new Uri($"{requestURL}/{id}"), dataToPut);
                        response.EnsureSuccessStatusCode();
                    }
                }
            }
            catch (Exception)
            {
                //handle
            }
        }

        public async Task Delete(string id)
        {
            try
            {
                using (var httpClient = new HttpClient())
                {
                    var response = await httpClient.DeleteAsync(new Uri($"{requestURL}/{id}"));
                    response.EnsureSuccessStatusCode();
                }
            }
            catch (Exception)
            {
                //handle
            }
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using ApiServer;
using ApiServer.Controllers;
using InMemoryDatabase.Extensions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace HttpClientConsole
{
    public class Program
    {
        public  static async Task Main(string[] args)
        {


            /*
             // http get
             
            using (var httpclient = new HttpClient())
            {

                string url = "https://localhost:44332/WeatherForecast";
                var response =await  httpclient.GetAsync(url);
                // another way 
                var oputput =await  httpclient.GetStringAsync(url);
                
                Console.WriteLine(oputput);
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    
                    
                    var output = await response.Content.ReadAsStringAsync();
                    Console.WriteLine(output);
                   var std = JsonSerializer.Deserialize<List<WeatherForecast>>(output , new JsonSerializerOptions()
                       {
                           PropertyNameCaseInsensitive = true
                       }
                   );
                   foreach (var item in std)
                   {
                       Console.WriteLine(item.Summary );
                   }
                }
                   
                      
                
                
            }
            */

            /*// http post 
            using (var httpclient = new HttpClient())
            {
                string url = "https://localhost:44332/WeatherForecast";

                var obj = getObj();
                obj.Summary = null;
              //  var response =await  httpclient.PostAsJsonAsync(url,obj);
              var serilaize = System.Text.Json.JsonSerializer.Serialize(obj);
              
              var stringContent = new StringContent(serilaize, Encoding.UTF8,"application/json");
              var response =await  httpclient.PostAsync(url,stringContent);
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    Console.WriteLine(await response.Content.ReadAsStringAsync());
                }

                if (response.StatusCode == HttpStatusCode.BadRequest)
                {
                    var xx =GetHttpError.ExeractErrorFromWebApiResponse(await response.Content.ReadAsStringAsync());

                    foreach (var item in xx)
                    {
                        Console.WriteLine(item.Key);
                        foreach (var item2 in item.Value)
                        {
                            Console.WriteLine(item2);
                        }
                    }
                }
            }*/

            /*using (var httpclient = new HttpClient())
            {
                string url = "https://localhost:44332/WeatherForecast/GetWithHeader";

                using (var httpRequestMessge = new HttpRequestMessage(HttpMethod.Get,url))
                {
                    
                    // for spesific header request  
                    httpRequestMessge.Headers.Add("count","5");
                    // for all request will use same header
                    httpclient.DefaultRequestHeaders.Add("count","5");
                    var response = await httpclient.SendAsync(httpRequestMessge);
                  Console.WriteLine(await response.Content.ReadAsStringAsync());  
                
                }
                using (var httpRequestMessge = new HttpRequestMessage(HttpMethod.Get,url))
                {
                    
                  //  httpRequestMessge.Headers.Add("count","6");
                    var response2 = await httpclient.SendAsync(httpRequestMessge);
                    Console.WriteLine(await response2.Content.ReadAsStringAsync());  
                }

            
            }*/

            /*using (var httpClient = new HttpClient())
            {

                var userInfo = new UserInfo {email = "ssss", password = "123", username = "sss", Id = 1};
                string url = "https://localhost:44332/user/login";
              
                var httpResponseToken = await httpClient.PostAsJsonAsync(url, userInfo);
                httpResponseToken.EnsureSuccessStatusCode();
                var responseToken = JsonSerializer.Deserialize<UserToken>(await
                    httpResponseToken.Content.ReadAsStringAsync(), new JsonSerializerOptions{PropertyNameCaseInsensitive = true});
                // here to send token for Requests 
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer",
                    responseToken.Token);

                string url2 = "https://localhost:44332/WeatherForecast";
                var response =await  httpClient.GetAsync(url2);
                // another way 
                Console.WriteLine(response.StatusCode);
                response.EnsureSuccessStatusCode();
                Console.WriteLine("Person created successfully");
                Console.WriteLine(await response.Content.ReadAsStringAsync());
            }*/
            /*
            var serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);
            var services = serviceCollection.BuildServiceProvider();
            // base on name cleint 
            var httpClientFactory = services.GetService<IHttpClientFactory>();
            var client = httpClientFactory.CreateClient("weather");
            //string url = "https://localhost:44332/WeatherForecast/GetAll";
           var resposne  = await  client.GetAsync("GetAll");

            var httpClientFactory = services.GetService<IWeatherHttpCleint>();
            var resposne =await  httpClientFactory.GetAll();
            
           
           Console.WriteLine(resposne);
            */
            /*
            var serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);
            var services = serviceCollection.BuildServiceProvider();
            // base on name cleint 
            var httpClientFactory = services.GetService<IHttpClientFactory>();
            var client = httpClientFactory.CreateClient();

            var fileRoute = @"C:\Users\Mohammed\Desktop\datajson.txt";
            var fileName = Path.GetFileName(fileRoute);

            using var requestContexnt = new MultipartFormDataContent();
            using var fileStram = File.OpenRead(fileRoute);
            requestContexnt.Add(new StreamContent(fileStram), "file", fileName);
         var resposne = await client.PostAsync("https://localhost:44332/filesd/savefile", requestContexnt);
           // resposne.EnsureSuccessStatusCode();
            Console.WriteLine("file created successfully");
            */
            var serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);
            var services = serviceCollection.BuildServiceProvider();
            // base on name cleint 
            var httpClientFactory = services.GetService<IHttpClientFactory>();
            var client = httpClientFactory.CreateClient();

            // var resposne = await client.GetStringAsync("https://localhost:5001/test/bb.txt");
            // Console.WriteLine(resposne);

            // var resposne = await client.GetByteArrayAsync("https://localhost:5001/test/img.png");
            // Console.WriteLine(resposne.Length);
             var Route = @"C:\Users\Mohammed\Desktop\httpClient\HttpTutorial\ApiServer\wwwroot\test";
            using var stream = await client.GetStreamAsync("https://localhost:5001/test/img.png");
            using(var fileStram= File.Create(Path.Combine(Route,"txtxt24.png")))
            {
                    await stream.CopyToAsync(fileStram);

            }

        }

        public static void ConfigureServices(ServiceCollection services)
        {
            //services.AddHttpClient();
            services.AddHttpClient("weather", x =>
            {
                x.BaseAddress = new Uri("https://localhost:44332/WeatherForecast/");
            });
            services.AddHttpClient<IWeatherHttpCleint,WeatherHttpCleint>();
        }

        private static WeatherForecast getObj()
        {
            
            var rng = new Random();
            var x = new WeatherForecast
            {
                Date = DateTime.Now,
                TemperatureC = rng.Next(-20, 55),
                Summary = "Warm2"
            };
            return x;
        }
    }
}
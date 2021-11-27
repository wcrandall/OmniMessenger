using System;
using System.Collections.Generic;
using System.Net.Http;
using Newtonsoft.Json;
using OmniMessengerDesktop.Models;
using ReactiveUI;

namespace OmniMessengerDesktop.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        private readonly HttpClient client;

        private string greeting= "hi";

        public string Greeting { 
            get
            {
                return greeting;
            } 
            
            set
            {
                this.RaiseAndSetIfChanged(ref greeting, value);
            } 
        }

        public MainWindowViewModel()
        {
            HttpClientHandler clientHandler = new HttpClientHandler();
            clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => {return true;};
            this.client = new HttpClient(clientHandler); 
        }

        public async void GetTheStringCommand(){
            Uri uri = new Uri(string.Format("https://localhost:5001/message", string.Empty));
            HttpResponseMessage response = await client.GetAsync(uri);
            List<ContactInfo> contactInfos = null;

            if(response.IsSuccessStatusCode){
                string content = await response.Content.ReadAsStringAsync();

                if(content != null)
                {
                    contactInfos = JsonConvert.DeserializeObject<List<ContactInfo>>(content);
                }

                Console.WriteLine(content);
            }

            Console.WriteLine(response.IsSuccessStatusCode.ToString());

            if(contactInfos == null || contactInfos.Count == 0){
                return;
            }
            
            this.Greeting = contactInfos[0].FullName + " has an email address " + contactInfos[0].Email;
            
            Console.WriteLine(this.Greeting);
        }

        public void DoSomething(){
            this.Greeting = "Do your homework";
        }
        
    }
}

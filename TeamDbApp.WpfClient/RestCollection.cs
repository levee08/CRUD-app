﻿using CTWO80_HFT_2022232.Models;
using Microsoft.AspNetCore.SignalR.Client;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MovieDbApp.WpfClient
{
    public class RestService
    {
        public HttpClient client;

        public RestService(string baseurl, string pingableEndpoint = "swagger")
        {
            bool isOk = false;
            do
            {
                isOk = Ping(baseurl + pingableEndpoint);
            } while (isOk == false);
            Init(baseurl);
        }

        private bool Ping(string url)
        {
            try
            {
                WebClient wc = new WebClient();
                wc.DownloadData(url);
                return true;
            }
            catch
            {
                return false;
            }
        }

        private void Init(string baseurl)
        {
            client = new HttpClient();
            client.BaseAddress = new Uri(baseurl);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue
                ("application/json"));
            try
            {
                client.GetAsync("").GetAwaiter().GetResult();
            }
            catch (HttpRequestException)
            {
                throw new ArgumentException("Endpoint is not available!");
            }

        }

        public async Task<List<T>> GetAsync<T>(string endpoint)
        {
            List<T> items = new List<T>();
            HttpResponseMessage response = await client.GetAsync(endpoint);
            if (response.IsSuccessStatusCode)
            {
                items = await response.Content.ReadAsAsync<List<T>>();
            }
            else
            {
                var error = await response.Content.ReadAsAsync<RestExceptionInfo>();
                throw new ArgumentException(error.Msg);
            }   
            return items;
        }

        public List<T> Get<T>(string endpoint)
        {
            List<T> items = new List<T>();
            HttpResponseMessage response = client.GetAsync(endpoint).GetAwaiter().GetResult();
            if (response.IsSuccessStatusCode)
            {
                items = response.Content.ReadAsAsync<List<T>>().GetAwaiter().GetResult();
            }
            else
            {
                var error = response.Content.ReadAsAsync<RestExceptionInfo>().GetAwaiter().GetResult();
                throw new ArgumentException(error.Msg);
            }
            return items;
        }

        public async Task<T> GetSingleAsync<T>(string endpoint)
        {
            T item = default(T);
            HttpResponseMessage response = await client.GetAsync(endpoint);
            if (response.IsSuccessStatusCode)
            {
                item = await response.Content.ReadAsAsync<T>();
            }
            else
            {
                var error = await response.Content.ReadAsAsync<RestExceptionInfo>();
                throw new ArgumentException(error.Msg);
            }
            return item;
        }

        public T GetSingle<T>(string endpoint)
        {
            T item = default(T);
            HttpResponseMessage response = client.GetAsync(endpoint).GetAwaiter().GetResult();
            if (response.IsSuccessStatusCode)
            {
                item = response.Content.ReadAsAsync<T>().GetAwaiter().GetResult();
            }
            else
            {
                var error = response.Content.ReadAsAsync<RestExceptionInfo>().GetAwaiter().GetResult();
                throw new ArgumentException(error.Msg);
            }
            return item;
        }

        public async Task<T> GetAsync<T>(int id, string endpoint)
        {
            T item = default(T);
            HttpResponseMessage response = await client.GetAsync(endpoint + "/" + id.ToString());
            if (response.IsSuccessStatusCode)
            {
                item = await response.Content.ReadAsAsync<T>();
            }
            else
            {
                var error = await response.Content.ReadAsAsync<RestExceptionInfo>();
                throw new ArgumentException(error.Msg);
            }
            return item;
        }

        public T Get<T>(int id, string endpoint)
        {
            T item = default(T);
            HttpResponseMessage response = client.GetAsync(endpoint + "/" + id.ToString()).GetAwaiter().GetResult();
            if (response.IsSuccessStatusCode)
            {
                item = response.Content.ReadAsAsync<T>().GetAwaiter().GetResult();
            }
            else
            {
                var error = response.Content.ReadAsAsync<RestExceptionInfo>().GetAwaiter().GetResult();
                throw new ArgumentException(error.Msg);
            }
            return item;
        }

        public async Task PostAsync<T>(T item, string endpoint)
        {
            HttpResponseMessage response =
                await client.PostAsJsonAsync(endpoint, item);

            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsAsync<RestExceptionInfo>();
                throw new ArgumentException(error.Msg);
            }
            response.EnsureSuccessStatusCode();
        }

        public void Post<T>(T item, string endpoint)
        {
            HttpResponseMessage response =
                client.PostAsJsonAsync(endpoint, item).GetAwaiter().GetResult();

            if (!response.IsSuccessStatusCode)
            {
                var error = response.Content.ReadAsAsync<RestExceptionInfo>().GetAwaiter().GetResult();
                throw new ArgumentException(error.Msg);
            }
            response.EnsureSuccessStatusCode();
        }

        public async Task DeleteAsync(int id, string endpoint)
        {
            HttpResponseMessage response =
                await client.DeleteAsync(endpoint + "/" + id.ToString());

            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsAsync<RestExceptionInfo>();
                throw new ArgumentException(error.Msg);
            }

            response.EnsureSuccessStatusCode();
        }

        public void Delete(int id, string endpoint)
        {
            HttpResponseMessage response =
                client.DeleteAsync(endpoint + "/" + id.ToString()).GetAwaiter().GetResult();

            if (!response.IsSuccessStatusCode)
            {
                var error = response.Content.ReadAsAsync<RestExceptionInfo>().GetAwaiter().GetResult();
                throw new ArgumentException(error.Msg);
            }

            response.EnsureSuccessStatusCode();
        }

        public async Task PutAsync<T>(T item, string endpoint)
        {
            HttpResponseMessage response =
                await client.PutAsJsonAsync(endpoint, item);

            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsAsync<RestExceptionInfo>();
                throw new ArgumentException(error.Msg);
            }

            response.EnsureSuccessStatusCode();
        }

        public void Put<T>(T item, string endpoint)
        {
            HttpResponseMessage response =
                client.PutAsJsonAsync(endpoint, item).GetAwaiter().GetResult();

            if (!response.IsSuccessStatusCode)
            {
                var error = response.Content.ReadAsAsync<RestExceptionInfo>().GetAwaiter().GetResult();
                throw new ArgumentException(error.Msg);
            }

            response.EnsureSuccessStatusCode();
        }

    }
    public class RestExceptionInfo
    {
        public RestExceptionInfo()
        {

        }
        public string Msg { get; set; }
    }
    class NotifyService
    {
        private HubConnection conn;

        public NotifyService(string url)
        {
            conn = new HubConnectionBuilder()
                .WithUrl(url)
                .Build();

            conn.Closed += async (error) =>
            {
                await Task.Delay(new Random().Next(0, 5) * 1000);
                await conn.StartAsync();
            };
        }

        public void AddHandler<T>(string methodname, Action<T> value)
        {
            conn.On<T>(methodname, value);
        }

        public async void Init()
        {
            await conn.StartAsync();
        }

    }

    public class RestCollection<T> : INotifyCollectionChanged, IEnumerable<T>
    {
        public event NotifyCollectionChangedEventHandler? CollectionChanged;

        NotifyService notify;
        RestService rest;
        List<T> items;
        bool hasSignalR;
        Type type = typeof(T);

        public RestCollection(string baseurl, string endpoint, string hub = null)
        {
            hasSignalR = hub != null;
            this.rest = new RestService(baseurl, endpoint);
            if (hub != null)
            {
                this.notify = new NotifyService(baseurl + hub);
                this.notify.AddHandler<T>(type.Name + "Created", (T item) =>
                {
                    items.Add(item);
                    CollectionChanged?.Invoke(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
                });
                this.notify.AddHandler<T>(type.Name + "Deleted", (T item) =>
                {
                    var element = items.FirstOrDefault(t => t.Equals(item));
                    if (element != null)
                    {
                        items.Remove(item);
                        CollectionChanged?.Invoke(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
                    }
                    else
                    {
                        Init(endpoint);
                    }

                });
                this.notify.AddHandler<T>(type.Name + "Updated", (T item) =>
                {
                    Init(endpoint);
                });

                this.notify.Init();
            }
            Init(endpoint);
        }

        private async Task Init(string endpoint)
        {   
            items = await rest.GetAsync<T>(endpoint);
            CollectionChanged?.Invoke(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
        }

        public IEnumerator<T> GetEnumerator()
        {
            if (items != null)
            {
                return items.GetEnumerator();
            }
            else return new List<T>().GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            if (items != null)
            {
                return items.GetEnumerator();
            }
            else return new List<T>().GetEnumerator();
        }

        public void Add(T item,string endpoint)
        {
            if (hasSignalR)
            {
                 this.rest.PostAsync(item, endpoint);
                
            }
            else
            {
                this.rest.PostAsync(item, endpoint).ContinueWith((t) =>
                {
                    Init(endpoint).ContinueWith(z =>
                    {
                        Application.Current.Dispatcher.Invoke(() =>
                        {
                            CollectionChanged?.Invoke(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
                        });
                    });
                });
              
            }

        }

        public void Update(T item,string endpoint)
        {
            if (hasSignalR)
            {
                this.rest.PutAsync(item, endpoint);
              
            }
            else
            {
                this.rest.PutAsync(item, endpoint).ContinueWith((t) =>
                {
                    Init(endpoint).ContinueWith(z =>
                    {
                        Application.Current.Dispatcher.Invoke(() =>
                        {
                            CollectionChanged?.Invoke(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
                        });
                    });
                });
               
            }
        }

        public void Delete(int id,string endpoint)
        {
            if (hasSignalR)
            {
                this.rest.DeleteAsync(id, endpoint);
               
            }
            else
            {
                this.rest.DeleteAsync(id, endpoint).ContinueWith((t) =>
                {
                    Init(endpoint).ContinueWith(z =>
                    {
                        Application.Current.Dispatcher.Invoke(() =>
                        {
                            CollectionChanged?.Invoke(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
                        });
                    });
                });
             
            }


        }
        
        public RestCollection<KeyValuePair<string, int>> GetNonCrudData(string endpoint)
        {
            try
            {
                HttpResponseMessage response = rest.client.GetAsync(endpoint).GetAwaiter().GetResult();

                if (response.IsSuccessStatusCode)
                {
                    HttpContent content = response.Content;
                    Task<string> result = content.ReadAsStringAsync();
                    string contentString = result.GetAwaiter().GetResult();

                    List<KeyValuePair<string, int>> items = JsonConvert.DeserializeObject<List<KeyValuePair<string, int>>>(contentString);

                    return new RestCollection<KeyValuePair<string, int>>("http://localhost:29829/", "PlayerNonCrud/ThrophiesByPosition", "hub") { items = items };
                }
                else
                {
                    throw new HttpRequestException($"HTTP Error: {response.StatusCode}");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return new RestCollection<KeyValuePair<string, int>>("http://localhost:29829/", "PlayerNonCrud/ThrophiesByPosition", "hub");
            }
        }






    }
}
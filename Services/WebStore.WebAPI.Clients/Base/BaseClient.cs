using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading;
using System.Threading.Tasks;

namespace WebStore.WebAPI.Clients.Base
{
    public abstract class BaseClient : IDisposable
    {
        protected HttpClient Http { get; }

        protected string Address { get; }

        protected BaseClient(HttpClient Client, string Address)
        {
            Http = Client;
            this.Address = Address;
        }

        protected T Get<T>(string url) => GetAsync<T>(url).Result;
        protected async Task<T> GetAsync<T>(string url, CancellationToken Cancel = default)
        {
            var response = await Http.GetAsync(url, Cancel).ConfigureAwait(false);
            return await response
               .EnsureSuccessStatusCode()
               .Content.ReadFromJsonAsync<T>(cancellationToken: Cancel)
               .ConfigureAwait(false);
        }

        protected HttpResponseMessage Post<T>(string url, T item) => PostAsync(url, item).Result;
        protected async Task<HttpResponseMessage> PostAsync<T>(string url, T item, CancellationToken Cancel = default)
        {
            var response = await Http.PostAsJsonAsync(url, item, Cancel).ConfigureAwait(false);
            return response.EnsureSuccessStatusCode();
        }

        protected HttpResponseMessage Put<T>(string url, T item) => PutAsync(url, item).Result;
        protected async Task<HttpResponseMessage> PutAsync<T>(string url, T item, CancellationToken Cancel = default)
        {
            var response = await Http.PutAsJsonAsync(url, item, Cancel).ConfigureAwait(false);
            return response.EnsureSuccessStatusCode();
        }

        protected HttpResponseMessage Delete(string url) => DeleteAsync(url).Result;
        protected async Task<HttpResponseMessage> DeleteAsync(string url, CancellationToken Cancel = default)
        {
            var response = await Http.DeleteAsync(url, Cancel).ConfigureAwait(false);
            return response;
        }

        public void Dispose()
        {
            Dispose(true);
            //GC.SuppressFinalize(this);
        }

        //~BaseClient() => Dispose(false);

        private bool _Disposed;
        protected virtual void Dispose(bool disposing)
        {
            if(_Disposed) return;
            _Disposed = true;

            if (disposing)
            {
                // уничтожаем управляемые ресурсы
                //Http.Dispose(); // если не Вы создали объект, то и не Вам его уничтожать!
            }

            // уничтожаем неуправляемые ресурсы при их наличии
        }
    }
}

using ControleCorrida.Model;
using Microsoft.WindowsAzure.MobileServices;
using Microsoft.WindowsAzure.MobileServices.SQLiteStore;
using Microsoft.WindowsAzure.MobileServices.Sync;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleCorrida.Services
{
    public class AzureClient
    {
        private IMobileServiceClient _client;
        private IMobileServiceSyncTable<Atividade> _table;
        private const string serviceUri = "http://controlecorrida.azurewebsites.net/";
        const string dbPath = "atividadeDb";

        public AzureClient()
        {
            _client = new MobileServiceClient(serviceUri);

            var store = new MobileServiceSQLiteStore(dbPath);
            store.DefineTable<Atividade>();
            _client.SyncContext.InitializeAsync(store);

            _table = _client.GetSyncTable<Atividade>();
        }

        public async Task<IEnumerable<Atividade>> GetAtividades()
        {
            var empty = new Atividade[0];
            try
            {
                if (Plugin.Connectivity.CrossConnectivity.Current.IsConnected)
                    await SyncAsync();
                return await _table.ToEnumerableAsync();
            }
            catch (Exception)
            {
                return empty;
            }
        }

        public async Task AddAtividade(Atividade atividade)
        {
            await _table.InsertAsync(atividade);
        }

        public async Task SyncAsync()
        {
            ReadOnlyCollection<MobileServiceTableOperationError> syncErrors = null;

            try
            {
                await _client.SyncContext.PushAsync();

                await _table.PullAsync("allAtividades", _table.CreateQuery());
            }
            catch (MobileServicePushFailedException pushEx)
            {
                if (pushEx.PushResult != null)
                    syncErrors = pushEx.PushResult.Errors;
            }
        }

        public async Task CleanData()
        {
            await _table.PurgeAsync("allAtividades", _table.CreateQuery(), new System.Threading.CancellationToken());
        }
    }
}

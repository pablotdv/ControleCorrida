using ControleCorrida.Model;
using ControleCorrida.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ControleCorrida.ViewModel
{
    public class AtividadesViewModel : ObservableBaseObject
    {
        public ObservableCollection<Atividade> Atividades { get; set; }
        private AzureClient _client;
        public Command RefreshCommand { get; set; }
        public Command GenerateAtividadesCommand { get; set; }
        public Command CleanLocalDataCommand { get; set; }

        public bool IsBusy
        {
            get
            {
                return isBusy;
            }

            set
            {
                isBusy = value;
                OnPropertyChanged();
            }
        }
        private bool isBusy;

        public AtividadesViewModel()
        {
            RefreshCommand = new Command(() => Load());
            GenerateAtividadesCommand = new Command(() => generateAtividades());
            CleanLocalDataCommand = new Command(() => cleanLocalData());
            Atividades = new ObservableCollection<Atividade>();
            _client = new AzureClient();
        }

        private async void cleanLocalData()
        {
            await _client.CleanData();
        }

        private async Task generateAtividades()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            await _client.AddAtividade(new Atividade { Data = new DateTime(2017, 03, 01), Voltas = 09, Tempo = new DateTime(2017, 03, 01, 0, 31, 57) });
            await _client.AddAtividade(new Atividade { Data = new DateTime(2017, 02, 14), Voltas = 10, Tempo = new DateTime(2017, 02, 14, 0, 33, 22) });
            await _client.AddAtividade(new Atividade { Data = new DateTime(2017, 02, 13), Voltas = 10, Tempo = new DateTime(2017, 02, 13, 0, 32, 35) });
            await _client.AddAtividade(new Atividade { Data = new DateTime(2017, 02, 10), Voltas = 10, Tempo = new DateTime(2017, 02, 10, 0, 32, 27) });
            await _client.AddAtividade(new Atividade { Data = new DateTime(2017, 02, 09), Voltas = 10, Tempo = new DateTime(2017, 02, 09, 0, 32, 48) });
            await _client.AddAtividade(new Atividade { Data = new DateTime(2017, 02, 08), Voltas = 10, Tempo = new DateTime(2017, 02, 08, 0, 33, 21) });
            await _client.AddAtividade(new Atividade { Data = new DateTime(2017, 02, 07), Voltas = 10, Tempo = new DateTime(2017, 02, 07, 0, 33, 52) });

            IsBusy = false;
        }

        public async void Load()
        {
            var result = await _client.GetAtividades();

            Atividades.Clear();

            foreach (var item in result)
            {
                Atividades.Add(item);
            }
            IsBusy = false;
        }
    }
}

using Android.App;
using Android.Widget;
using Android.OS;
using SQLite;
using System.IO;
using System.Linq;
using Android.Content;
using Android.Runtime;

namespace ControleCorrida
{
    [Activity(Label = "ControleCorrida", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView (Resource.Layout.Main);

            Button btnAtividade = FindViewById<Button>(Resource.Id.btnNovaAtividade);
            btnAtividade.Click += delegate
            {
                Intent objIntent = new Intent(this, typeof(AtividadeCadActivity));
                StartActivity(objIntent);
                
            };
        }

        protected override void OnResume()
        {
            base.OnResume();

            Carregar();
        }
        
        private void Carregar()
        {
            var path = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            path = Path.Combine(path, "Base.db3");
            var conn = new SQLiteConnection(path);

            conn.CreateTable<Atividade>();

            var atividades = conn.Table<Atividade>()
                .OrderByDescending(a => a.Data)
                .ToList();

            ListView lvAtividades = FindViewById<ListView>(Resource.Id.ltvAtividades);
            AtividadeAdapter adapter = new AtividadeAdapter(atividades.ToArray());
            lvAtividades.Adapter = adapter;
        }
    }
}


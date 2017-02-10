using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System.IO;
using SQLite;
using Android.Util;

namespace ControleCorrida
{
    [Activity(Label = "AtividadeCadActivity")]
    public class AtividadeCadActivity : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            SetContentView(Resource.Layout.AtividadeCadLayout);

            var btnData = FindViewById<Button>(Resource.Id.btnData);

            btnData.Click += delegate
             {
                 DatePickerFragment frag = DatePickerFragment.NewInstance(delegate (DateTime time)
                 {
                     btnData.Text = time.ToShortDateString();
                 });
                 frag.Show(FragmentManager, DatePickerFragment.TAG);
             };

            var path = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            path = Path.Combine(path, "Base.db3");
            var conn = new SQLiteConnection(path);
            conn.CreateTable<Atividade>();

            var btnSalvar = FindViewById<Button>(Resource.Id.btnSalvar);

            btnSalvar.Click += delegate
            {
                
                var edtVoltas = FindViewById<EditText>(Resource.Id.edtVoltas);
                var edtTempo = FindViewById<EditText>(Resource.Id.edtTempo);

                DateTime data = DateTime.Parse(btnData.Text);
                decimal voltas = Decimal.Parse(edtVoltas.Text);
                TimeSpan tempo = TimeSpan.Parse(edtTempo.Text);

                Atividade atividade = new Atividade()
                {
                    Data = data,
                    Voltas = voltas,
                    Tempo = tempo,
                };

                conn.Insert(atividade);
                Toast.MakeText(this, "Atividade salva", ToastLength.Long).Show();
                OnBackPressed();
            };
        }
    }

    public class DatePickerFragment : DialogFragment,
                                  DatePickerDialog.IOnDateSetListener
    {
        // TAG can be any string of your choice.
        public static readonly string TAG = "X:" + typeof(DatePickerFragment).Name.ToUpper();

        // Initialize this value to prevent NullReferenceExceptions.
        Action<DateTime> _dateSelectedHandler = delegate { };

        public static DatePickerFragment NewInstance(Action<DateTime> onDateSelected)
        {
            DatePickerFragment frag = new DatePickerFragment();
            frag._dateSelectedHandler = onDateSelected;
            return frag;
        }

        public override Dialog OnCreateDialog(Bundle savedInstanceState)
        {
            DateTime currently = DateTime.Now;
            DatePickerDialog dialog = new DatePickerDialog(Activity,
                                                           this,
                                                           currently.Year,
                                                           currently.Month,
                                                           currently.Day);
            return dialog;
        }

        public void OnDateSet(DatePicker view, int year, int monthOfYear, int dayOfMonth)
        {
            // Note: monthOfYear is a value between 0 and 11, not 1 and 12!
            DateTime selectedDate = new DateTime(year, monthOfYear + 1, dayOfMonth);
            Log.Debug(TAG, selectedDate.ToLongDateString());
            _dateSelectedHandler(selectedDate);
        }
    }
}
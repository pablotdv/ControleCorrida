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
using SQLite;

namespace ControleCorrida
{
    public class Atividade
    {
        [PrimaryKey]
        [NotNull]
        [AutoIncrement]
        public long AtividadeId { get; set; }

        public DateTime Data { get; set; }

        public decimal Voltas { get; set; }
        public TimeSpan Tempo { get; set; }

        public TimeSpan Media
        {
            get
            {                
                return new TimeSpan((long)(Tempo.Ticks / Voltas));
            }
        }
    }
}
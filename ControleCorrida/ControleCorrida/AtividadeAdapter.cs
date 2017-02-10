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

namespace ControleCorrida
{
    public class AtividadeAdapter : BaseAdapter<Atividade>
    {
        Atividade[] data;
        
        public AtividadeAdapter(Atividade[] data)
        {
            this.data = data;
        }

        public override Atividade this[int position]
        {
            get
            {
                return data[position];
            }
        }

        public override int Count
        {
            get
            {
                return data.Count();
            }
        }

        public override long GetItemId(int position)
        {
            return data[position].AtividadeId;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var inflater = LayoutInflater.From(parent.Context);
            var view = inflater.Inflate(Resource.Layout.AtividadeItemLayout, parent, false);

            var item = data[position];
            view.FindViewById<TextView>(Resource.Id.txtData).Text = item.Data.ToShortDateString();
            view.FindViewById<TextView>(Resource.Id.txtVoltas).Text = item.Voltas.ToString();
            view.FindViewById<TextView>(Resource.Id.txtTempo).Text = item.Tempo.ToString();
            view.FindViewById<TextView>(Resource.Id.txtMedia).Text = item.Media.ToString();

            return view;

        }
    }
}
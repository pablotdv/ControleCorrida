using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ControleCorrida.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ControleCorridaPage : ContentPage
    {
        public ControleCorridaPage()
        {
            InitializeComponent();
            BindingContext = new ViewModel.AtividadesViewModel();
        }
    }
}

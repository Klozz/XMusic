using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
namespace XMusic.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Paginas : ContentPage
    {
        public Paginas()
        {
            BindingContext = MusicModelView.Instance;
            InitializeComponent();
        }
    }
}
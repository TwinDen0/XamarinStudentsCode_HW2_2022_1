using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace HW2
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EditorPage : ContentPage
    {
        public string text;
        public EditorPage()
        {
            InitializeComponent();
        }

        private void Editor_TextChanged(object sender, TextChangedEventArgs e)
        {
            text = e.NewTextValue;
        }
    }
}
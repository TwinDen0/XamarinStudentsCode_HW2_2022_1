using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace HW2
{
    public partial class MainPage : ContentPage
    {
        ObservableCollection<string> list = new ObservableCollection<string>();
        public MainPage()
        {
            InitializeComponent();

            list.Add("Заметка 4");
            list.Add("Заметка 5");
            list.Add("Заметка 6");
            BindableLayout.SetItemsSource(notes, list);
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            var editor = new EditorPage();
            editor.Disappearing += (sender1, e1) =>
            {
                list.Add(editor.text);
            };
            Navigation.PushAsync(editor);
        }
    }

    
}

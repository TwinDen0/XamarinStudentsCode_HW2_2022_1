using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace HW2
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EditorPage : ContentPage
    {
        public string text;
        public EditorPage(string text = "")
        {
            InitializeComponent();
            this.text = text;
            editor.Text = this.text;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            editor.Focus();
        }

        private void Editor_TextChanged(object sender, TextChangedEventArgs e)
        {
            text = e.NewTextValue;
            count.Text = $"{e.NewTextValue.Length.ToString()} символов";
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }
    }
}
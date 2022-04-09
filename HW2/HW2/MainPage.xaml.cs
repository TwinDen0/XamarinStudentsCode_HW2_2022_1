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
    public class Note
    {
        public string visible_text { get; set; }
        public string full_text { get; set; }
    }
    public partial class MainPage : ContentPage
    {
        ObservableCollection<Note> list_left = new ObservableCollection<Note>();
        ObservableCollection<Note> list_right = new ObservableCollection<Note>();
        public MainPage()
        {
            InitializeComponent();
            SaveSystem.Load(out list_left, out list_right);
            BindableLayout.SetItemsSource(notes_left, list_left);
            BindableLayout.SetItemsSource(notes_right, list_right);
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            var editor = new EditorPage();

            editor.Disappearing += (sender1, e1) =>
            {
                if (editor.text == "")
                {
                    return;
                }

                var note = new Note { full_text = editor.text, visible_text = editor.text};
                var lines = note.full_text.Split('\n');
                
                if (lines.Length > 10)
                {
                    note.visible_text = string.Join("\n", lines.Take(10));

                }

                if (note.visible_text.Length > 300)
                {
                    note.visible_text = note.visible_text.Remove(300);
                    
                }

                if (note.visible_text != note.full_text)
                {
                    note.visible_text += "...";
                }

                if(notes_left.Height <= notes_right.Height)
                {
                    list_left.Add(note);
                } 
                else
                {
                    list_right.Add(note);
                }

                SaveSystem.Save(list_left, list_right);
            };
            Navigation.PushAsync(editor);
        }
    }

    
}

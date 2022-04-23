using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace HW2
{
    public class Note
    {
        private static int idCounter = 0;

        public int id { get; }
        public string visible_text { get => UIFixerSuperUtilsBazuka.Shorten(full_text, 120, 3); }
        public string full_text { get; set; }
        public string text_count { get => $"{full_text.Length} символов"; }
        public string creation_data { get; set; }

        public Note()
        {
            id = ++idCounter;
        }
    }
    public partial class MainPage : ContentPage
    {
        List<Note> list_all = new List<Note>();
        ObservableCollection<Note> list_left = new ObservableCollection<Note>();
        ObservableCollection<Note> list_right = new ObservableCollection<Note>();
        public MainPage()
        {
            InitializeComponent();
            SaveSystem.Load(out list_all);
            BindableLayout.SetItemsSource(notes_left, list_left);
            BindableLayout.SetItemsSource(notes_right, list_right);
        }

        protected override void OnSizeAllocated(double width, double height)
        {
            base.OnSizeAllocated(width, height);
            SortNotes();
        }

        private void SortNotes()
        {
            list_left.Clear();
            list_right.Clear();

            foreach (var note in list_all)
            {
                if (notes_left.Height <= notes_right.Height)
                {
                    list_left.Add(note);
                }
                else
                {
                    list_right.Add(note);
                }
                notes_left.ResolveLayoutChanges();
                notes_right.ResolveLayoutChanges();
            }

        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            var editor = new EditorPage();

            editor.Disappearing += (__, _) =>
            {
                if (editor.text == "")
                {
                    return;
                }

                var note = new Note { full_text = editor.text, creation_data = DateTime.Now.ToString("d")};
                list_all.Add(note);

                SortNotes();

                SaveSystem.Save(list_all);
            };
            Navigation.PushAsync(editor);
        }

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            var id = (sender as Frame).ClassId;
            foreach (var item in list_all)
            {
                if (item.id.ToString() == id)
                {
                    var editor = new EditorPage(item.full_text);

                    editor.Disappearing += (__, _) =>
                    {
                        item.full_text = editor.text;

                        SortNotes();

                        SaveSystem.Save(list_all);

                    };
                    Navigation.PushAsync(editor);
                    return;
                }
            }
        }

        private void PanGestureRecognizer_PanUpdated(object sender, PanUpdatedEventArgs e)
        {
            scroll.InputTransparent = true;
            if (e.TotalX > 0) return;
            var frame = (sender as Frame);
            switch (e.StatusType) {
                case GestureStatus.Running:
                    frame.TranslationX = e.TotalX;
                    break;
                case GestureStatus.Completed:
                case GestureStatus.Canceled:
                    frame.TranslationX = 0;
                    scroll.InputTransparent = false;
                    Note note = list_all.FirstOrDefault(u => u.id.ToString() == frame.ClassId);
                    Task.Run(() =>
                    {
                        Device.BeginInvokeOnMainThread(() =>
                        {
                            list_all.Remove(note);
                            SortNotes();
                        });
                    });
                    break;
                default:
                    frame.TranslationX = 0;
                    break;
            }
        }
    }

    
}

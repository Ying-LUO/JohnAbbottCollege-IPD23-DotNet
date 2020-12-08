using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Day05WpfNotePad
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
		private static NotepadDocument notepadDocument = new NotepadDocument();

        public MainWindow()
        {
            InitializeComponent();
            DataContext = notepadDocument;
        }

		private void NewFile(object sender, ExecutedRoutedEventArgs e)
        {
            notepadDocument.FileName = string.Empty;
            notepadDocument.FilePath = string.Empty;
            notepadDocument.Text = string.Empty;
		}

        public void SaveFile(object sender, ExecutedRoutedEventArgs e)
        {
            if (notepadDocument.isEmpty)
            {
                SaveAsFile(sender, e);
                return;
            }
            File.WriteAllText(notepadDocument.FilePath, notepadDocument.Text);
        }

        public void SaveAsFile(object sender, ExecutedRoutedEventArgs e)
        {
            var saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Text File (*.txt)|*.txt";
            if (saveFileDialog.ShowDialog() == true)
            {
                DockFile(saveFileDialog);
                File.WriteAllText(saveFileDialog.FileName, notepadDocument.Text);
            }
        }

        public void OpenFile(object sender, ExecutedRoutedEventArgs e)
        {
            var openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                DockFile(openFileDialog);
                notepadDocument.Text = File.ReadAllText(openFileDialog.FileName);
            }
        }

        private void DockFile<T>(T dialog) where T : FileDialog
        {
            notepadDocument.FilePath = dialog.FileName;  
            notepadDocument.FileName = dialog.SafeFileName;
        }

        public void CloseFile(object sender, ExecutedRoutedEventArgs e)
        {
            SaveFile(sender, e);
            Application.Current.MainWindow.Close();
        }

    }
}

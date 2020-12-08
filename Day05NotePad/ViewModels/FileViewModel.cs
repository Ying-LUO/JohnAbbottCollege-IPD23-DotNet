using Day05NotePad.Models;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Day05NotePad.ViewModels
{
    public class FileViewModel
    {
        public DocumentModel Document
        {
            get;
            private set;
        }

        // Toolbar commands
        public ICommand NewCommand
        {
            get;
        }

        public ICommand SaveCommand
        {
            get;
        }

        public ICommand SaveAsCommand
        {
            get;
        }

        public ICommand OpenCommand
        {
            get;
        }

        public FileViewModel(DocumentModel document)
        {
            Document = document;
            NewCommand = new RelayCommand(NewFile);
            SaveCommand = new RelayCommand(SaveFile);
            SaveAsCommand = new RelayCommand(SaveAsFile);
            OpenCommand = new RelayCommand(OpenFile);
        }

        public void NewFile()
        {
            Document.FileName = string.Empty;
            Document.FilePath = string.Empty;
            Document.Text = string.Empty;
        }

        public void SaveFile()
        {
            if (Document.isEmpty)
            {
                SaveAsFile();
                return;
            }
            File.WriteAllText(Document.FilePath, Document.Text);
        }

        public void SaveAsFile()
        {
            var saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Text File (*.txt)|*.txt";
            if (saveFileDialog.ShowDialog() == true)
            {
                DockFile(saveFileDialog);
                File.WriteAllText(saveFileDialog.FileName, Document.Text);
            }
        }

        public void OpenFile()
        {
            var openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                DockFile(openFileDialog);
                Document.Text = File.ReadAllText(openFileDialog.FileName);
            }
        }

        private void DockFile<T>(T dialog) where T : FileDialog
        {
            Document.FilePath = dialog.FileName;  // by using the T : FileDialog to make T get the parent property, like fileName, otherwise you cannot find that
            Document.FileName = dialog.SafeFileName;
        }

    }
}

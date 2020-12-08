using Day05NotePad.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day05NotePad.ViewModels
{
    public class MainViewModel
    {
        private DocumentModel _document;  // the only one accessing document

        public EditorViewModel Editor { get; set; }
        public FileViewModel File { get; set; }
        public HelpViewModel Help { get; set; }

        public MainViewModel()
        {
            _document = new DocumentModel();
            Help = new HelpViewModel();
            File = new FileViewModel(_document);
            Editor = new EditorViewModel(_document);
        }



    }
}

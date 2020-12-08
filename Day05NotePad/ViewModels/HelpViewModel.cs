using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Day05NotePad.ViewModels
{
    public class HelpViewModel : ObservableObject
    {
        public ICommand HelpCommand
        {
            get;
        }

        public HelpViewModel()
        {
            HelpCommand = new RelayCommand(DisplayAbout);
        }
        
        private void DisplayAbout()
        {
            // WE WILL OPEN HELP DIALOG
        }

    }
}

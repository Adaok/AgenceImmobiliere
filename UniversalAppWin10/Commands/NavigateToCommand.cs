using Oyosoft.AgenceImmobiliere.UniversalAppWin10.Commons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Oyosoft.AgenceImmobiliere.UniversalAppWin10.Commands
{
    public class NavigateToCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        private readonly Type _nextPageType;
        private readonly bool _withMenu;

        public NavigateToCommand(Type nextPageType, bool withMenu)
        {
            if (nextPageType == null)
            {
                throw new ArgumentNullException("nextPageType");
            }
            _nextPageType = nextPageType;
            _withMenu = withMenu;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            if (CanExecute(parameter) && _nextPageType != null)
            {
                if (_withMenu)
                    Connectivity.LaunchWithMenu(_nextPageType);
                else
                    Connectivity.LaunchWithoutMenu(_nextPageType);
            }
        }
    }
}

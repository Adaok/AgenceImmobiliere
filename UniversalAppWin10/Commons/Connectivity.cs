using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Oyosoft.AgenceImmobiliere.UniversalAppWin10.Commons
{
    class Connectivity
    {
        public static void LaunchWithoutMenu(Type pagetype)
        {
            var simpleFrame = new Frame();

            simpleFrame.Navigate(pagetype);

            Window.Current.Content = simpleFrame;
            Window.Current.Activate();

        }

        public static void LaunchWithMenu(Type pagetype)
        {
            var advanceFrame = new AppShell();

            advanceFrame.AppFrame.Navigate(pagetype);

            Window.Current.Content = advanceFrame;
            Window.Current.Activate();
        }
    }
}

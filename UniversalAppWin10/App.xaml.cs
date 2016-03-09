﻿using Oyosoft.AgenceImmobiliere.UniversalAppWin10.Commons;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

namespace Oyosoft.AgenceImmobiliere.UniversalAppWin10
{
    /// <summary>
    /// Fournit un comportement spécifique à l'application afin de compléter la classe Application par défaut.
    /// </summary>
    sealed partial class App : Application
    {

        private const string DATABASE_FILE_NAME = "agence.db";
        private const string ENDPOINT_ADRESS = "http://localhost:8645/agenceimmobiliere.svc";

        /// <summary>
        /// Initialise l'objet d'application de singleton.  Il s'agit de la première ligne du code créé
        /// à être exécutée. Elle correspond donc à l'équivalent logique de main() ou WinMain().
        /// </summary>
        public App()
        {
            this.InitializeComponent();
            this.Suspending += OnSuspending;
        }

        /// <summary>
        /// Invoqué lorsque l'application est lancée normalement par l'utilisateur final.  D'autres points d'entrée
        /// seront utilisés par exemple au moment du lancement de l'application pour l'ouverture d'un fichier spécifique.
        /// </summary>
        /// <param name="e">Détails concernant la requête et le processus de lancement.</param>
        protected override void OnLaunched(LaunchActivatedEventArgs e)
        {

#if DEBUG
            if (System.Diagnostics.Debugger.IsAttached)
            {
                this.DebugSettings.EnableFrameRateCounter = true;
            }
#endif

            Core.ViewModels.Connection._dbDefaultDirectoryPath = Windows.Storage.ApplicationData.Current.LocalFolder.Path;
            Core.ViewModels.Connection._dbDirectoryPath = "";
            Core.ViewModels.Connection._dbFileName = DATABASE_FILE_NAME;
            Core.ViewModels.Connection._sqlitePlatform = new SQLite.Net.Platform.WinRT.SQLitePlatformWinRT();
            Core.ViewModels.Connection._endpointConfigurationName = "";
            Core.ViewModels.Connection._endpointConfigurationAddress = ENDPOINT_ADRESS;

            AppShell shell = Window.Current.Content as AppShell;

            // Ne répétez pas l'initialisation de l'application lorsque la fenêtre comporte déjà du contenu,
            // assurez-vous juste que la fenêtre est active
            if (shell == null)
            {
                // Créez un Frame utilisable comme contexte de navigation et naviguez jusqu'à la première page
                shell = new AppShell();

                //default language
                shell.Language = Windows.Globalization.ApplicationLanguages.Languages[0];

                shell.AppFrame.NavigationFailed += OnNavigationFailed;

                if (e.PreviousExecutionState == ApplicationExecutionState.Terminated)
                {
                    //TODO: chargez l'état de l'application précédemment suspendue
                }

                // Placez le frame dans la fenêtre active
                Window.Current.Content = shell;
            }

            //if (shell.AppFrame.Content == null)
            //{
            //    // Quand la pile de navigation n'est pas restaurée, accédez à la première page,
            //    // puis configurez la nouvelle page en transmettant les informations requises en tant que
            //    // paramètre
            //    shell.AppFrame.Navigate(typeof(ConnexionPage), e.Arguments);
            //}
            Connectivity.LaunchWithoutMenu(typeof(ConnexionPage));
            // Vérifiez que la fenêtre actuelle est active
            Window.Current.Activate();
        }

        /// <summary>
        /// Appelé lorsque la navigation vers une page donnée échoue
        /// </summary>
        /// <param name="sender">Frame à l'origine de l'échec de navigation.</param>
        /// <param name="e">Détails relatifs à l'échec de navigation</param>
        void OnNavigationFailed(object sender, NavigationFailedEventArgs e)
        {
            throw new Exception("Failed to load Page " + e.SourcePageType.FullName);
        }

        /// <summary>
        /// Appelé lorsque l'exécution de l'application est suspendue.  L'état de l'application est enregistré
        /// sans savoir si l'application pourra se fermer ou reprendre sans endommager
        /// le contenu de la mémoire.
        /// </summary>
        /// <param name="sender">Source de la requête de suspension.</param>
        /// <param name="e">Détails de la requête de suspension.</param>
        private void OnSuspending(object sender, SuspendingEventArgs e)
        {
            Core.ViewModels.Connection con = new Core.ViewModels.Connection();
            con.DisconnectUser(null);
            var deferral = e.SuspendingOperation.GetDeferral();
            //TODO: enregistrez l'état de l'application et arrêtez toute activité en arrière-plan
            deferral.Complete();
        }

        private void OnResuming(object sender, object e)
        {
            Connectivity.LaunchWithoutMenu(typeof(ConnexionPage));
        }

    }
}

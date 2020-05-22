using MiniTC.ViewModel.BaseClass;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace MiniTC.ViewModel
{
    using Model;
    class MainVM : ViewModelBase
    {
        Panel panel = new Panel();

        #region Lewy panel
        private string leftdirectory = null;
        //Uaktualnij lewy panel
        public string LeftDirectory
        {
            get { return leftdirectory; }
            set
            {
                leftdirectory = value;
                onPropertyChanged(nameof(CurrentLeftFiles));
                onPropertyChanged(nameof(LeftDirectory));
            }
        }
        //Dodaj zawartość ścieżki do lewego panelu
        public ObservableCollection<string> CurrentLeftFiles
        {
            get 
            {
                return new ObservableCollection<string> (panel.GetAllFiles(LeftDirectory));
            }
        }
        //Zmiana zaznaczenia w lewym panelu
        private ICommand leftchange = null;
        public ICommand LeftChceck
        {
            get
            {
                if (leftchange == null)
                {
                    leftchange = new RelayCommand(
                        x => {
                            LeftDirectory = panel.ChangePath
                         (LeftDirectory, SelectedFile);
                        },
                        x => true);
                }
                return leftchange;
            }
        }
        #endregion

        #region Prawy panel
        private string rightdirectory = null;
        //Uaktualnij prawy panel
        public string RightDirectory
        {
            get { return rightdirectory; }
            set
            {
                rightdirectory = value;
                onPropertyChanged(nameof(CurrentRightFiles));
                onPropertyChanged(nameof(RightDirectory));
            }
        }
        //Dodaj zawartość ścieżki do prawego panelu
        public ObservableCollection<string> CurrentRightFiles
        {
            get
            {
                return new ObservableCollection<string>
                    (panel.GetAllFiles(RightDirectory));
            }
        }
        //Zmiana zaznaczenia w prawym panelu
        private ICommand rightcheck = null;
        public ICommand RightCheck
        {
            get
            {
                if (rightcheck == null)
                {
                    rightcheck = new RelayCommand(
                    (arg) => {
                        RightDirectory = panel.ChangePath
                 (RightDirectory, SelectedFile);
                    },
                    (arg) => true
                    );
                }
                return rightcheck;
            }

        }
        #endregion

        #region Funkcje
        //Zwróć dostępne dyski
        public ObservableCollection<string> CurrentDrives
        {
            get
            {
                return new ObservableCollection<string>(panel.GetAllDrives());
            }
        }
        //Zwróć wybrany plik
        public string SelectedFile { get; set; }
        //Kopiowanie pliku
        private ICommand _copy = null;
        public ICommand Copy
        {
            get
            {
                if (_copy == null)
                {
                    _copy = new RelayCommand(x =>
                    {
                        if (RightDirectory != null)
                        {
                            string source = LeftDirectory + @"\" + SelectedFile;
                            string destination = rightdirectory + @"\" + SelectedFile;
                            panel.CopyFile(source, destination);
                        }
                        onPropertyChanged(nameof(CurrentRightFiles));
                    },
                    x => true);
                }
                return _copy;
            }
        }
        #endregion
    }
}

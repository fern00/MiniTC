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
        private string _currentLeftDirectory = null;
        //Uaktualnij lewy panel
        public string CurrentLeftDirectory
        {
            get { return _currentLeftDirectory; }
            set
            {
                _currentLeftDirectory = value;
                onPropertyChanged(nameof(CurrentLeftFiles));
                onPropertyChanged(nameof(CurrentLeftDirectory));
            }
        }
        //Dodaj zawartość ścieżki do lewego panelu
        public ObservableCollection<string> CurrentLeftFiles
        {
            get
            {
                return new ObservableCollection<string> (panel.GetAllFiles(CurrentLeftDirectory));
            }
        }
        //Zmiana zaznaczenia w lewym panelu
        private ICommand _leftClick = null;
        public ICommand LeftClick
        {
            get
            {
                if (_leftClick == null)
                {
                    _leftClick = new RelayCommand(
                        x => {
                            CurrentLeftDirectory = panel.ChangePath
                         (CurrentLeftDirectory, SelectedFile);
                        },
                        x => true);
                }
                return _leftClick;
            }
        }
        #endregion

        #region Prawy panel
        private string _currentRightDirectory = null;
        //Uaktualnij prawy panel
        public string CurrentRightDirectory
        {
            get { return _currentRightDirectory; }
            set
            {
                _currentRightDirectory = value;
                onPropertyChanged(nameof(CurrentRightFiles));
                onPropertyChanged(nameof(CurrentRightDirectory));
            }
        }
        //Dodaj zawartość ścieżki do prawego panelu
        public ObservableCollection<string> CurrentRightFiles
        {
            get
            {
                return new ObservableCollection<string>
                    (panel.GetAllFiles(CurrentRightDirectory));
            }
        }
        //Zmiana zaznaczenia w prawym panelu
        private ICommand _rightClick = null;
        public ICommand RightClick
        {
            get
            {
                if (_rightClick == null)
                {
                    _rightClick = new RelayCommand(
                    (arg) => {
                        CurrentRightDirectory = panel.ChangePath
                 (CurrentRightDirectory, SelectedFile);
                    },
                    (arg) => true
                    );
                }
                return _rightClick;
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
                        if (CurrentRightDirectory != null)
                        {
                            string source = CurrentLeftDirectory + @"\" + SelectedFile;
                            string destination = _currentRightDirectory + @"\" + SelectedFile;
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

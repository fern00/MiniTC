﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace MiniTC.ViewModel.BaseClass
{
    class ViewModelBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected void onPropertyChanged(params string[] namesOfProperties)
        {
            if (PropertyChanged != null)
                foreach (var prop in namesOfProperties)
                    PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }

    }
}
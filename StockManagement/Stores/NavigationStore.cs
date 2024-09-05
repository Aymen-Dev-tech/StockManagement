﻿using StockManagement.ViewModels;
using System;

namespace StockManagement.Stores
{
    public class NavigationStore
    {
        public event Action CurrentViewModelChanged;
        private  ViewModelBase _currentViewModel;
        public ViewModelBase CurrentViewModel 
        { 
            get => _currentViewModel;
            set
            {
                _currentViewModel = value;
                OnCurrentViewModelChanged();
            }
        }

        private void OnCurrentViewModelChanged()
        {
            CurrentViewModelChanged?.Invoke();
        }
    }
}

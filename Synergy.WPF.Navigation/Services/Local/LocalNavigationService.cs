﻿using CommunityToolkit.Mvvm.ComponentModel;
using Synergy.WPF.Navigation.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Synergy.WPF.Navigation.Services.Local
{
    public class LocalNavigationService : ObservableObject, ILocalNavigationService
    {
        private ViewModel? _currentView;
        public ViewModel? CurrentView
        {
            get => _currentView;
            private set => SetProperty(ref _currentView, value);
        }

        public void NavigateTo<TViewModel>(params object[] prms) where TViewModel : ViewModel
        {
            var constructor = GetSuitableConstructor<TViewModel>(prms);

            CurrentView = (ViewModel)constructor.Invoke(prms);
        }

        public void NavigateTo<TViewModel>() where TViewModel : ViewModel
        {
            var constructor = GetSuitableConstructor<TViewModel>(Array.Empty<object>());

            CurrentView = (ViewModel)constructor.Invoke(null);
        }

        public void NavigateTo(ViewModel viewModel)
        {
            CurrentView = viewModel;
        }

        private ConstructorInfo GetSuitableConstructor<T>(object[] prms)
        {
            var constructors = typeof(T).GetConstructors();

            if(constructors.Length == 0)
            {
                throw new InvalidOperationException("Class has no constructors!");
            }

            foreach(var constructor in constructors)
            {
                if (!constructor.IsPublic)
                    continue;

                if(CompareConstructorParameters(constructor.GetParameters(), prms))
                    return constructor;
            }

            throw new InvalidOperationException("There is no suitable constructor for specified parameters!");
        }

        private bool CompareConstructorParameters(object[] prms1, object[] prms2)
        {
            if(prms1.Length != prms2.Length)
                return false;

            for(int i = 0; i < prms1.Length; i++)
            {
                if (prms1[i].GetType() != prms2[i].GetType())
                    return false;
            }

            return true;
        }
    }
}
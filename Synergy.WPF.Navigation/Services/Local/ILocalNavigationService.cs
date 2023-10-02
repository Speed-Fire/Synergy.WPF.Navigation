﻿using Synergy.WPF.Navigation.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Synergy.WPF.Navigation.Services.Local
{
    public interface ILocalNavigationService : INavigationService
    {
        void NavigateTo<TViewModel>(params object[] prms) where TViewModel : ViewModel;
    }
}

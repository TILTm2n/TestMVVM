using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestingMVVM.ViewModels.Base;

namespace TestingMVVM.ViewModels
{
    internal class MainWindowViewModel : ViewModel
    {
        #region Ввод страны
        private string _CountryName;

        public string CountryName
        {
            get => _CountryName;
            set => _CountryName = value;
        }
        #endregion
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TestingMVVM.ViewModels.Base;
using TestingMVVM.Infrastructure.Commands;

namespace TestingMVVM.ViewModels
{
    internal class MainWindowViewModel : ViewModel
    {
        #region Поле ввода названия страны

        private string _PartialUrl;

        public string PartialUrl
        {
            get => _PartialUrl;
            set => _PartialUrl = value;
        }

        #endregion

        #region Команды

        #region SendHttpCommand

        public ICommand SendHttpCommand { get; }

        private bool CanSendHttpCommandExecute(object p) => true;

        private void OnSendHttpCommandExecute(object p)
        {

        }

        #endregion

        #endregion

        public MainWindowViewModel()
        {

            #region Команды

            SendHttpCommand = new LambdaCommand(OnSendHttpCommandExecute, CanSendHttpCommandExecute);

            #endregion

        }

    }
}

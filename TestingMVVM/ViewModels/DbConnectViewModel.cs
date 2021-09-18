using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TestingMVVM.ViewModels.Base;
using TestingMVVM.Infrastructure.Commands;
using TestingMVVM.Model.Methods;
using System.Collections.ObjectModel;
using TestingMVVM.Model.ModelOfCountry;
using System.Windows.Threading;
using System.Windows;
using TestingMVVM.View.Windows;

namespace TestingMVVM.ViewModels
{
    class DbConnectViewModel : ViewModel
    {

        #region Команды

        #region Подключение к БД

        public ICommand ConnectDbCommand { get; }

        private bool CanConnectDbExecute(object p) => true;

        private void OnConnectDbExecute(object p)
        {
            DbConnect dbConnect = new DbConnect();

            dbConnect.Hide();
        }

        #endregion

        #endregion

        public DbConnectViewModel()
        {
            ConnectDbCommand = new LambdaCommand(OnConnectDbExecute, CanConnectDbExecute);
        }

    }
}

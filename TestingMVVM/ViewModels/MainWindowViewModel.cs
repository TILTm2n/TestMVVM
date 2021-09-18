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
using TestingMVVM.Model.DataBase.DBContext;
using System.ComponentModel;

namespace TestingMVVM.ViewModels
{
    internal class MainWindowViewModel : ViewModel
    {
        #region Поля и свойства

        #region Источник БД

        private string _DataSource;

        public string DataSource
        {
            get => _DataSource;
            set => Set(ref _DataSource, value);
        }

        #endregion

        #region Название БД

        private string _Catalog;

        public string Catalog
        {
            get => _Catalog;
            set => Set(ref _Catalog, value);
        }

        #endregion

        #region Видимость чекбокса

        private Visibility _VisibilityValue;

        public Visibility VisibilityValue
        {
            get => _VisibilityValue;
            set => Set(ref _VisibilityValue, value);
        }

        #endregion

        #region Коллекция найденныйх стран

        private ObservableCollection<CountryModel> _Countries;

        public ObservableCollection<CountryModel> Countries
        {
            get => _Countries;
            set => Set(ref _Countries, value);
        }

        #endregion

        #region Название страны

        private string _PartialUrl;

        public string PartialUrl
        {
            get => _PartialUrl;
            set
            {
                if (string.IsNullOrEmpty(value) || string.IsNullOrWhiteSpace(value))
                    throw new Exception("Value cannot be empty");

                Set(ref _PartialUrl, value);
            }
        }

        #endregion

        #endregion

        #region Команды

        #region GetCountriesCommand

        public ICommand GetCountriesCommand { get; }

        private bool CanGetCountriesCommandExecute(object p) => true;

        private async void OnGetCountriesCommandExecute(object p)
        {
            VisibilityValue = Visibility.Visible;

            SendHttp sendHttp = new SendHttp();

            Countries = await sendHttp.HttpResponse(PartialUrl);
        }

        #endregion

        #region AddToDataBaseCommand

        public ICommand AddToDataBaseCommand { get; }

        private bool CanAddToDataBaseCommandExecute(object p) => true;

        private async void OnAddToDataBaseCommandExecute(object p)
        {

            await Task.Run(() =>
            {
                AddToDB addToDB = new AddToDB();

                addToDB.AddToDb(Countries);
            });
        }

        #endregion

        #region DeleteAllDataCommand

        public ICommand DeleteAllDataCommand { get; }

        private bool CanDeleteAllDataExecute(object p) => true;

        private async void OnDeleteAllDataExecute(object p)
        {
            await Task.Run(() =>
            {
                DeleteData deleteData = new DeleteData();
                ShowData showData = new ShowData();

                deleteData.DeleteAllData();
                Countries = showData.ShowAllData();
            });
        }

        #endregion

        #region ShowAllDataCommand

        public ICommand ShowAllDataCommand { get; }

        private bool CanShowAllDataExecute(object p) => true;

        private async void OnShowAllDataExecute(object p)
        {
            VisibilityValue = Visibility.Collapsed;

            await Task.Run(() => {

                ShowData showData = new ShowData();

                //Dispatcher.BeginInvoke((Action)(() => Countries = showData.ShowAllData(Countries)));

                Countries = showData.ShowAllData();
            });
        }

        #endregion

        #region ConnectToDataBaseCommand

        public ICommand ConnectToDataBaseCommand { get; }

        private bool CanConnectToDataBaseExecute(object p) => true;

        private void OnConnectToDataBaseExecute(object p)
        {
            CountriesContext.SourceDB = DataSource;

            CountriesContext.CatalogDB = Catalog;
            
            
        }

        #endregion

        #endregion

        public MainWindowViewModel()
        {

            #region Команды

            GetCountriesCommand = new LambdaCommand(OnGetCountriesCommandExecute, CanGetCountriesCommandExecute);

            AddToDataBaseCommand = new LambdaCommand(OnAddToDataBaseCommandExecute, CanAddToDataBaseCommandExecute);

            DeleteAllDataCommand = new LambdaCommand(OnDeleteAllDataExecute, CanDeleteAllDataExecute);

            ShowAllDataCommand = new LambdaCommand(OnShowAllDataExecute, CanShowAllDataExecute);

            ConnectToDataBaseCommand = new LambdaCommand(OnConnectToDataBaseExecute, CanConnectToDataBaseExecute);

            #endregion

        }

    }
}

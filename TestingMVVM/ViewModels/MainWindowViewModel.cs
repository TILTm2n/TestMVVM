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
using System.Text.RegularExpressions;
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

        #region Индикатор подключения к БД

        private string _DbColor = "Red";

        public string DbColor
        {
            get => _DbColor;
            set => Set(ref _DbColor, value);
        }

        #endregion

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
            set => Set(ref _PartialUrl, value);

        }

        #endregion

        #endregion

        #region Команды

        #region GetCountriesCommand

        public ICommand GetCountriesCommand { get; }

        private bool CanGetCountriesCommandExecute(object p) => true;

        private async void OnGetCountriesCommandExecute(object p)
        {
            try
            {
                VisibilityValue = Visibility.Visible;

                SendHttp sendHttp = new SendHttp();

                Countries = await sendHttp.HttpResponse(PartialUrl);
            }
            catch(Exception HttpEx)
            {
                MessageBox.Show(HttpEx.Message);
            }

            
        }

        #endregion

        #region AddToDataBaseCommand

        public ICommand AddToDataBaseCommand { get; }

        private bool CanAddToDataBaseCommandExecute(object p) => true;

        private async void OnAddToDataBaseCommandExecute(object p)
        {

            await Task.Run(() =>
            {
                try
                {
                    AddToDB addToDB = new AddToDB();

                    addToDB.AddToDb(Countries);
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                
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
                try
                {
                    DeleteData deleteData = new DeleteData();
                    ShowData showData = new ShowData();

                    deleteData.DeleteAllData();
                    Countries = showData.ShowAllData();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            });
        }

        #endregion

        #region ShowAllDataCommand

        public ICommand ShowAllDataCommand { get; }

        private bool CanShowAllDataExecute(object p) => true;

        private async void OnShowAllDataExecute(object p)
        {
            VisibilityValue = Visibility.Hidden;

            await Task.Run(() => {

                try
                {
                    ShowData showData = new ShowData();

                    //Dispatcher.BeginInvoke((Action)(() => Countries = showData.ShowAllData(Countries)));

                    Countries = showData.ShowAllData();
                }
                catch(Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            });
        }

        #endregion

        #region ConnectToDataBaseCommand

        public ICommand ConnectToDataBaseCommand { get; }

        private bool CanConnectToDataBaseExecute(object p) => true;

        private async void OnConnectToDataBaseExecute(object p)
        {

            await Task.Run(() => {
                try
                {
                    if (string.IsNullOrWhiteSpace(DataSource) || string.IsNullOrWhiteSpace(Catalog) || string.IsNullOrEmpty(DataSource) || string.IsNullOrEmpty(Catalog))
                        throw new Exception("Необходимо заполнить данные конфигурации базы данных");

                    CountriesContext.SourceDB = DataSource;

                    CountriesContext.CatalogDB = Catalog;

                    DbColor = "Green";
                }
                catch (Exception DbConnectException)
                {
                    MessageBox.Show(DbConnectException.Message);
                }
            });
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

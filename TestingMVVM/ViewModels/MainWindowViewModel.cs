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

namespace TestingMVVM.ViewModels
{
    internal class MainWindowViewModel : ViewModel
    {
        #region Поля и свойства

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

        public ICommand GetCountriesCommand { get;}

        private bool CanGetCountriesCommandExecute(object p) => true;

        private async void OnGetCountriesCommandExecute(object p)
        {
            SendHttp sendHttp = new SendHttp();

            Countries = await sendHttp.HttpResponse(PartialUrl);

        }

        #endregion

        #endregion

        public MainWindowViewModel()
        {

            #region Команды

            GetCountriesCommand = new LambdaCommand(OnGetCountriesCommandExecute, CanGetCountriesCommandExecute);

            #endregion

        }

    }
}

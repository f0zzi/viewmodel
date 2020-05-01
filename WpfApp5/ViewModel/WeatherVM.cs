using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using WpfApp5.Model;
using WpfApp5.ViewModel.Commands;
using WpfApp5.ViewModel.Helpers;

namespace WpfApp5.ViewModel
{
    public class WeatherVM : INotifyPropertyChanged
    {
        private CurrentCondition currentCondition;
        private City selectedCity;
        public ObservableCollection<City> Cities { get; set; }
        public SearchCommand SearchCommand { get; set; }
        public WeatherVM()
        {
            SearchCommand = new SearchCommand(this);
            Cities = new ObservableCollection<City>();
            if (DesignerProperties.GetIsInDesignMode(new System.Windows.DependencyObject()))
            {
                SelectedCity = new City
                {
                    LocalizedName = "Rivne"
                };
                currentCondition = new CurrentCondition
                {
                    WeatherText = "Sunny",
                    Temperature = new Temperature
                    {
                        Metric = new Units
                        {
                            Value = 11
                        }
                    }
                };
            }
        }
        public async void GetCurrentCondition()
        {
            CurrentCondition = await WeatherHelper.GetCurrentCondition(SelectedCity.Key);
        }
        public City SelectedCity
        {
            get { return selectedCity; }
            set
            {
                selectedCity = value;
                GetCurrentCondition();
                NotifyPropertyChanged();
            }
        }

        public CurrentCondition CurrentCondition
        {
            get { return currentCondition; }
            set
            {
                currentCondition = value;
                NotifyPropertyChanged();
            }
        }

        private string query;

        public string Query
        {
            get { return query; }
            set
            {
                query = value;
                NotifyPropertyChanged();
            }
        }

        public async void MakeRequest()
        {
            var cities = await WeatherHelper.GetCities(Query);
            Cities.Clear();
            foreach (var item in cities)
            {
                Cities.Add(item);
            }
            Query = String.Empty; // ""
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void NotifyPropertyChanged([CallerMemberName]string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

using CTWO80_HFT_2022232.Models;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using MovieDbApp.WpfClient;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace TeamDbApp.WpfClient
{
    public class ManagerWindowViewModel :ObservableRecipient
    {
        public RestCollection<Manager> Managers { get; set; }
        private Manager selectedManager;

        public Manager SelectedManager
        {
            get { return selectedManager; }
            set
            {
                if (value != null)
                {
                    selectedManager = new Manager()
                    {
                        ManagerName = value.ManagerName,
                        ManagerId = value.ManagerId,
                        ManagerAge = value.ManagerAge,
                        IsBold = value.IsBold
                       
                    };

                }
               
                OnPropertyChanged();
                (DeleteManagerCommand as RelayCommand).NotifyCanExecuteChanged();
                (CreateManagerCommand as RelayCommand).NotifyCanExecuteChanged();
                (UpdateManagerCommand as RelayCommand).NotifyCanExecuteChanged();
            }
        }
        public ICommand CreateManagerCommand { get; set; }
        public ICommand UpdateManagerCommand { get; set; }
        public ICommand DeleteManagerCommand { get; set; }
        public static bool IsInDesignMode
        {
            get
            {
                var prop = DesignerProperties.IsInDesignModeProperty;
                return (bool)DependencyPropertyDescriptor.FromProperty(prop, typeof(FrameworkElement)).Metadata.DefaultValue;
            }

        }
        public ManagerWindowViewModel()
        {
            if (!IsInDesignMode)
            {


                Managers = new RestCollection<Manager>("http://localhost:29829/", "Manager", "hub");
                CreateManagerCommand = new RelayCommand(()
                    =>
                {
                    Managers.Add(new Manager()
                    {
                        ManagerName = selectedManager.ManagerName,
                       
                        ManagerAge = selectedManager.ManagerAge,
                       
                        IsBold = selectedManager.IsBold


                    }, "http://localhost:29829/Manager");
                });
                UpdateManagerCommand = new RelayCommand(() =>
                {
                    Managers.Update(selectedManager, "http://localhost:29829/Manager");
                });

                DeleteManagerCommand = new RelayCommand(() =>
                {
                    Managers.Delete(SelectedManager.ManagerId, "http://localhost:29829/Manager");
                },
                () =>
                {
                    return SelectedManager != null;
                });
            }
            selectedManager = new Manager();
        }

    }
}

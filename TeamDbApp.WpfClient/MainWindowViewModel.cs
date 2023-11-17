using CTWO80_HFT_2022232.Models;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using MovieDbApp.WpfClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace TeamDbApp.WpfClient
{
   public class MainWindowViewModel :ObservableRecipient
    {
        public RestCollection<FootballTeam> FootballTeams { get; set;}
        private FootballTeam selectedTeam;

        public FootballTeam SelectedTeam
        {
            get { return selectedTeam; }
            set 
            {
                if (value!=null)
                {
                    selectedTeam = new FootballTeam()
                    {
                        FootballTeamName = value.FootballTeamName,
                        FootballTeamId = value.FootballTeamId,
                        CurrentPlacement= value.CurrentPlacement,
                        TrophiesWon =value.TrophiesWon
                        
                    };

                }
                OnPropertyChanged();
                (DeleteTeamCommand as RelayCommand).NotifyCanExecuteChanged();
                (CreateTeamCommand as RelayCommand).NotifyCanExecuteChanged();
                (UpdateTeamCommand as RelayCommand).NotifyCanExecuteChanged();
            }
        }
        public static bool IsInDesignMode
        {
            get
            {
                var prop = DesignerProperties.IsInDesignModeProperty;
                return (bool)DependencyPropertyDescriptor.FromProperty(prop, typeof(FrameworkElement)).Metadata.DefaultValue;
            }

        }
     


        public ICommand CreateTeamCommand { get; set; }
        public ICommand UpdateTeamCommand { get; set; }
        public ICommand DeleteTeamCommand { get; set; }

        public MainWindowViewModel()
        {
            if (!IsInDesignMode)
            {
               

                FootballTeams = new RestCollection<FootballTeam>("http://localhost:29829/", "FootballTeam", "hub");
                CreateTeamCommand = new RelayCommand(()
                    =>
                {
                    FootballTeams.Add(new FootballTeam()
                    {
                        FootballTeamName = selectedTeam.FootballTeamName,
                        CurrentPlacement = selectedTeam.CurrentPlacement,
                        TrophiesWon=selectedTeam.TrophiesWon
                       

                    });
                });
                UpdateTeamCommand = new RelayCommand(()=>
                {
                    FootballTeams.Update(selectedTeam);
                });

                DeleteTeamCommand = new RelayCommand(() =>
                {
                    FootballTeams.Delete(SelectedTeam.FootballTeamId);
                },
                () =>
                {
                    return SelectedTeam != null;
                });
            }
            selectedTeam = new FootballTeam();
        }


    }
}

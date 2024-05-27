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
        private RestCollection<FootballTeam> boldManagers;

        public RestCollection<FootballTeam> BoldManagers
        {
            get { return boldManagers; }
            set
            {
                if (value != boldManagers)
                {
                    boldManagers = value;
                    OnPropertyChanged();
                }
            }
        }
        private IEnumerable<KeyValuePair<string, int>> playerCountResult;

        public IEnumerable<KeyValuePair<string, int>> PlayerCountResult
        {
            get { return playerCountResult; }
            set
            {
                if (value != playerCountResult)
                {
                    playerCountResult = value;
                    OnPropertyChanged();
                }
            }
        }
        private IEnumerable<KeyValuePair<string, int>> oldManagersTeamname;

        public IEnumerable<KeyValuePair<string, int>> OldManagersTeamname
        {
            get { return oldManagersTeamname; }
            set
            {
                if (value != oldManagersTeamname)
                {
                    oldManagersTeamname = value;
                    OnPropertyChanged();
                }
            }
        }

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
                        TrophiesWon =value.TrophiesWon,
                        Manager =value.Manager
                        
                    };

                }
                OnPropertyChanged();
                (DeleteTeamCommand as RelayCommand).NotifyCanExecuteChanged();
                (CreateTeamCommand as RelayCommand).NotifyCanExecuteChanged();
                (UpdateTeamCommand as RelayCommand).NotifyCanExecuteChanged();
                (BoldManagersCommand as RelayCommand).NotifyCanExecuteChanged();
                (PlayerCountCommand as RelayCommand).NotifyCanExecuteChanged();
                (OldManagersCommand as RelayCommand).NotifyCanExecuteChanged();
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
        public ICommand BoldManagersCommand { get; set; }
        public ICommand PlayerCountCommand { get; set; }
        public ICommand OldManagersCommand { get; set; }

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
                        TrophiesWon=selectedTeam.TrophiesWon,
                        Manager =selectedTeam.Manager,

                       

                    }, "http://localhost:29829/FootballTeam");
                });
                UpdateTeamCommand = new RelayCommand(()=>
                {
                    FootballTeams.Update(selectedTeam, "http://localhost:29829/FootballTeam");
                });

                DeleteTeamCommand = new RelayCommand(() =>
                {
                    FootballTeams.Delete(SelectedTeam.FootballTeamId, "http://localhost:29829/FootballTeam");
                },
                () =>
                {
                    return SelectedTeam != null;
                });
                BoldManagersCommand = new RelayCommand(() =>
                {
                    BoldManagers = FootballTeams.BoldManagersNonCrud("http://localhost:29829/noncrud/BoldManagersTeamName");
                });
                PlayerCountCommand = new RelayCommand(() =>
                {
                    PlayerCountResult= FootballTeams.TeamPlayersCount("http://localhost:29829/noncrud/TeamPlayersCount");
                });
                OldManagersCommand = new RelayCommand(() =>
                {
                    OldManagersTeamname = FootballTeams.OldManagersNonCrud("http://localhost:29829/noncrud/OldManagersTeamName");
                });

            }
            selectedTeam = new FootballTeam();
        }


    }
}

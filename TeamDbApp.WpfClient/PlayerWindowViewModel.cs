using CTWO80_HFT_2022232.Endpoint.Controllers;
using CTWO80_HFT_2022232.Logic;
using CTWO80_HFT_2022232.Models;
using CTWO80_HFT_2022232.Repository;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using MovieDbApp.WpfClient;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace TeamDbApp.WpfClient
{
    public class PlayerWindowViewModel:ObservableRecipient
    {
        public RestCollection<Player> Players { get; set; }
        private Player selectedPlayer;
        private IEnumerable<KeyValuePair<string, int>> trophiesByPositionResult;

        public IEnumerable<KeyValuePair<string, int>> TrophiesByPositionResult
        {
            get { return trophiesByPositionResult; }
            set
            {
                if (value != trophiesByPositionResult)
                {
                    trophiesByPositionResult = value;
                    OnPropertyChanged();
                }
            }
        }
        public Player SelectedPlayer
        {
            get { return selectedPlayer; }
            set
            {
                if (value != null)
                {
                    selectedPlayer = new Player()
                    {
                        PlayerId =value.PlayerId,
                        PlayerName = value.PlayerName,
                        PlayerPosition= value.PlayerPosition,
                        FootballTeamId=value.FootballTeamId
                       
                        
                    };

                }

                OnPropertyChanged();
                (DeletePlayerCommand as RelayCommand).NotifyCanExecuteChanged();
                (CreatePlayerCommand as RelayCommand).NotifyCanExecuteChanged();
                (UpdatePlayerCommand as RelayCommand).NotifyCanExecuteChanged();
                (TrophiesByPosition as RelayCommand).NotifyCanExecuteChanged();
            }
        }
        public ICommand CreatePlayerCommand { get; set; }
        public ICommand UpdatePlayerCommand { get; set; }
        public ICommand DeletePlayerCommand { get; set; }

        public ICommand TrophiesByPosition { get; set; }
        public static bool IsInDesignMode
        {
            get
            {
                var prop = DesignerProperties.IsInDesignModeProperty;
                return (bool)DependencyPropertyDescriptor.FromProperty(prop, typeof(FrameworkElement)).Metadata.DefaultValue;
            }

        }
        public PlayerWindowViewModel()
        {
            if (!IsInDesignMode)
            {


                Players = new RestCollection<Player>("http://localhost:29829/", "Player", "hub");
                CreatePlayerCommand = new RelayCommand(()
                    =>
                {
                    Players.Add(new Player()
                    {

                        PlayerName = selectedPlayer.PlayerName,
                        PlayerPosition = selectedPlayer.PlayerPosition,
                        FootballTeamId = selectedPlayer.FootballTeamId

                    }, "http://localhost:29829/Player");
                });
                UpdatePlayerCommand = new RelayCommand(() =>
                {
                    Players.Update(selectedPlayer, "http://localhost:29829/Player");
                });

                DeletePlayerCommand = new RelayCommand(() =>
                {
                    Players.Delete(SelectedPlayer.PlayerId, "http://localhost:29829/Player");
                },
                () =>
                {
                    return SelectedPlayer != null;
                });
                TrophiesByPosition = new RelayCommand(async () =>
                {
                    TrophiesByPositionResult = Players.GetNonCrudData("http://localhost:29829/PlayerNonCrud/ThrophiesByPosition");        
                });



            }
            selectedPlayer = new Player();
        }

    

    }
}

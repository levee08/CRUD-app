using CTWO80_HFT_2022232.Models;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using MovieDbApp.WpfClient;
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
    public class PlayerWindowViewModel:ObservableRecipient
    {
        public RestCollection<Player> Players { get; set; }
        private Player selectedPlayer;

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
                        PlayerPosition= value.PlayerPosition

                    };

                }

                OnPropertyChanged();
                (DeletePlayerCommand as RelayCommand).NotifyCanExecuteChanged();
                (CreatePlayerCommand as RelayCommand).NotifyCanExecuteChanged();
                (UpdatePlayerCommand as RelayCommand).NotifyCanExecuteChanged();
            }
        }
        public ICommand CreatePlayerCommand { get; set; }
        public ICommand UpdatePlayerCommand { get; set; }
        public ICommand DeletePlayerCommand { get; set; }
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
                        PlayerPosition = selectedPlayer.PlayerPosition

                    }) ;
                });
                UpdatePlayerCommand = new RelayCommand(() =>
                {
                    Players.Update(selectedPlayer);
                });

                DeletePlayerCommand = new RelayCommand(() =>
                {
                    Players.Delete(SelectedPlayer.PlayerId);
                },
                () =>
                {
                    return SelectedPlayer != null;
                });
            }
            selectedPlayer = new Player();
        }

    }
}

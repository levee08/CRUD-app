using CTWO80_HFT_2022232.Models;
using MovieDbApp.WpfClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace TeamDbApp.WpfClient
{
    class MainWindowViewModel
    {
        public RestCollection<FootballTeam> FootballTeams { get; set;}

        public ICommand CreateTeamCommand { get; set; }
        public ICommand CreateTeamCommand { get; set; }
        public ICommand CreateTeamCommand { get; set; }

        public MainWindowViewModel()
        {
            FootballTeams = new RestCollection<FootballTeam>("http://localhost:29829/", "FootballTeam");
        }

    }
}

using CTWO80_HFT_2022232.Logic;
using CTWO80_HFT_2022232.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CTWO80_HFT_2022232.Endpoint.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class noncrudController : ControllerBase
    {

        IFootballTeamLogic logic;
       
        public noncrudController(IFootballTeamLogic l)
        {
            this.logic = l;
            
        }
      


        [HttpGet]
        public IEnumerable<FootballTeam> BoldManagersTeamName()
        {
           return this.logic.BoldManagersTeamName();
        }

        [HttpGet]
        public IEnumerable<KeyValuePair<string, int>> TeamPlayersCount()
        {
            return this.logic.TeamPlayersCount();
        }

        [HttpGet]
        public IEnumerable<KeyValuePair<string, int>> OldManagersTeamName()
        {
            return this.logic.OldManagersTeamName();
        }

     
    }
}

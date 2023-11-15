using CTWO80_HFT_2022232.Endpoint.Services;
using CTWO80_HFT_2022232.Logic;
using CTWO80_HFT_2022232.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CTWO80_HFT_2022232.Endpoint.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class FootballTeamController : ControllerBase
    {
      

            IFootballTeamLogic logic;
        IHubContext<SignalRHub> hub;

            public FootballTeamController(IFootballTeamLogic logic, IHubContext<SignalRHub> hub)
            {
                this.logic = logic;
                  this.hub = hub;
            }

            [HttpGet]
            public IEnumerable<FootballTeam> ReadAll()
            {
                return this.logic.ReadAll();
            }

            [HttpGet("{id}")]
            public FootballTeam Read(int id)
            {
                return this.logic.Read(id);
            }

            [HttpPost]
            public void Create([FromBody] FootballTeam value)
            {
                this.logic.Create(value);
            this.hub.Clients.All.SendAsync("FootballTeamCreated", value);
            }

            [HttpPut]
            public void Update([FromBody] FootballTeam value)
            {
                this.logic.Update(value);
                 this.hub.Clients.All.SendAsync("FootballTeamUpdated", value);
            }

            [HttpDelete("{id}")]
            public void Delete(int id)
            {
                var TeamToDelete = this.logic.Read(id);
                this.logic.Delete(id);
                this.hub.Clients.All.SendAsync("FootballTeamDeleted", TeamToDelete);
            }

       

       

    }
}

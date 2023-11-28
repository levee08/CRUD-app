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
    public class PlayerController : ControllerBase
    {
        IPlayerLogic logic;
        IHubContext<SignalRHub> hub;
        public PlayerController(IPlayerLogic logic, IHubContext<SignalRHub> hub)
        {
            this.logic = logic;
            this.hub = hub;
        }


        [HttpGet]
        public IEnumerable<Player> ReadAll()
        {
            return this.logic.ReadAll();
        }

        [HttpGet("{id}")]
        public Player Read(int id)
        {
            return this.logic.Read(id);
        }

        [HttpPost]
        public void Create([FromBody] Player value)
        {
            this.logic.Create(value);
            this.hub.Clients.All.SendAsync("PlayerCreated", value);
        }

        [HttpPut]
        public void Update([FromBody] Player value)
        {
            this.logic.Update(value);
            this.hub.Clients.All.SendAsync("PlayerUpdated", value);
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var PlayerToDelete = this.logic.Read(id);
            this.logic.Delete(id);
            this.hub.Clients.All.SendAsync("PlayerDeleted", PlayerToDelete);
        }


       

    }
}

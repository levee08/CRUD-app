using CTWO80_HFT_2022232.Logic;
using CTWO80_HFT_2022232.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace CTWO80_HFT_2022232.Endpoint.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class FootballTeamController : ControllerBase
    {

        IFootballTeamLogic logic;

        public FootballTeamController(IFootballTeamLogic logic)
        {
            this.logic = logic;
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
        }

        [HttpPut]
        public void Update([FromBody] FootballTeam value)
        {
            this.logic.Update(value);
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            this.logic.Delete(id);
        }
    }
}

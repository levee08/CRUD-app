using CTWO80_HFT_2022232.Logic;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CTWO80_HFT_2022232.Endpoint.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class PlayerNonCrudController : ControllerBase
    {

        IPlayerLogic logic;

        public PlayerNonCrudController(IPlayerLogic logic)
        {
            this.logic = logic;
        }

        [HttpGet("{name}")]
        public IEnumerable<KeyValuePair<string, int>> PlayerTrophiesAndPosition(string name)
        {
            return this.logic.PlayerTrophiesAndPosition(name);
        }

        [HttpGet]
        public IEnumerable<KeyValuePair<string, int>> ThrophiesByPosition()
        {
            return this.logic.ThrophiesByPosition();
        }
    }
}

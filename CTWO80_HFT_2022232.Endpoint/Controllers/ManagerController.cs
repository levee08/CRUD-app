using CTWO80_HFT_2022232.Logic;
using CTWO80_HFT_2022232.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace CTWO80_HFT_2022232.Endpoint.Controllers
{

    [Route("[controller]")]
    [ApiController]
    public class ManagerController:ControllerBase
    {

        IManagerLogic logic;

        public ManagerController(IManagerLogic logic)
        {
            this.logic = logic;
        }

        [HttpGet]
        public IEnumerable<Manager> ReadAll()
        {
            return this.logic.ReadAll();
        }

        [HttpGet("{id}")]
        public Manager Read(int id)
        {
            return this.logic.Read(id);
        }

        [HttpPost]
        public void Create([FromBody] Manager value)
        {
            this.logic.Create(value);
        }

        [HttpPut]
        public void Update([FromBody] Manager value)
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

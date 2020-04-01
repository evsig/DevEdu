using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DevEdu.Data.Storages;
using DevEdu.Data;
using DevEdu.Data.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Authorization;
using DevEdu.Common;
using DevEdu.Models.Mappings;
using DevEdu.Models.InputModels;

namespace DevEdu.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DictionaryController : ControllerBase
    {
        private readonly DictionaryStorage dictionaryStorage;
        public DictionaryController(IConfiguration Configuration)
        {
            string dbCon = Configuration.GetConnectionString("DefaultConnection");
            dictionaryStorage = new DictionaryStorage(dbCon);
        }
        #region City Controller

        [Authorize]
        [HttpGet]
        [Route("city")]
        public async Task<ActionResult<List<City>>> GetAllCities()
        {
            List <City> cities = await dictionaryStorage.CitiesGetAll();
            return Ok(CityMapper.ToOutputModels(cities));
        }

        [Authorize]
        [HttpGet]
        [Route("city/{id}")]
        public async Task<ActionResult<City>> GetCityByID( int id)
        {
            if(id <= 0) return BadRequest(); 
            City city = await dictionaryStorage.CityGetByID(id);
            return Ok(CityMapper.ToOutputModel(city));
        }

        [Authorize]
        [HttpPost]
        [Route("city")]
        public async Task<ActionResult<int?>> AddCity([FromBody] CityInputModel model)
        {
            if (model == null) return BadRequest();
            City dataModel = CityMapper.ToDataModel(model);
            int? id = await dictionaryStorage.CityAddOrUpdate(dataModel);
            return Ok(id);
        }

        [Authorize]
        [HttpPut]
        [Route("city/{id}")]
        public async Task<ActionResult<int?>> UpdateCity([FromBody] CityInputModel model)
        {
            if (model == null) return BadRequest();
            City dataModel = CityMapper.ToDataModel(model);
            int? id = await dictionaryStorage.CityAddOrUpdate(dataModel);
            return Ok(id);
        }

        [Authorize]
        [HttpDelete]
        [Route("city/{id}")]
        public async Task<ActionResult> DeleteCity(int id)
        {
            if (id <= 0) return BadRequest();
            await dictionaryStorage.CityDelete(id);
            return Ok();
        }

        #endregion

        #region Role Controller

        [Authorize]
        [HttpGet]
        [Route("role")]
        public async Task<ActionResult<List<Role>>> RoleGetAll()
        {
            List<Role> roles = await dictionaryStorage.RoleGetAll();
            return Ok(RoleMapper.ToOutputModels(roles));
        }

        [Authorize]
        [HttpGet]
        [Route("role/{id}")]
        public async Task<ActionResult<Role>> RoleGetById(int id)
        {
            if (id < 0) return BadRequest();
            Role role = await dictionaryStorage.RoleGetByID(id);
            return Ok(role);
        }

        [Authorize]
        [HttpPost]
        [Route("role")]
        public async Task<ActionResult<int>> RoleAdd([FromBody] RoleInputModel model)
        {
            if (model == null) return BadRequest();
            Role dataModel = RoleMapper.ToDataModel(model);
            int id = (int)await dictionaryStorage.RoleAddOrUpdate(dataModel);
            return Ok(id);
        }

        [Authorize]
        [HttpPut]
        [Route("role/{id}")]
        public async Task<ActionResult<int>> UpdateRole([FromBody] RoleInputModel model)
        {
            if (model == null) return BadRequest();
            Role dataModel = RoleMapper.ToDataModel(model);
            int id = (int)await dictionaryStorage.RoleAddOrUpdate(dataModel);
            return Ok(id);
        }

        [Authorize]
        [HttpDelete]
        [Route("role/{id}")]
        public async Task<ActionResult> RoleDelete(int id)
        {
            if (id < 0) return BadRequest();
            await dictionaryStorage.RoleDelete(id);
            return Ok();
        }
        #endregion
    }
}
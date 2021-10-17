using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DataAccess.Concrete;
using Entities;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using Service.Abstract;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonalsController : ControllerBase
    {
        private readonly IPersonalService _personalService;
        private readonly IDistributedCache _distributedCache;



        public PersonalsController(/*CRUD_Context context,*/ IDistributedCache distributedCache, IPersonalService personalService)
        {
            _distributedCache = distributedCache;
            _personalService = personalService;
        }

        // GET: api/Personals
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Personal>>> GetPersonals()
        {
            //redis query
            #region 
            //if (string.IsNullOrEmpty(_distributedCache.GetString("personals")))
            //{
            //    var personals = _context.Personals.ToListAsync();
            //    var personalsString = JsonConvert.SerializeObject(personals);
            //    _distributedCache.SetString("employees", personalsString);
            //}
            //else
            //{
            //    var personalFromCache = _distributedCache.GetString("personals");
            //    var personals = JsonConvert.DeserializeObject<List<Personal>>(personalFromCache);
            //}
            #endregion 
            return _personalService.GetAll();

        }

        // GET: api/Personals/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Personal>> GetPersonal(int id)
        {
            var personal = _personalService.GetById(id);

            if (personal == null)
            {
                return NotFound();
            }
            //redis query
            #region
            //if (string.IsNullOrEmpty(_distributedCache.GetString("personals")))
            //{
            //    var personals = _context.Personals.FindAsync(id);
            //    var personalsString = JsonConvert.SerializeObject(personals);
            //    _distributedCache.SetString("employees", personalsString);
            //}
            //else
            //{
            //    var personalFromCache = _distributedCache.GetString("personals");
            //    var personals = JsonConvert.DeserializeObject<List<Personal>>(personalFromCache);
            //}
            #endregion


            return personal;
        }

        //// PUT: api/Personals/5
        //// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPersonal(int id, Personal personal)
        {
            if (id != personal.Id)
            {
                return BadRequest();
            }
            //redis query
            #region
            //if (string.IsNullOrEmpty(_distributedCache.GetString("personals")))
            //{
            //    var personals = _context.Entry(personal).State = EntityState.Modified;
            //    var personalsString = JsonConvert.SerializeObject(personals);
            //    _distributedCache.SetString("employees", personalsString);
            //}
            //else
            //{
            //    var personalFromCache = _distributedCache.GetString("personals");
            //    var personals = JsonConvert.DeserializeObject<List<Personal>>(personalFromCache);
            //}
            #endregion

            try
            {
                _personalService.Update(personal);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (personal.Id==null)
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        //// POST: api/Personals
        //// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Personal>> PostPersonal(Personal personal)
        {
            _personalService.Create(personal);
            //redis query
            #region
            //if (string.IsNullOrEmpty(_distributedCache.GetString("personals")))
            //{
            //    var personals = _context.Personals.Add(personal);
            //    var personalsString = JsonConvert.SerializeObject(personals);
            //    _distributedCache.SetString("employees", personalsString);
            //}
            //else
            //{
            //    var personalFromCache = _distributedCache.GetString("personals");
            //    var personals = JsonConvert.DeserializeObject<List<Personal>>(personalFromCache);
            //}
            #endregion       

            return CreatedAtAction("GetPersonal", new { id = personal.Id }, personal);
        }

        //// DELETE: api/Personals/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePersonal(int id)
        {
            var personal =  _personalService.GetById(id);
            if (personal == null)
            {
                return NotFound();
            }

            _personalService.Delete(personal);
            //redis query
            #region
            //if (string.IsNullOrEmpty(_distributedCache.GetString("personals")))
            //{
            //    var personals = _context.Personals.Remove(personal);
            //    var personalsString = JsonConvert.SerializeObject(personals);
            //    _distributedCache.SetString("employees", personalsString);
            //}
            //else
            //{
            //    var personalFromCache = _distributedCache.GetString("personals");
            //    var personals = JsonConvert.DeserializeObject<List<Personal>>(personalFromCache);
            //}

            #endregion          

            return NoContent();
        }

    }
}

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Terraforming.Api.Database;
using Terraforming.Api.Models;

namespace Terraforming.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    public class GameController : ControllerBase
    {
        private MsDataContext _db;
        public GameController(MsDataContext db)
        {
            _db = db;
        }

        [Authorize(Policy = "GameMaster")]
        [HttpPost]
        public IActionResult AddGame([FromBody] Game game)
        {
            try
            {
                game.Date = DateTime.UtcNow;
                game.Updated = DateTime.UtcNow;
                var result = _db.Game.Add(game).Entity;
                _db.SaveChanges();

                return Ok(result);
            }
            catch (Exception exc)
            {
                return BadRequest(exc.Message);
            }
        }

        [Authorize(Policy = "GameMaster")]
        [HttpPut]
        public IActionResult UpdateGame([FromBody] Game game)
        {
            try
            {
                var result = _db.Game.Update(game).Entity;
                _db.SaveChanges();

                return Ok(result);
            }
            catch (Exception exc)
            {
                return BadRequest(exc.Message);
            }
        }

        [Authorize(Policy = "GameMaster")]
        [HttpDelete("{id}")]
        public IActionResult DeleteGame(string id)
        {
            try
            {
                var result = _db.Game.Find(id);
                if (result == null)
                {
                    return BadRequest("The game was not found");
                }
                _db.Game.Remove(result);
                _db.SaveChanges();

                return Ok(result);
            }
            catch (Exception exc)
            {
                return BadRequest(exc.Message);
            }
        }

        [Authorize]
        [HttpGet("{id}")]
        public IActionResult GetGame(string id)
        {
            try
            {
                var result = _db.Game.Find(id);
                if (result == null)
                {
                    return BadRequest("The game was not found");
                }

                return Ok(result);
            }
            catch (Exception exc)
            {
                return BadRequest(exc.Message);
            }
        }

        [Authorize]
        [HttpGet("all")]
        public IActionResult GetGames()
        {
            try
            {
                var result = _db.Game.Where(x => true).ToList();
                if (result == null)
                {
                    return BadRequest("The game was not found");
                }

                return Ok(result);
            }
            catch (Exception exc)
            {
                return BadRequest(exc.Message);
            }
        }

        [Authorize]
        [HttpGet("ratings")]
        public IActionResult GetRatings()
        {
            try
            {
                var result = _db.Users.Include(i => i.GameScores).Where(x => true);
                result = result.OrderByDescending(x => x.GameScores.Count(a => a.Place == 1));
                if (result == null)
                {
                    return BadRequest("No Games Found");
                }

                return Ok(result.ToList());
            }
            catch (Exception exc)
            {
                return BadRequest(exc.Message);
            }
        }
    }
}


using System;
using System.Collections.Generic;
using GlowGlider.Server.Data;
using GlowGlider.Shared;
using Microsoft.AspNetCore.Mvc;

namespace GlowGlider.Server.Controllers
{
    [Route("api/[controller]")]
    public class HighScoresController : Controller
    {
        private readonly IHighScoreRepository _repository;

        public HighScoresController(IHighScoreRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public IReadOnlyList<HighScoreData> Get(string playerId)
        {
            if (string.IsNullOrEmpty(playerId))
            {
                return _repository.GetBestScores();
            }

            return _repository.GetScoresForPlayer(playerId);
        }

        [HttpPost]
        public IActionResult PublishScore([FromBody] PublishRequest request)
        {
            var player = request.PlayerAlias;
            var score = request.Score;

            if (string.IsNullOrWhiteSpace(player))
            {
                return new BadRequestResult();
            }

            if (TokenBuilder.TokenFor(request) == request.Token)
            {
                _repository.InsertScore(new GameData
                {
                    PlayerAlias = player,
                    Score = score,
                    PlayerId = new Guid(request.PlayerId)
                });
            }

            return new OkResult();
        }
    }
}
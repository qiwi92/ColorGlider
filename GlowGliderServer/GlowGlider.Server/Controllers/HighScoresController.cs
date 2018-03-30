using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
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
        public IReadOnlyList<HighScoreData> Get(string playerName)
        {
            if (string.IsNullOrEmpty(playerName))
            {
                return _repository.GetBestScores();
            }

            return _repository.GetScoresForPlayer(playerName);
        }

        [HttpPost]
        public IActionResult PublishScore([FromBody] PublishRequest request)
        {
            var player = request.PlayerName;
            var score = request.Score;

            if (string.IsNullOrWhiteSpace(player))
            {
                return new BadRequestResult();
            }

            if (CalculateHash($"{player}-{score}") == request.Token)
            {
                _repository.InsertScore(player, score);
            }

            return new OkObjectResult(Get(player));
        }

        private string CalculateHash(string input)
        {
            using (var algorithm = SHA512.Create()) 
            {
                var hashedBytes = algorithm.ComputeHash(Encoding.UTF8.GetBytes(input));

                return BitConverter.ToString(hashedBytes)
                    .Replace("-", "")
                    .ToLower()
                    .Substring(0, 16);
            }
        }
    }
}
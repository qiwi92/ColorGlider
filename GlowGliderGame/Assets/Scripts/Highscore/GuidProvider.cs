using System;
using UnityEngine;

namespace Highscore
{
    public class GuidProvider
    {
        public Guid GetGuid()
        {
            var guidString = SavegameService.Instance.Guid;

            if (!string.IsNullOrEmpty(guidString))
                return new Guid(guidString);

            var guid = GenerateGuid();

            Debug.Log("Guid Generated: " + guid);
            SavegameService.Instance.Guid = guid.ToString();

            return guid;
        }

        private Guid GenerateGuid()
        {
            return Guid.NewGuid();
        }
    }
}
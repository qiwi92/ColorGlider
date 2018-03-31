using System;
using UnityEngine;

namespace Highscore
{
    public class GuidProvider
    {
        public Guid GetGuid()
        {
            var guidString = PlayerPrefs.GetString("Guid",null);

            if (!string.IsNullOrEmpty(guidString))
                return new Guid(guidString);

            var guid = GenerateGuid();

            Debug.Log("Guid Generated: " + guid);
            PlayerPrefs.SetString("Guid", guid.ToString());

            return guid;
        }

        private Guid GenerateGuid()
        {
            return Guid.NewGuid();
        }
    }
}
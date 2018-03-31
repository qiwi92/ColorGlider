using System;
using UnityEngine;

namespace Highscore
{
    public class GuidProvider
    {
        public Guid GetGuid()
        {
            var guidString = PlayerPrefsService.Instance.Alias;

            if (!string.IsNullOrEmpty(guidString))
                return new Guid(guidString);

            var guid = GenerateGuid();

            Debug.Log("Guid Generated: " + guid);
            PlayerPrefsService.Instance.Alias = guid.ToString();

            return guid;
        }

        private Guid GenerateGuid()
        {
            return Guid.NewGuid();
        }
    }
}
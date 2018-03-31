using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using GlowGlider.Shared;
using UnityEngine;

namespace Highscore
{
    public class HighScoreModel : IHighScoreModel
    {
        private readonly HighScoreApi _highScoreApi = new HighScoreApi();
        private readonly GuidProvider _guidProvider = new GuidProvider();
        private IReadOnlyList<HighScoreData> _highscoresAroundPlayer;


        public HighScoreModel()
        {
            ServicePointManager.ServerCertificateValidationCallback = MyRemoteCertificateValidationCallback;

            UpdateHighscore();
        }

        private void UpdateHighscore()
        {
            Task.Run(ReadData)
                .ContinueWith(c => _highscoresAroundPlayer = c.Result);
        }

        private async Task<IReadOnlyList<HighScoreData>> ReadData()
        {
            Debug.Log("Calling Api");
            try
            {
                var highScoreDatas = await _highScoreApi.GetTop10Async();
                Debug.Log("Received " + highScoreDatas.Count);
                return highScoreDatas;
            }
            catch (Exception ex)
            {
                Debug.Log(ex);
            }

            return null;
        }

        private Guid PlayerId => _guidProvider.GetGuid();

        private PlayerHighScore MapServerToViewHighscore(HighScoreData from)
        {
            return new PlayerHighScore
            {
                IsPlayer = from.PlayerId == PlayerId,
                PlayerName = from.PlayerName,
                Rank = from.Rank,
                Score = from.Score
            };
        }

        public void UploadHighScore(int highscore,string playerAlias)
        {
            var request = new PublishRequest(PlayerId.ToString(),playerAlias,highscore);
            Task.Run(async () => await _highScoreApi.PublishScore(request)).ContinueWith(res =>
            {
                Debug.Log("Uploaded Highscore! " + request.ToString() );
            });
        }

        public IEnumerable<PlayerHighScore> HighScoresAroundPlayer => _highscoresAroundPlayer.Select(MapServerToViewHighscore);


        public bool MyRemoteCertificateValidationCallback(System.Object sender,
            X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
        {
            bool isOk = true;
            // If there are errors in the certificate chain,
            // look at each error to determine the cause.
            if (sslPolicyErrors != SslPolicyErrors.None)
            {
                for (int i = 0; i < chain.ChainStatus.Length; i++)
                {
                    if (chain.ChainStatus[i].Status == X509ChainStatusFlags.RevocationStatusUnknown)
                    {
                        continue;
                    }
                    chain.ChainPolicy.RevocationFlag = X509RevocationFlag.EntireChain;
                    chain.ChainPolicy.RevocationMode = X509RevocationMode.Online;
                    chain.ChainPolicy.UrlRetrievalTimeout = new TimeSpan(0, 1, 0);
                    chain.ChainPolicy.VerificationFlags = X509VerificationFlags.AllFlags;
                    bool chainIsValid = chain.Build((X509Certificate2)certificate);
                    if (!chainIsValid)
                    {
                        isOk = false;
                        break;
                    }
                }
            }
            return isOk;
        }
    }

    public class GuidProvider
    {
        public Guid GetGuid()
        {
            var guidString = PlayerPrefs.GetString("Guid",null);
            Debug.Log("Guid Loaded: " + guidString);
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
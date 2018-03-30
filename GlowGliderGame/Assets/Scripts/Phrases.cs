using UnityEngine;

namespace Assets.Scripts
{
    public static class Phrases
    {

        public static readonly string hello;

        private static string[] _phrases = new[]
        {
            "Place your thumbs here",
            //"Please try again",
            //"Oh I'm sorry! is it hard?",
            //"Try harder! You can do it!",
            //"Yeah, it lagged, totally!",
            //"Close enogh! Next Time",
            //"Impossible, game = bugged!",
            //"No! this is impossible!",
            //"Nice one ... not really xD",
            "Oh WOW!  Highscore!"
        };


        public static string GetTutorialPhrase()
        {
            return _phrases[0];
        }

        public static string GetRandomPhrase()
        {
            var randomInt = Random.Range(1, _phrases.Length - 1);
            return _phrases[randomInt];
        }

        public static string GetHighScorePhrase()
        {
            return _phrases[_phrases.Length - 1];
        }
    }
}
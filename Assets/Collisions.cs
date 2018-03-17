using UnityEngine;

namespace Assets
{
    public class Collisions
    {
        public int NormalCollections = 0;
        public int Score = 0;
        public Circle[] Circles;
        public Glider Glider;
        private int _collisionCounter;
        public bool SwitchColor;

        public Collisions()
        {
            _collisionCounter = 0;
            SwitchColor = false;
        }

        public void CheckCollision()
        {
            var gliderPos = Glider.transform.position;

            foreach (var circle in Circles)
            {
                var circlePos = circle.transform.position;
                var distanceSquared = (circlePos.x - gliderPos.x)* (circlePos.x - gliderPos.x) + (circlePos.y - gliderPos.y) * (circlePos.y - gliderPos.y);

                circle.SetFill();

                if (distanceSquared < Glider.CollisionDistance)
                {
                    circle.Alive = false;
                    NormalCollections += 1;

                    Score += circle.Value;

                    if (circle.Id != Glider.Id)
                    {
                        Glider.IsAlive = false;
                        _collisionCounter = 0;
                        NormalCollections = 0;
                        Score = 0;


                        foreach (var aliveCircle in Circles)
                        {
                            aliveCircle.Alive = false;              
                        }
                    }
                    else
                    {
                        _collisionCounter += 1;

                        if (_collisionCounter > 2)
                        {
                            SwitchColor = true;
                            _collisionCounter = 0;
                        }
                    }           
                }
            }
        }
    }
}
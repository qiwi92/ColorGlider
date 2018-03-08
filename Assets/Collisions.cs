using System.Collections.Generic;
using UnityEngine;

namespace Assets
{
    public class Collisions
    {
        public int NumberOfCollisions = 0;
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
            var gliderPos = Glider.GameObject.transform.position;

            foreach (var circle in Circles)
            {
                var circlePos = circle.GameObject.transform.position;
                var distanceSquared = (circlePos.x - gliderPos.x)* (circlePos.x - gliderPos.x) + (circlePos.y - gliderPos.y) * (circlePos.y - gliderPos.y);

                if (distanceSquared < Glider.CollisionDistance)
                {
                    circle.Alive = false;
                    NumberOfCollisions += 1;

                    if (circle.Id != Glider.Id)
                    {
                        Glider.IsAlive = false;
                        _collisionCounter = 0;
                        NumberOfCollisions = 0;
                        
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
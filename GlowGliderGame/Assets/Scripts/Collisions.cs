﻿using System.Collections.Generic;

namespace Assets.Scripts
{
    public class Collisions
    {       
        private Glider _glider;

        private readonly List<ICollider> _collider;

        public Collisions(Glider glider,CircleView[] circles, DiamondView[] diamonds, PowerupView[] powerups)
        {
            _glider = glider;
            _collider = new List<ICollider>();

            _collider.AddRange(circles);
            _collider.AddRange(diamonds);
            _collider.AddRange(powerups);
        }

        public void CheckCollisions()
        {
            var gliderPos = _glider.transform.position;

            foreach (var collider in _collider)
            {
                var colliderPos = collider.GetPosition();

                var distanceSquared = (colliderPos.x - gliderPos.x) * (colliderPos.x - gliderPos.x) + (colliderPos.y - gliderPos.y) * (colliderPos.y - gliderPos.y);

                if (distanceSquared < _glider.CollisionDistance)
                {
                    _glider.HandleCollision(collider);  
                }
            }
        }
    }
}
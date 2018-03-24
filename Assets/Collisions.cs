using System.Collections.Generic;

namespace Assets
{
    public class Collisions
    {       
        public Glider Glider;

        private readonly List<ICollider> _collider;

        public Collisions(Circle[] circles, Diamond[] diamonds, PowerUp[] powerUps)
        {
            _collider = new List<ICollider>();

            _collider.AddRange(circles);
            _collider.AddRange(diamonds);
            _collider.AddRange(powerUps);
        }

        public void CheckCollisions()
        {
            var gliderPos = Glider.transform.position;

            foreach (var collider in _collider)
            {
                var colliderPos = collider.GetPosition();

                var distanceSquared = (colliderPos.x - gliderPos.x) * (colliderPos.x - gliderPos.x) + (colliderPos.y - gliderPos.y) * (colliderPos.y - gliderPos.y);

                if (distanceSquared < Glider.CollisionDistance)
                {
                    Glider.HandleCollision(collider);
                    Glider.CollisionState = CollisionStates.JustCollided;
                }
            }
        }
    }
}
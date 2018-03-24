using UnityEngine;

namespace Assets
{
    public class Diamonds : MonoBehaviour
    {
        public Diamond DiamondPrefab;

        public int Amount;
        public float Speed;

        private Diamond[] _diamonds;

        private float _width;
        public float Height;

        public void SetUp(float width)
        {
            _width = width;
            _diamonds = new Diamond[Amount];

            for (var index = 0; index < _diamonds.Length; index++)
            {
                var newDiamond = Instantiate(DiamondPrefab);
                _diamonds[index] = newDiamond;
            }
        }

        public void Move()
        {
            foreach (var diamond in _diamonds)
            {
                if (diamond.transform.position.y < -Height)
                {
                    diamond.transform.position = new Vector3(Random.Range(- _width, _width), Height, 0);
                }

                diamond.transform.position += Vector3.down * Speed * Time.deltaTime;
            }
        }


    }
}

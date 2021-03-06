﻿using UnityEngine;

namespace Assets.Scripts
{
    public class DiamondsView : MonoBehaviour
    {
        public DiamondView DiamondPrefab;

        public int Amount;



        [Range(0, 1)] public float SpawnProbability;

        public ParticleSystem ParticleSystem;

        private ParticleSystem.EmitParams _emitParams;

        [HideInInspector] public DiamondView[] Diamonds;
        [HideInInspector] public ColorPalette ColorPalette;

        private float _width;
        public float Height;
        private readonly SpeedData _speedData = new SpeedData();


        public void SetUp(float width)
        {
            _width = width;
            Diamonds = new DiamondView[Amount];

            for (var index = 0; index < Diamonds.Length; index++)
            {
                var randomPos = new Vector3(Random.Range(-_width, _width), Height, 0);
                var newDiamond = Instantiate(DiamondPrefab, randomPos, Quaternion.identity);

                newDiamond.IsAlive = true;
                SetSpawnState(newDiamond);

                Diamonds[index] = newDiamond;
                
            }


            var mainModule = ParticleSystem.main;
            mainModule.duration = 4;
            mainModule.startColor = ColorPalette.Diamond;
            mainModule.startDelay = 2f;

            _emitParams = new ParticleSystem.EmitParams();
            _emitParams.applyShapeToPosition = true;

        }

        public void Move()
        {
            var smoothDeltaTime = Time.smoothDeltaTime;

            foreach (var diamond in Diamonds)
            {
                if (diamond.transform.position.y < -Height)
                {
                    Reset(diamond);
                }

                if (!diamond.IsAlive)
                {
                    _emitParams.position = diamond.transform.position;
                    ParticleSystem.Emit(_emitParams, 4);

                    Reset(diamond);
                    diamond.IsAlive = true;
                }

                if(diamond.CanSapwn)
                {
                    diamond.transform.position += Vector3.down * diamond.Speed * smoothDeltaTime;
                }
            }
        }

        public void SetSpeed(int score, bool isBoosted)
        {
            foreach (var diamond in Diamonds)
            {
                diamond.Speed = _speedData.GetSpeed(score, isBoosted); ;
            }
        }

        private void Reset(DiamondView diamond)
        {
            diamond.IsAlive = false;
            diamond.transform.position = new Vector3(Random.Range(-_width, _width), Height, 0);
        }

        public void KillAll()
        {
            foreach (var diamond in Diamonds)
            {
                Reset(diamond);           
            }
        }

      


        private void SetSpawnState(DiamondView diamond)
        {
            var roll = Random.Range(0, 1f);

            if (roll < SpawnProbability)
            {
                diamond.CanSapwn = true;
            }

            else
            {
                diamond.CanSapwn = false;
            }
        }
    }
}

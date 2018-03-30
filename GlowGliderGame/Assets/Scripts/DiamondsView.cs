using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class DiamondsView : MonoBehaviour
    {
        public DiamondView DiamondPrefab;

        public int Amount;
        private float _speed;

        public Image DiamondsImage;
        public Text DiamondText;

        [Range(0, 1)] public float SpawnProbability;

        public ParticleSystem ParticleSystem;

        private ParticleSystem.EmitParams _emitParams;

        [HideInInspector] public DiamondView[] Diamonds;
        [HideInInspector] public ColorPalette ColorPalette;

        private float _width;
        public float Height;


        public void SetUp(float width)
        {
            _width = width;
            Diamonds = new DiamondView[Amount];
            SetSpeed(0);

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
                    diamond.transform.position += Vector3.down * _speed * Time.deltaTime;
                }



                       
            }
        }

        public float SetSpeed(int score)
        {
            var baseSpeed = 3 + 0.1f * score;
            return _speed = Random.Range(baseSpeed, baseSpeed * 1.3f);
        }

        private void Reset(DiamondView diamond)
        {
            diamond.transform.position = new Vector3(Random.Range(-_width, _width), Height, 0);
        }

        public void KillAll()
        {
            foreach (var diamond in Diamonds)
            {
                diamond.IsAlive = false;
            }
        }

        public void SetNewValue(int diamondAmount)
        {
            DiamondText.text = diamondAmount.ToString("0");
            DiamondText.DOFade(1, 0.2f);
            DiamondsImage.DOFade(1, 0.2f).OnComplete(() =>
            {
                DiamondText.DOFade(0, 2f);
                DiamondsImage.DOFade(0, 2f);
            });
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

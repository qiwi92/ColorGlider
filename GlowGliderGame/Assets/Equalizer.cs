using UnityEngine;

namespace Assets
{
    public class Equalizer : MonoBehaviour
    {

        public AudioSource AudioSource;
        public GameObject Object;

        [Range(1, 256)] public int HighCut;
        [Range(1, 256)] public int LowCut;

        [Range(0, 3)] public float BounceCut;

        [Range(1, 10)] public float Multiplier;
        [Range(0, 1)] public float CameraShake;

        private readonly float[] _spectrum = new float[256];

        void Start()
        {
            //AudioSource.GetSpectrumData(200,)
        }


        void Update()
        {


            AudioListener.GetSpectrumData(_spectrum, 0, FFTWindow.Rectangular);

            float bounce = 0;

            if (LowCut > HighCut)
            {
                LowCut = HighCut;
            }

            for (int i = LowCut; i < HighCut; i++)
            {
                bounce += _spectrum[i];
                
            }


            if (bounce > BounceCut)
            {
                Object.transform.position += new Vector3(0, bounce*Multiplier, 0);
                Debug.Log("Hello");
            }

            if (Object.transform.position.y > 0)
            {
                Object.transform.position += Vector3.down * Time.deltaTime;
            }
        }
    }
}

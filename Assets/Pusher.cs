using UnityEngine;

namespace Assets
{
    public class Pusher : MonoBehaviour
    {

        public float Speed;
        public float ScreenHight;
        public float XPos;

        public ParticleSystem Paricles;
        public Transform ParticleTransform;

        public Glider Glider;

        [HideInInspector] public bool IsPushing;
        [HideInInspector] public ColorPalette ColorPalette;

        private bool _isActive;
        public int Direction { get; private set; }
        public int Id { get; private set; }

        public State CurrentState;

        void Start()
        {
            ResetPosition();
            _isActive = false;
        }

        //void Update ()
        //{
        //    if (_isActive)
        //    {
        //        if (transform.position.y < -ScreenHight)
        //        {
        //            Activate(false);
        //        }

        //        this.transform.position += Vector3.down * Speed * Time.deltaTime;

        //        switch (CurrentState)
        //        {
        //            case State.Normal:
        //                HandleNormal();
        //                break;
        //            case State.TransitionToDraining:
        //                HandleTransitionToDraining();
        //                break;
        //            case State.Draining:
        //                HandleDraining();
        //                break;
        //            case State.TransitionToNormal:
        //                TransitionToNormal();
        //                break;
        //        }
        //    }
        //}

        //private void HandleNormal()
        //{
        //    if (!Glider.HasHitBox)
        //    {
        //        CurrentState = State.TransitionToDraining;
        //    }
        //}

        //private void HandleTransitionToDraining()
        //{
        //    SetColor(ColorPalette.Untargetable);
        //    CurrentState = State.Draining;
        //}

        //private void HandleDraining()
        //{
        //    if (Glider.HasHitBox)
        //    {
        //        CurrentState = State.TransitionToNormal;
        //    }
        //}

        //private void TransitionToNormal()
        //{
        //    SetColor(ColorPalette.Colors[Id]);
        //    CurrentState = State.Normal;
        //}

        public void Activate(bool isActive)
        {
            if (isActive)
            {
                _isActive = true;
                SetRandomDirection();
                SetRandomdId();
                SetColor(ColorPalette.Colors[Id]);
                Paricles.Play();
            }

            if (!isActive)
            {            
                _isActive = false;
                ResetPosition();
                Paricles.Stop();
            }
        }

        private void ResetPosition()
        {
            transform.position = Vector3.up * ScreenHight;
        }

        private void SetRandomDirection()
        { 
            if (Random.value < 0.5f)
            {
                Direction = -1;
            }
            else
            {
                Direction = 1;
            }

            ParticleTransform.localPosition = XPos * Vector3.right * Direction;
            ParticleTransform.localRotation = Quaternion.Euler(0,0, Direction * 90);
        }

        private void SetRandomdId()
        {
            Id = Random.Range(0, 3);
        }

        private void SetColor(Color color)
        {
            Paricles.startColor = color;
        }

        public enum State
        {
            Normal,
            TransitionToDraining,
            Draining,
            TransitionToNormal,
        }
    }
}

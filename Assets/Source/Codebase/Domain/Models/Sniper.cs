using System;
using UnityEngine;

namespace Source.Root
{
    public class Sniper : Character, IDamageable
    {
        private readonly Camera _camera;

        public Sniper(float health) : base(health) 
        {
            _camera = Camera.main;
        }

        public bool InAim { get; private set; }

        public void EnterToAim()
            => InAim = true;

        public void ExitOfAim()
            => InAim = false;

        public override Ray Ray()
            => new(_camera.transform.position, _camera.transform.forward);
    }
}

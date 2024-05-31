using UnityEngine;

namespace Source.Root
{
    public class Criminal : Character
    {
        private Transform _target;

        public Criminal(float health) : base(health) { }

        public void SetTarget(Transform target)
            => _target = target;

        public override Vector3 CalculateTrajectory(IGun gun, IBullet bullet)
        {
            Vector3 direction = _target.position - gun.EndPoint.position;
            Ray ray = new(gun.EndPoint.position, direction);

            if (Physics.Raycast(ray, out RaycastHit hitInfo, gun.Range))
            {
                bullet.Attack(hitInfo);
                return gun.EndPoint.position + (hitInfo.point - gun.EndPoint.position);
            }
            else
            {
                return gun.EndPoint.position + gun.EndPoint.forward * gun.Range;
            }
        }
    }
}

using Source.Root;
using UnityEngine;

public interface IGun
{
    public Transform EndPoint {  get; }
    public void ApplyTrajectori(Ray ray);
    public void TakeBullet(IBullet bullet);
}

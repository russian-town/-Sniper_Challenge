using Source.Root;
using UnityEngine;

public class BulletViewFactory
{
    private readonly BulletView _template;
    private readonly ICoroutineRunner _coroutineRunner;

    public BulletViewFactory(BulletView template, ICoroutineRunner coroutineRunner)
    {
        _template = template;
        _coroutineRunner = coroutineRunner;
    }

    public void Create(Transform point, Vector3 direction)
    {
        Bullet bullet = new(point.position, direction);
        BulletView view = Object.Instantiate(_template, point.position, point.rotation);
        BulletPresenter bulletPresenter = new(bullet, view, _coroutineRunner);
        view.Construct(bulletPresenter);
    }
}

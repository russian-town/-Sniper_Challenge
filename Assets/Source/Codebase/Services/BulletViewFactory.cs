using Source.Root;
using UnityEngine;

public class BulletViewFactory
{
    private readonly BulletView _template;
    private readonly ICoroutineRunner _coroutineRunner;
    private readonly BulletConfig _config;

    public BulletViewFactory(BulletView template, ICoroutineRunner coroutineRunner, BulletConfig config)
    {
        _template = template;
        _coroutineRunner = coroutineRunner;
        _config = config;
    }

    public void Create(Transform point, RaycastHit[] raycastHits)
    {
        Bullet bullet = new(point.position, _config);
        BulletView view = Object.Instantiate(_template, point.position, point.rotation);
        BulletPresenter bulletPresenter = new(bullet, view, _coroutineRunner, raycastHits);
        view.Construct(bulletPresenter);
    }
}

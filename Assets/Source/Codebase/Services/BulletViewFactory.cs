using Source.Root;
using UnityEngine;

public class BulletViewFactory
{
    private BulletConfig _config;
    private BulletView _template;

    public BulletViewFactory(BulletConfig config, BulletView template)
    {
        _config = config;
        _template = template;
    }

    public BulletView Create(Transform point)
    {
        Bullet bullet = new(_config);
        BulletView view = Object.Instantiate(_template, point.position, point.rotation);
        BulletPresenter bulletPresenter = new(bullet, view);
        view.Construct(bulletPresenter);
        return view;
    }
}

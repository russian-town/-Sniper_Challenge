using Source.Root;
using UnityEngine;

public class BulletViewFactory
{
    private BulletView _template;

    public BulletViewFactory(BulletView template)
    {
        _template = template;
    }

    public BulletView Create(Transform point)
    {
        Bullet bullet = new();
        BulletView view = Object.Instantiate(_template, point.position, point.rotation);
        BulletPresenter bulletPresenter = new(bullet, view);
        view.Construct(bulletPresenter);
        return view;
    }
}

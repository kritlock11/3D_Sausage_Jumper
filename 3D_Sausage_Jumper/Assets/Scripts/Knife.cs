using DG.Tweening;
using UnityEngine;

public class Knife : Trap
{
    [SerializeField] private Collider _coll;

    private float _offset = 8f;
    private float _time1 = 1f;
    private float _time2 = 0.2f;
    private int _loops = 100;

    private float _mDown;
    private float _mUp;

    private void Start()
    {
        var posY = transform.position.y;
        _mDown = posY - _offset;
        _mUp = _mDown + _offset;

        var seq = DOTween.Sequence();

        seq.Append(transform.DOMoveY(_mDown, _time2)).AppendCallback(() => _coll.enabled = false)
            .Append(transform.DOMoveY(_mUp, _time1)).AppendCallback(() => _coll.enabled = true)
            .SetLoops(_loops);

    }

    private void OnDrawGizmos()
    {
        var direction = new Vector3(transform.position.x, transform.position.y - _offset / 2, transform.position.z);
        Gizmos.DrawWireCube(direction, new Vector3(2, _offset, 2));
    }
}
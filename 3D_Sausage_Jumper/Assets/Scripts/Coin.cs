using System.Collections;
using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] private GameObject _go;
    [SerializeField] private ParticleSystem _particle;
    [SerializeField] private Renderer _renderer;
    [SerializeField] private Collider _collider;
    [SerializeField] private AnimationCurve _lerpCurve;

    private float _offsetY = 50f;
    private float _duration = 1f;

    private void OnTriggerEnter()
    {
        StartCoroutine(LightUp(_offsetY, _duration));
    }

    private IEnumerator LightUp(float offset, float duration)
    {
        var basePos = _go.transform.position.y;
        var yOffset = basePos + offset;
        var t = duration;

        while (t > 0f)
        {
            t -= Time.unscaledDeltaTime;
            var progress = 1 - t / duration;
            SetSize(Mathf.Lerp(basePos, yOffset, _lerpCurve.Evaluate(progress)));
            yield return null;
        }

        Off();
    }

    private void Off()
    {
        _collider.enabled = false;
        _renderer.enabled = false;
    }

    private void SetSize(float offset)
    {
        var pos = _go.transform.position;
        pos.y = offset;

        _go.transform.position = pos;
    }
}

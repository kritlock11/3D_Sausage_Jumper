using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class UIFinish : MonoBehaviour
{
    [SerializeField] private RectTransform _finTransform;
    [SerializeField] private RectTransform _adTransform;
    [SerializeField] private Text _noThx;

    public void ActiveFinUi()
    {
        _finTransform.gameObject.SetActive(true);

        var seq = DOTween.Sequence();
        seq.PrependInterval(.5f)
            .Append(_finTransform.DOScaleY(1f, 0.20f))
            .AppendInterval(.4f)
            .Append(_adTransform.DOScaleY(1f, .15f).SetEase(Ease.Linear))
            .AppendInterval(2f)
            .AppendCallback(ActiveNoThx);
    }

    private void ActiveNoThx()
    {
        _noThx.DOFade(0.8f, 2f);
    }
}

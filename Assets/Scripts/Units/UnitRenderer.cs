using UnityEngine;
using TMPro;
using DG.Tweening;

public class UnitRenderer : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _spriteRenderer;
    public SpriteRenderer SpriteRenderer => _spriteRenderer;

    [SerializeField] private TextMeshProUGUI _damageText;
    private Vector3 _damageTextSpawnPosition;

    [SerializeField] private ParticleSystem _hitParticle;

    [SerializeField] private HPBarUI _hpBarUI;
    public HPBarUI HPBarUI => _hpBarUI;

    private void Start()
    {
        _damageTextSpawnPosition = _damageText.transform.localPosition;   
    }

    public void HitParticleRender()
    {
        if(_hitParticle.isPlaying)
        {
            _hitParticle.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
        }
        _hitParticle.Play();
    }

    public void DamageTextAnim(int hitDamage)
    {
        _damageText.text = hitDamage.ToString("N0");

        Sequence seq = DOTween.Sequence();

        _damageText.color = new Color(_damageText.color.r, _damageText.color.g, _damageText.color.b, 1);

        seq.Append(_damageText.transform.DOLocalMoveY(-0.25f, 0.5f).SetEase(Ease.OutBack))
           .Join(_damageText.DOFade(0.0f, 0.45f))
           .OnComplete(() =>
           {
               _damageText.transform.localPosition = _damageTextSpawnPosition;
           });

        seq.Play();
    }
}

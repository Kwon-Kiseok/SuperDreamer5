using UnityEngine;
using TMPro;
using DG.Tweening;

public class UserCharacterRenderer : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _spriteRenderer;
    public SpriteRenderer SpriteRenderer => _spriteRenderer;

    [SerializeField] private HPBarUI _hpBarUI;
    public HPBarUI HPBarUI => _hpBarUI;
}

using UnityEngine;
using UnityEngine.UI;

public class HPBarUI : MonoBehaviour
{
    [SerializeField] private Image _fillImage;

    public void UpdateHealth(float currentHp, float maxHp)
    {
        _fillImage.fillAmount = currentHp / maxHp;
    }
}

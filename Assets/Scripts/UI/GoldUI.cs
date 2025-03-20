using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Cysharp.Threading.Tasks;
using UniRx;

public class GoldUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _goldText;

    void Start()
    {
        if(CurrenyManager.Instance != null)
        {
            CurrenyManager.Instance.CurrenntGold.Subscribe((int goldAmount) =>
            {
                UpdateGoldUI(goldAmount);
            }).AddTo(this);

            _goldText.text = CurrenyManager.Instance.CurrenntGold.Value.ToString();
        }
    }

    void UpdateGoldUI(int goldAmount)
    {
        _goldText.text = goldAmount.ToString();
    }
}

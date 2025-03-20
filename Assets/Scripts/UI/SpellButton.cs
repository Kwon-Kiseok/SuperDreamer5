using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SpellButton : MonoBehaviour
{
    [SerializeField] private string _spellName;
    public string SpellName => _spellName;
    [SerializeField] private TextMeshProUGUI _spellCountText;
    [SerializeField] private Button _spellButton;
    public Button Button => _spellButton;  

    public void SetSpellButtonActive(bool active)
    {
        _spellButton.interactable = active;
    }

    public void SetSpellCount(int spellCount)
    {
        _spellCountText.text = spellCount.ToString();
    }
}

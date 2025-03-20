using UnityEngine;
using UnityEngine.UI;
using UniRx;
using TMPro;
using Zenject;
using DG.Tweening;

public class ActionPanelUI : MonoBehaviour
{
    public enum SubPanelState
    {
        None,
        Hunting,
        Godsu,
        Enhanced,
        Mining
    }

    [SerializeField] private RectTransform _actionUITransform;

    [SerializeField] private Button HuntingBtn;
    [SerializeField] private Button GodsuBtn;
    [SerializeField] private Button SpellCreateBtn;
    [SerializeField] private Button EnhancedBtn;
    [SerializeField] private Button MiningBtn;

    [SerializeField] private TextMeshProUGUI _spellSpawnCostText;

    private Vector3 _originRectTransformPosition;
    [SerializeField] private float _panelMoveDistance = 160f;
    [SerializeField] private float _moveDuration = 0.5f;

    private bool _isUpPanel = false;

    private SubPanelState _currentSubPanelState = SubPanelState.None;

    private SpellSpawner _spellSpawner; 

    [Inject]
    public void Inject(SpellSpawner spellSpawner)
    {
        _spellSpawner = spellSpawner;
    }

    void Start()
    {
        _originRectTransformPosition = _actionUITransform.anchoredPosition;

        if (CurrenyManager.Instance != null)
        {
            CurrenyManager.Instance.CurrenntGold.Subscribe((int goldAmount) =>
            {
                if(_spellSpawner.SpellSpawnCostGold > goldAmount)
                {
                    _spellSpawnCostText.color = Color.red;
                }
                else
                {
                    _spellSpawnCostText.color = Color.gray;
                }
            }).AddTo(this);
        }

        SpellCreateBtn.onClick.AddListener(() => {
            _spellSpawner.SpawnSpell();

            _spellSpawnCostText.text = _spellSpawner.SpellSpawnCostGold.ToString();
        });

        HuntingBtn.onClick.AddListener(() => {
            if(_currentSubPanelState != SubPanelState.Hunting)
            {
                if(_isUpPanel)
                {
                    // active hunting panel
                }
                else
                {
                    UpMoveActivePanel();
                }
                _currentSubPanelState = SubPanelState.Hunting;
            }
            else
            {
                DownMoveActivePanel();
            }
        });

        GodsuBtn.onClick.AddListener(() => {
            if (_currentSubPanelState != SubPanelState.Godsu)
            {
                if (_isUpPanel)
                {
                    // active Godsu panel
                }
                else
                {
                    UpMoveActivePanel();
                }
                _currentSubPanelState = SubPanelState.Godsu;
            }
            else
            {
                DownMoveActivePanel();
            }
        });

        EnhancedBtn.onClick.AddListener(() =>
        {
            if (_currentSubPanelState != SubPanelState.Enhanced)
            {
                if (_isUpPanel)
                {
                    // active Enhanced panel
                }
                else
                {
                    UpMoveActivePanel();
                }
                _currentSubPanelState = SubPanelState.Enhanced;
            }
            else
            {
                DownMoveActivePanel();
            }
        });

        MiningBtn.onClick.AddListener(() =>
        {
            if (_currentSubPanelState != SubPanelState.Mining)
            {
                if (_isUpPanel)
                {
                    // active Mining panel
                }
                else
                {
                    UpMoveActivePanel();
                }
                _currentSubPanelState = SubPanelState.Mining;
            }
            else
            {
                DownMoveActivePanel();
            }
        });
    }

    private void UpMoveActivePanel()
    {
        _actionUITransform.DOAnchorPosY(_actionUITransform.anchoredPosition.y + _panelMoveDistance, _moveDuration).SetEase(Ease.OutBack);
        _isUpPanel = true;
    }

    private void DownMoveActivePanel()
    {
        _actionUITransform.DOAnchorPosY(_originRectTransformPosition.y, _moveDuration).SetEase(Ease.OutBack);
        _isUpPanel = false;
        _currentSubPanelState = SubPanelState.None;
    }
}

using UnityEngine;

public class Lane : MonoBehaviour
{
    private bool _isPlayerControlled = false;
    public bool IsPlayerControlled => _isPlayerControlled;

    [SerializeField] private LaneRenderer _laneRenderer;

    public void SetAsPlayerLane()
    {
        _isPlayerControlled = true;
        _laneRenderer.SetPlayerLaneSprite();
    }

    public void SetAsBotLane()
    {
        _isPlayerControlled = false;
        _laneRenderer.SetBotLaneSprite();
    }
}

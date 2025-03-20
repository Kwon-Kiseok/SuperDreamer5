using UnityEngine;

public class LaneRenderer : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _laneSpriteRenderer;
    [SerializeField] private SpriteRenderer _playerPositionSpriteRenderer;

    public void SetPlayerLaneSprite()
    {
        _laneSpriteRenderer.color = Color.white;
        _playerPositionSpriteRenderer.color = Color.green;
    }

    public void SetBotLaneSprite()
    {
        _laneSpriteRenderer.color = Color.grey;
        _playerPositionSpriteRenderer.color = Color.yellow;
    }
}

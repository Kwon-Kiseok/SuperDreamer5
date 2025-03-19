using UnityEngine;

public class LaneSpawner : MonoBehaviour
{
    [SerializeField] private GameObject _lanePrefab;
    [SerializeField] private int _playerCount = 4;
    [SerializeField] private int _columns = 4;
    [SerializeField] private float _laneSpacing = 1.25f;

    void Start()
    {
        SpawnLanes();
    }

    void SpawnLanes()
    {
        // 맵 양쪽의 공백까지 고려해서 전체 공백의 넓이를 구함
        float totalSpacingWidth = (_playerCount - 1) * _laneSpacing;
        float startX = -totalSpacingWidth * 0.5f;   

        for (int x = 0; x < _columns; x++)
        {
            Vector3 position = new Vector3(startX + x * _laneSpacing, 0, 0);
            GameObject laneObject = GameObject.Instantiate(_lanePrefab, position, Quaternion.identity);
            laneObject.transform.parent = this.transform;
        }
    }
}

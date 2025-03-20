using UnityEngine;
using Zenject;
using System.Collections.Generic;

public class LaneSpawner : MonoBehaviour
{
    [SerializeField] private GameObject _lanePrefab;
    [SerializeField] private int _playerCount = 4;
    [SerializeField] private int _columns = 4;
    [SerializeField] private float _laneSpacing = 1.25f;

    private List<Lane> _lanelist = new List<Lane>();
    private Lane _playerLane;
    public Lane PlayerLane => _playerLane;

    [Inject] private DiContainer _container;

    void Start()
    {
        SpawnLanes();
        SetPlayerLane();
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
            _container.InjectGameObject(laneObject);

            Lane lane = laneObject.GetComponent<Lane>();
            _lanelist.Add(lane);
        }
    }

    void SetPlayerLane()
    {
        int randomIndex = Random.Range(0, _lanelist.Count);
        _playerLane = _lanelist[randomIndex];

        foreach(Lane lane in _lanelist)
        {
            if(lane == _playerLane)
            {
                lane.SetAsPlayerLane();
            }
            else
            {
                lane.SetAsBotLane();
            }
        }
    }
}

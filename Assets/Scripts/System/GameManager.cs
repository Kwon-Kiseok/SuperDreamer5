using UnityEngine;
using Zenject;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    private LaneSpawner _laneSpawner;

    [Inject]
    public void Inject(LaneSpawner laneSpawner)
    {
        _laneSpawner = laneSpawner;
    }
}

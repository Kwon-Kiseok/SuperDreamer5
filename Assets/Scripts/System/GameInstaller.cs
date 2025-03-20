using UnityEngine;
using Zenject;

public class GameInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<GameManager>().FromComponentInHierarchy().AsSingle();
        Container.Bind<LaneSpawner>().FromComponentInHierarchy().AsSingle();

        Container.Bind<EnemyDatabase>().AsSingle().NonLazy();
    }
}
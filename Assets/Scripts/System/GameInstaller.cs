using UnityEngine;
using Zenject;

public class GameInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<EnemyDatabase>().AsSingle().NonLazy();
        Container.Bind<SpellDatabase>().AsSingle().NonLazy();

        Container.Bind<GameManager>().FromComponentInHierarchy().AsSingle();
        Container.Bind<LaneSpawner>().FromComponentInHierarchy().AsSingle();
        Container.Bind<SpellSpawner>().FromComponentInHierarchy().AsSingle();

        SpellFactory.RegisterSpell("Fireball", (prefab, player) => new FireballSpell(prefab));
        SpellFactory.RegisterSpell("SandPistol", (prefab, player) => new SandPistolSpell(prefab));
    }
}
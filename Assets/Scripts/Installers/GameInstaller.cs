
using Adver;
using Level;
using UnityEngine.Rendering;
using Zenject;

public class GameInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<AudioManager>().FromComponentInHierarchy().AsSingle().NonLazy();
        Container.Bind<UIManager>().FromComponentInHierarchy().AsSingle().NonLazy();
        Container.Bind<PlayerForce>().FromComponentInHierarchy().AsSingle().NonLazy();
        Container.Bind<PlayerEvents>().FromComponentInHierarchy().AsSingle().NonLazy();
        Container.Bind<Player>().FromComponentInHierarchy().AsSingle().NonLazy();
        Container.Bind<ObstacleSpawner>().FromComponentInHierarchy().AsSingle().NonLazy();
        Container.Bind<LevelSwitcher>().FromComponentInHierarchy().AsSingle().NonLazy();
        Container.Bind<ILevelSaver>().To<FakeLevelSaver>().FromNew().AsSingle();
        Container.BindInterfacesAndSelfTo<AndroidYandexAd>().AsSingle();
    }
}

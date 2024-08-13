using Adver;
using Analytics;
using Level;
using Localization;
using UnityEngine;
using Zenject;

public class GameInstaller : MonoInstaller
{

    [SerializeField] private int _dieStep;
    
    public override void InstallBindings()
    {
        Container.Bind<AudioManager>().FromComponentInHierarchy().AsSingle().NonLazy();
        Container.Bind<UIManager>().FromComponentInHierarchy().AsSingle().NonLazy();
        Container.Bind<PlayerForce>().FromComponentInHierarchy().AsSingle().NonLazy();
        Container.Bind<PlayerEvents>().FromComponentInHierarchy().AsSingle().NonLazy();
        Container.Bind<Player>().FromComponentInHierarchy().AsSingle().NonLazy();
        Container.Bind<ObstacleSpawner>().FromComponentInHierarchy().AsSingle().NonLazy();
        Container.Bind<LevelSwitcher>().FromComponentInHierarchy().AsSingle().NonLazy();
        Container.Bind<ILevelSaver>().To<YGLevelSaver>().FromNew().AsSingle();
        Container.BindInterfacesAndSelfTo<YGAd>().AsSingle();
        Container.Bind<IAnalytics>().To<GamePushAnalytics>().FromNew().AsSingle().NonLazy();
        Container.Bind<ILocalization>().To<YGLocalization>().AsSingle().NonLazy();
        Container.BindInterfacesAndSelfTo<FullScreenAdShower>().AsSingle().WithArguments(_dieStep);
    }
}
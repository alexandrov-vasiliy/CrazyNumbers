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

        Container.Bind<IAnalytics>().To<YandexMetrikaAnalytics>().FromNew().AsSingle().NonLazy();
        Container.BindInterfacesAndSelfTo<FullScreenAdShower>().AsSingle().WithArguments(_dieStep);

        BindSaver();
        BindAd();
        BindLocalization();
    }

    private void BindSaver()
    {
        #if UNITY_WEBGL
                Container.Bind<ILevelSaver>().To<YGLevelSaver>().FromNew().AsSingle();
        #endif
        #if UNITY_ANDROID
                                Container.Bind<ILevelSaver>().To<PlayerPrefsSaver>().FromNew().AsSingle();
        #endif
    }

    private void BindAd()
    {
        #if UNITY_WEBGL
                Container.BindInterfacesAndSelfTo<YGAd>().AsSingle();
        #endif
        #if UNITY_ANDROID
                Container.BindInterfacesAndSelfTo<AndroidYandexAd>().AsSingle();
        #endif
    }

    private void BindLocalization()
    {
        #if UNITY_WEBGL
                Container.Bind<ILocalization>().To<YGLocalization>().AsSingle().NonLazy();
        #endif
        #if UNITY_ANDROID
                Container.Bind<ILocalization>().To<AndroidLocalization>().AsSingle().NonLazy();
        #endif
    }
}
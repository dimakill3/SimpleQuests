using _Assets.Scripts.Game.Enemies.Factory;
using _Assets.Scripts.Game.Enemies.Spawner;
using _Assets.Scripts.Game.Map;
using _Assets.Scripts.Game.Quests.Services;
using _Assets.Scripts.Game.Randomizers;
using _Assets.Scripts.Game.Time;
using UnityEngine;
using Zenject;

namespace _Assets.Scripts.Game.Installers
{
    public class GameInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            BindSceneComponents();
            BindFactories();
            BindServices();
        }

        private void BindSceneComponents()
        {
            Container
                .Bind<Camera>()
                .FromInstance(Camera.main)
                .AsTransient();
            Container
                .Bind<IMapConfigurator>()
                .To<MapConfigurator>()
                .AsSingle();
        }

        private void BindFactories()
        {
            Container
                .Bind<IEnemiesPool>()
                .To<EnemiesPool>()
                .AsSingle();
            
            Container
                .Bind<IEnemyFactory>()
                .To<EnemyFactory>()
                .AsSingle();
        }

        private void BindServices()
        {
            Container
                .Bind<IEnemySpawner>()
                .To<EnemySpawner>()
                .AsSingle();
            Container
                .Bind<IPlayTimeService>()
                .To<PlayTimeService>()
                .AsSingle();
            Container
                .Bind<IEnemyRandomizer>()
                .To<EnemyRandomizer>()
                .AsSingle();
            Container
                .Bind<IQuestsGiver>()
                .To<QuestsGiver>()
                .AsSingle();
            Container
                .Bind<IQuestsProgression>()
                .To<QuestsProgression>()
                .AsSingle();
        }
    }
}
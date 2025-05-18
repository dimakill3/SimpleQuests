using System;
using _Assets.Scripts.Core.Infrastructure.EventManagement;
using _Assets.Scripts.Game.Enemies.Configs;
using _Assets.Scripts.Game.Enemies.Damage;
using _Assets.Scripts.Game.Enemies.Enums;
using _Assets.Scripts.Game.Enemies.Movement;
using _Assets.Scripts.Game.Events;
using UnityEngine;
using Zenject;
using IPoolable = _Assets.Scripts.Core.Infrastructure.ObjectPool.IPoolable;

namespace _Assets.Scripts.Game.Enemies
{
    public class Enemy : MonoBehaviour, IPoolable
    {
        public event Action<Enemy> OnDeath;

        [SerializeField] private EnemyDamageable damageable;
        [SerializeField] private ChaoticMovement chaoticMovement;
        private IEventProvider _eventProvider;

        public EnemyType EnemyType { get; private set; }

        [Inject]
        private void Construct(IEventProvider eventProvider)
        {
            _eventProvider = eventProvider;
        }

        public void Initialize(EnemyConfig enemyConfig)
        {
            EnemyType = enemyConfig.EnemyType;
            
            chaoticMovement.Initialize();

            damageable.OnDeath += HandleDeath;
        }

        public void OnSpawned() =>
            gameObject.SetActive(true);

        public void OnDespawned()
        {
            gameObject.SetActive(false);
            chaoticMovement.Deinitialize();
        }

        private void OnDestroy() =>
            damageable.OnDeath -= HandleDeath;

        private void HandleDeath(IDamageable damageableObj)
        {
            OnDeath?.Invoke(this);
            _eventProvider.Invoke(new EnemyDestroyedEvent(EnemyType));
        }
    }
}
using System;
using UnityEngine;

namespace _Assets.Scripts.Game.Enemies.Damage
{
    public class EnemyDamageable : MonoBehaviour, IDamageable
    {
        public event Action<IDamageable> OnDeath;

        private void OnMouseDown() =>
            OnDeath?.Invoke(this);
    }
}
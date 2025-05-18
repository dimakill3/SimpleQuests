using System;

namespace _Assets.Scripts.Game.Enemies.Damage
{
    public interface IDamageable
    {
        event Action<IDamageable> OnDeath;
    }
}
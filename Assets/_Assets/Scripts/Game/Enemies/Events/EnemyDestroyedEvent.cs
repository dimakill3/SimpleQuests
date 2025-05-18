using _Assets.Scripts.Core.Infrastructure.EventManagement;
using _Assets.Scripts.Game.Enemies.Enums;

namespace _Assets.Scripts.Game.Events
{
    public class EnemyDestroyedEvent : IEvent
    {
        public EnemyDestroyedEvent(EnemyType enemyType)
        {
            EnemyType = enemyType;
        }

        public EnemyType EnemyType { get; private set; }
    }
}
namespace _Assets.Scripts.Core.Infrastructure.ObjectPool
{
    public interface IPoolable
    {
        void OnSpawned();
        void OnDespawned();
    }
}
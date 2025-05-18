using _Assets.Scripts.Core.Infrastructure.Configs;
using _Assets.Scripts.Core.Infrastructure.Mono;
using _Assets.Scripts.Game.Configs;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace _Assets.Scripts.Game.Enemies.Movement
{
    public class ChaoticMovement : MonoBehaviour
    {
        [SerializeField] private float speed = 3f;
        [SerializeField] private float minTime = 1f;
        [SerializeField] private float maxTime = 3f;

        private Vector3 _boundsMin;
        private Vector3 _boundsMax;
        private Vector3 _target;
        private float _timer;
        private MonoService _monoService;
        private MapConfig _mapConfig;

        [Inject]
        public void Construct(MonoService monoService, GameConfig gameConfig)
        {
            _monoService = monoService;
            _mapConfig = gameConfig.MapConfig;
        }

        public void Initialize()
        {
            SetBounds();
            SetRandomTarget();
            _monoService.OnTick += Tick;
        }

        public void Deinitialize() =>
            _monoService.OnTick -= Tick;

        private void OnDestroy() =>
            _monoService.OnTick -= Tick;

        private void SetBounds()
        {
            _boundsMin = new Vector3(-_mapConfig.MapSize.x, 0, -_mapConfig.MapSize.z);
            _boundsMax = new Vector3(_mapConfig.MapSize.x, _mapConfig.MapSize.y, _mapConfig.MapSize.z);
        }

        private void SetRandomTarget()
        {
            var x = Random.Range(_boundsMin.x, _boundsMax.x);
            var y = Random.Range(_boundsMin.y, _boundsMax.y);
            var z = Random.Range(_boundsMin.z, _boundsMax.z);
            _target = new Vector3(x, y, z);
            _timer = Random.Range(minTime, maxTime);
        }

        private void Tick()
        {
            MoveTowardsTarget();
            ProcessTimer();
        }

        private void MoveTowardsTarget() =>
            transform.position = Vector3.MoveTowards(transform.position, _target, speed * UnityEngine.Time.deltaTime);

        private void ProcessTimer()
        {
            _timer -= UnityEngine.Time.deltaTime;
            if (_timer <= 0f || Vector3.Distance(transform.position, _target) < 0.1f)
                SetRandomTarget();
        }
    }
}
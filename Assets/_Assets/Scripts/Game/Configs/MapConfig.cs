using UnityEngine;

namespace _Assets.Scripts.Game.Configs
{
    [CreateAssetMenu(fileName = "MapConfig", menuName = "Configs/MapConfig")]
    public class MapConfig : ScriptableObject
    {
        public Vector3 MapSize;
    }
}
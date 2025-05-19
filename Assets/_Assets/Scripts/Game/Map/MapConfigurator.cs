using UnityEngine;

namespace _Assets.Scripts.Game.Map
{
    public interface IMapConfigurator
    {
        public void ConfigureMap(Vector3 mapSize);
    }
    
    public class MapConfigurator : IMapConfigurator
    {
        public void ConfigureMap(Vector3 mapSize)
        {
            var activeTerrain = Terrain.activeTerrain;
            
            activeTerrain.terrainData.size = new Vector3(mapSize.x, activeTerrain.terrainData.size.y, mapSize.z);
            activeTerrain.Flush();
        }
    }
}
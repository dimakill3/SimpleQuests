using UnityEngine;

namespace _Assets.Scripts.Game.Map
{
    public static class MapUtil
    {
        public static Vector3 GetRandomMapPosition()
        {
            var terrain = Terrain.activeTerrain;
            
            if (terrain == null)
                return Vector3.zero;
            
            var terrainData = terrain.terrainData;
            var terrainPos = terrain.transform.position;
            
            var x = Random.Range(0f, terrainData.size.x);
            var z = Random.Range(0f, terrainData.size.z);
            var y = terrain.SampleHeight(new Vector3(x, 0, z)) + terrainPos.y;

            return new Vector3(x + terrainPos.x, y, z + terrainPos.z);
        }
    }
}
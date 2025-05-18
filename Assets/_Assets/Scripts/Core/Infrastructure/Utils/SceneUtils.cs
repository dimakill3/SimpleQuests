using System.IO;
using System.Linq;
using UnityEditor;

namespace _Assets.Scripts.Core.Infrastructure.Utils
{
    public static class SceneUtils
    {
#if UNITY_EDITOR
        public static string[] GetBuildScenes()
        {
            return EditorBuildSettings.scenes
                .Where(scene => scene.enabled)
                .Select(scene => Path.GetFileNameWithoutExtension(scene.path))
                .ToArray();
        }
#endif
    }
}
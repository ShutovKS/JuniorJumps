#region

using UnityEngine;

#endregion

namespace Data.Setting
{
    [CreateAssetMenu(fileName = "GameplaySetting", menuName = "Setting/GameplaySetting", order = 0)]
    public class GameplaySetting : ScriptableObject
    {
        [field: SerializeField] public float MinPlatformSpawnPositionByX { get; private set; }
        [field: SerializeField] public float MaxPlatformSpawnPositionByX { get; private set; }
        [field: SerializeField] public float MinPlatformSpawnPositionByY { get; private set; }
        [field: SerializeField] public float MaxPlatformSpawnPositionByY { get; private set; }
        [field: SerializeField] public float StartPlatformSpawnPositionByY { get; private set; }
        [field: SerializeField] public Vector2 SpawnPositionPlayer { get; private set; }
    }
}
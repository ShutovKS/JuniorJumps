#region

using UnityEngine;

#endregion

namespace Data.Setting
{
    [CreateAssetMenu(fileName = "PlatformSpawnSetting", menuName = "Setting/PlatformSpawnSetting", order = 0)]
    public class GameplaySetting : ScriptableObject
    {
        [field: SerializeField] public float MinPlatformSpawnPositionByX { get; private set; }
        [field: SerializeField] public float MaxPlatformSpawnPositionByX { get; private set; }
        [field: SerializeField] public float MinPlatformSpawnPositionByY { get; private set; }
        [field: SerializeField] public float MaxPlatformSpawnPositionByY { get; private set; }
        [field: SerializeField] public float StartPlatformSpawnPositionByY { get; private set; }
        [field: SerializeField] public Vector2 SpawnPositionPlayer { get; private set; }

        [field: Space] [field: SerializeField] public float PlayerJumpForce { get; private set; }
        [field: SerializeField] public float PlayerMoveSpeed { get; private set; }
        [field: SerializeField] public float PlayerDeadDistance { get; private set; }
    }
}
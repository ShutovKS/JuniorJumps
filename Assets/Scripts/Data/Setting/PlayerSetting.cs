#region

using UnityEngine;

#endregion

namespace Data.Setting
{
    [CreateAssetMenu(fileName = "PlayerSetting", menuName = "Setting/PlayerSetting", order = 0)]
    public class PlayerSetting : ScriptableObject
    {
        [field: SerializeField] public float NormalJumpForce { get; private set; }
        [field: SerializeField] public float MoveSpeed { get; private set; }
        [field: SerializeField] public float DeadDistance { get; private set; }
    }
}
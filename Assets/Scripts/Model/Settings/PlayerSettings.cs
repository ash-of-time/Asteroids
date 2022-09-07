using UnityEngine;

namespace Model
{
    [CreateAssetMenu(menuName = "ScriptableObjects/PlayerSettings")]
    public class PlayerSettings : GameModelSettings
    {
        public float Acceleration { get; }
        public float Deceleration { get; }
        public Vector3 InitialPosition { get; }
    }
}
using UnityEngine;

namespace Model
{
    [CreateAssetMenu(menuName = "ScriptableObjects/PlayerSettings")]
    public class PlayerSettings : GameModelSettings
    {
        public float Acceleration;
        public float Deceleration;
        public float RotationSpeed;
        public Vector3 InitialPosition;
        public Vector3 BarrelPosition;
        public int LaserCharges;
        public int LaserReloadTime;
        public bool IsInvulnerable;
    }
}
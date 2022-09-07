using UnityEngine;

namespace Model
{
    public abstract class GameModelSettings : ScriptableObject
    {
        public GameObject Prefab;
        public float MaxVelocity;
    }
}
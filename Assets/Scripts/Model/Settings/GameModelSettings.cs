using UnityEngine;

namespace Model
{
    [CreateAssetMenu(menuName = "ScriptableObjects/GameModelSettings")]
    public class GameModelSettings : ScriptableObject
    {
        public GameObject Prefab;
        public float MaxVelocity;
    }
}
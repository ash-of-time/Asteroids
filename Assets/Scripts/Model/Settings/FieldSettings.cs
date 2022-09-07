using UnityEngine;

namespace Model
{
    [CreateAssetMenu(menuName = "ScriptableObjects/FieldSettings")]
    public class FieldSettings : ScriptableObject
    {
        public Vector2 Size;
    }
}
using UnityEngine;

namespace Model
{
    [CreateAssetMenu(menuName = "ScriptableObjects/GameSettings")]
    public class GameSettings : ScriptableObject
    {
        public FieldSettings FieldSettings;
        public PlayerSettings PlayerSettings;
        public GameModelSettings ProjectileSettings;
        public EnemySettings AsteroidSettings;
        public EnemySettings AsteroidPieceSettings;
        public EnemySettings SaucerSettings;
    }
}
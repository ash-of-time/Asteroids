﻿using UnityEngine;

namespace Model
{
    [CreateAssetMenu(menuName = "ScriptableObjects/EnemySettings")]
    public class EnemySettings : GameModelSettings
    {
        public int InitialCount;
        public int CreateCooldown;
        public int PlayerMinimumDistance;
    }
}
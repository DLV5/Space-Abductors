using System;
using UnityEngine;

[Serializable]
public class EnemyWave
{
    [Serializable]
    public class WavePart
    {
        [TagSelector] public string EnemyTag;
        [SerializeField, Range(0, 100)] private int _enemyCount;
        public int EnemyCount 
        { 
            get => _enemyCount; 
            set => _enemyCount = value;
        }
        public Vector3 TargetPoint { get; set;}
        [SerializeField, Range(0, 20)] private float _delayBetweenSpawn;
        public float DelayBetweenSpawn
        {
            get => _delayBetweenSpawn;
            set => _delayBetweenSpawn = value;
        }
        [SerializeField, Range(0, 20)] private float _timeUntilNextWave;

        public float TimeUntilNextWave
        {
            get => _timeUntilNextWave;
            set => _timeUntilNextWave = value;
        }
    }
    public WavePart[] WaveParts;

}

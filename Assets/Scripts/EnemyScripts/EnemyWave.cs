using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[System.Serializable]
public class EnemyWave
{
    [System.Serializable]
    public class WavePart
    {
        [TagSelector]
        public string EnemyTag;
        public int EnemyCount;
        [HideInInspector]
        public Vector3 TargetPoint;
        public float DelayBetweenSpawn;
        public float TimeUntilNextWave;
    }
    public WavePart[] WaveParts;

}

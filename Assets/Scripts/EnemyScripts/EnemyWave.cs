using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EnemyWave
{
    [System.Serializable]
    public class WavePart
    {
        [TagSelector]
        public string enemyTag;
        public int enemyCount;
        public Vector3 targetPoint;
        public float delayBetweenSpawn;
        public float timeUntilNextWave;
    }
    public WavePart[] waveParts;

}

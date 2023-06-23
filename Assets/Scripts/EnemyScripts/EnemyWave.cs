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
        public string enemyTag;
        public int enemyCount;
        [HideInInspector]
        public Vector3 targetPoint;
        public float delayBetweenSpawn;
        public float timeUntilNextWave;
    }
    public WavePart[] waveParts;

}

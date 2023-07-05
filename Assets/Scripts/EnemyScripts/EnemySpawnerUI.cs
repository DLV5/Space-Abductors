using TMPro;
using UnityEngine;

public class EnemySpawnerUI : MonoBehaviour
{
    private EnemySpawner _spawner;
    private TextMeshProUGUI _waveText;

    private void Start()
    {
        _spawner = GameObject.Find("EnemySpawner").GetComponent<EnemySpawner>();
        _waveText = GameObject.Find("WaveText").GetComponent<TextMeshProUGUI>();
        _spawner.WaveSpawned += UpdateWaveText;
        UpdateWaveText(1);
    }

    private void OnDisable()
    {
        _spawner.WaveSpawned -= UpdateWaveText;
    }

    private void UpdateWaveText(int currentWave)
    {
        _waveText.text = "Wave " + currentWave + " / " + _spawner.NumberOfWaves;
    }
}

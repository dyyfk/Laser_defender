using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
  [SerializeField] List<WaveConfig> waveConfigs;
  [SerializeField] bool looping = false;

  // Start is called before the first frame update
  void Start()
  {
    // if (waveConfigs.Count <= 0) return;
    StartCoroutine(SpawnAllWaves());
  }

  private IEnumerator SpawnAllWaves()
  {
    do
    {
      foreach (WaveConfig wave in waveConfigs)
      {
        yield return StartCoroutine(SpawnWave(wave));
      }
    } while (looping);
  }

  private IEnumerator SpawnWave(WaveConfig wave)
  {
    for (int i = 0; i < wave.GetNumberOfEnemies(); i++)
    {
      var newEnemy = Instantiate(wave.GetEnemyPrefab(),
           wave.GetWaypoints()[0].transform.position,
           Quaternion.identity);
      newEnemy.GetComponent<EnemyPath>().SetWaveConfig(wave);
      yield return new WaitForSeconds(wave.GetTimeBetweenSpawns());
    }
  }
}

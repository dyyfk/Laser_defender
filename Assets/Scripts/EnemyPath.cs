using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPath : MonoBehaviour
{
  [SerializeField] WaveConfig waveConfig;
  List<Transform> waypoints;
  float moveSpeed;
  int waypointIndex = 0;


  // Start is called before the first frame update
  void Start()
  {
    waypoints = waveConfig.GetWaypoints();
    moveSpeed = waveConfig.GetMoveSpeed();
    transform.position = waypoints[waypointIndex].transform.position;
  }

  // Update is called once per frame
  void Update()
  {
    Move();
  }
  public void SetWaveConfig(WaveConfig waveConfig)
  {
    this.waveConfig = waveConfig;
  }

  void Move()
  {
    if (waypointIndex < waypoints.Count)
    {
      Vector3 targetPos = waypoints[waypointIndex].transform.position;
      float moveAtThisFrame = moveSpeed * Time.deltaTime;
      transform.position = Vector2.MoveTowards(transform.position, targetPos, moveAtThisFrame);
      if (transform.position == targetPos)
      {
        // Reaching a point on the path
        waypointIndex++;
      }
    }
    else
    {
      // Last point on the path, could destroy itself after reaching the rendezvous
      Destroy(gameObject);
    }
  }
}

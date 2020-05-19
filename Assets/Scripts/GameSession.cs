using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSession : MonoBehaviour
{
  int score = 0;
  private void Awake()
  {
    SetUpSingleton();
  }

  private void SetUpSingleton()
  {
    int numberOfSession = FindObjectsOfType<GameSession>().Length;
    if (numberOfSession > 1)
    {
      // Trying to initialize a new Game Session every scene
      Destroy(gameObject);
    }
    else
    {
      // Game Session would continue to live into the next scene
      DontDestroyOnLoad(gameObject);
    }
  }

  public int GetScore()
  {
    return score;
  }

  public void AddToScore(int scoreValue)
  {
    score += scoreValue;
  }

  public void ResetGame()
  {
    Destroy(gameObject);
  }
}

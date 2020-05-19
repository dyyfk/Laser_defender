using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Level : MonoBehaviour
{
  [SerializeField] float delayInSeconds = 2f;
  public void LoadStartMenu()
  {
    SceneManager.LoadScene("Main Menu");
  }

  public void LoadGame()
  {
    GameSession gameSession = FindObjectOfType<GameSession>();
    if (gameSession)
    {
      gameSession.ResetGame();
    }
    SceneManager.LoadScene("Game");
  }

  public void LoadGameOver()
  {
    StartCoroutine(WaitAndLoad());
  }

  public void QuitGame()
  {
    Application.Quit();
  }

  IEnumerator WaitAndLoad()
  {
    yield return new WaitForSeconds(delayInSeconds);
    SceneManager.LoadScene("Game Over");
  }
}

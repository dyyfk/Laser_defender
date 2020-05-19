using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class HealthDisplay : MonoBehaviour
{
  Player player;
  Text healthText;

  // Start is called before the first frame update
  void Start()
  {
    healthText = GetComponent<Text>();
    player = FindObjectOfType<Player>();
  }

  // Update is called once per frame
  void Update()
  {
    if (player) // to prevent the users from playing from the game over scene
    {
      healthText.text = player.GetHealth().ToString();
    }
    else
    {
      healthText.text = 0f.ToString(); // Player is already destroyed, clear the health
    }
  }
}

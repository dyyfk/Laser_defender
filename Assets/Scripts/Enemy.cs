using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
  [Header("Enemy Stats")]
  [SerializeField] float health = 400f;
  [SerializeField] int score = 150;

  [Header("Shooting")]

  [SerializeField] float shotCounter;
  [SerializeField] float minTimeBtwShots = 0.2f;
  [SerializeField] float maxTimeBtwShots = 3f;
  [SerializeField] float projectileSpeed = 10f;
  [SerializeField] GameObject laserPrefab;

  [Header("Effects")]
  [SerializeField] AudioClip deathSound;
  [SerializeField] [Range(0, 1)] float deathSoundVolume = 0.75f;
  [SerializeField] AudioClip shootSound;
  [SerializeField] [Range(0, 1)] float shootSoundVolume = 0.25f;



  // Start is called before the first frame update
  void Start()
  {
    shotCounter = Random.Range(minTimeBtwShots, maxTimeBtwShots);
  }

  // Update is called once per frame
  void Update()
  {
    CountDownAndShoot();
  }

  private void CountDownAndShoot()
  {
    shotCounter -= Time.deltaTime;
    if (shotCounter <= 0f)
    {
      Fire();
      shotCounter = Random.Range(minTimeBtwShots, maxTimeBtwShots);
      // randomize the interval between shoots of enemies
    }
  }

  private void Fire()
  {
    // No need to use coroutine here, because the enemy does not control laser shooting period
    GameObject laser = Instantiate(laserPrefab, transform.position, Quaternion.identity) as GameObject;
    laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -projectileSpeed);
    AudioSource.PlayClipAtPoint(shootSound, Camera.main.transform.position, shootSoundVolume);
  }

  private void OnTriggerEnter2D(Collider2D other)
  {
    DamageDealer damageDealer = other.gameObject.GetComponent<DamageDealer>();

    if (!damageDealer) return; // Could be hit by the player rather than the laser
    ProcessHit(damageDealer);
  }

  private void ProcessHit(DamageDealer damageDealer)
  {
    health -= damageDealer.GetDamage();
    damageDealer.Hit(); // Destroy the laser

    if (health <= 0)
    {
      Die();
    }
  }

  private void Die()
  {
    FindObjectOfType<GameSession>().AddToScore(score);

    Destroy(gameObject); // Destroy the aircraft itself 
    AudioSource.PlayClipAtPoint(deathSound, Camera.main.transform.position, deathSoundVolume);
  }
}

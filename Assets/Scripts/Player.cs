using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
  [Header("Player")]
  [SerializeField] float movesSpeed = 10f;
  [SerializeField] float padding = 0.5f;
  [SerializeField] float health = 200f;
  [SerializeField] AudioClip deathSound;
  [SerializeField] [Range(0, 1)] float deathSoundVolume = 0.75f;
  [SerializeField] AudioClip shootSound;
  [SerializeField] [Range(0, 1)] float shootSoundVolume = 0.25f;

  [Header("Projectile")]
  [SerializeField] float projectileSpeed = 8f;
  [SerializeField] float projectileFiringPeriod = 0.1f;
  [SerializeField] GameObject laserPrefab;
  Coroutine firingCoroutine;

  float xMin, xMax;
  float yMin, yMax;


  public float GetHealth()
  {
    return health;
  }

  // Start is called before the first frame update
  void Start()
  {
    SetUpBoundary();
  }

  private void SetUpBoundary()
  {
    Camera gameCamera = Camera.main;
    // Viewport is pixel wise. e.g. (480 * 960)
    // WordPoint is normalized. e.g. (0.4, 0.8)
    xMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x + padding;
    xMax = gameCamera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x - padding;
    yMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).y + padding;
    yMax = gameCamera.ViewportToWorldPoint(new Vector3(0, 1, 0)).y - padding;
  }

  // Update is called once per frame
  void Update()
  {
    Move();
    Fire();
  }

  IEnumerator FireContinuously()
  {
    // In this coroutine, the player aircraft keeps firing until the users leave the space key
    while (true)
    {
      GameObject laser = Instantiate(laserPrefab, transform.position, Quaternion.identity) as GameObject;
      laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, projectileSpeed);
      AudioSource.PlayClipAtPoint(shootSound, Camera.main.transform.position, shootSoundVolume);
      // yield return means the condition for this coroutine to resume from suspension.
      yield return new WaitForSeconds(projectileFiringPeriod);
    }
  }


  private void Fire()
  {
    if (Input.GetButtonDown("Fire1"))
    {
      firingCoroutine = StartCoroutine(FireContinuously());
    }

    if (Input.GetButtonUp("Fire1"))
    {
      StopCoroutine(firingCoroutine);
    }
  }

  private void Move()
  {
    // Time.deltaTime * movesSpeed gives frame independent result
    // Time.deltaTime provides the time between the current and previous frame.
    float deltaX = Input.GetAxis("Horizontal") * Time.deltaTime * movesSpeed;
    float deltaY = Input.GetAxis("Vertical") * Time.deltaTime * movesSpeed;

    float newXPos = Mathf.Clamp(transform.position.x + deltaX, xMin, xMax);
    float newYPos = Mathf.Clamp(transform.position.y + deltaY, yMin, yMax);

    transform.position = new Vector2(newXPos, newYPos);
  }

  private void OnTriggerEnter2D(Collider2D other)
  {
    DamageDealer damageDealer = other.gameObject.GetComponent<DamageDealer>();

    if (damageDealer == null) return;

    ProcessHit(damageDealer);
  }

  private void ProcessHit(DamageDealer damageDealer)
  {
    health -= damageDealer.GetDamage();
    damageDealer.Hit(); // Destroy laser

    if (health <= 0)
    {
      Die();
    }
  }

  private void Die()
  {
    FindObjectOfType<Level>().LoadGameOver();
    Destroy(gameObject); // Destroy the aircraft itself 
    AudioSource.PlayClipAtPoint(deathSound, Camera.main.transform.position, deathSoundVolume);
  }
}

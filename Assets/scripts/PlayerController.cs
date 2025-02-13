using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public AudioClip shootSound;
    public float moveSpeed = 5f;
    public float rotationSpeed = 700f;
    public GameObject bulletPrefab;
    public Transform shootPoint;
    public float bulletSpeed = 10f;
    private Camera mainCamera;
    public float maxHealth = 100f;
    private float currentHealth;
    public Image lifeBar;
    public GameManager gameManager;

    void Awake()
    {
        mainCamera = Camera.main;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        currentHealth = maxHealth;
        lifeBar.fillAmount = 1;
    }

    void Update()
    {
        HandleMovement();
        HandleShooting();
        HandleRotation();
        if (currentHealth <= 0)
        {
            Death();
        }
    }

    void HandleMovement()
    {
        float moveY = Input.GetAxis("Vertical");
        // Mover en la dirección hacia la que mira el personaje
        Vector3 movement = transform.forward * moveY * moveSpeed * Time.deltaTime;
        transform.Translate(movement, Space.World);

        // Movimiento lateral (strafe)
        float moveX = Input.GetAxis("Horizontal");
        Vector3 strafeMovement = transform.right * moveX * moveSpeed * Time.deltaTime;
        transform.Translate(strafeMovement, Space.World);
    }

    void HandleRotation()
    {
        float mouseX = Input.GetAxis("Mouse X");
        transform.Rotate(0, mouseX * rotationSpeed * Time.deltaTime, 0);
    }

    void HandleShooting()
    {
        if (Input.GetMouseButtonDown(0))
        {
            ShootBullet();
        }
    }

    void ShootBullet()
    {
        if (shootSound != null)
        {
            AudioSource.PlayClipAtPoint(shootSound, transform.position);
        }
        GameObject bullet = Instantiate(bulletPrefab, shootPoint.position, shootPoint.rotation);
        Rigidbody bulletRb = bullet.GetComponent<Rigidbody>();
        bulletRb.velocity = shootPoint.forward * bulletSpeed;
        Destroy(bullet, 2f);
    }

    void Death()
    {
        gameManager.GameOver();
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        lifeBar.fillAmount = currentHealth / maxHealth;
    }
}
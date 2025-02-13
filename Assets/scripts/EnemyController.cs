using UnityEngine;
public class EnemyController : MonoBehaviour
{
    public AudioClip shootSound;
    public float health = 50f;
    public float speed = 3f;
    public Transform target;
    private Rigidbody rb;
    public float stopDistance = 5f;
    public GameObject deathExplosionEffect;
    public GameObject bulletPrefab;
    public Transform shootPoint;
    public float bulletSpeed = 10f;
    public float fireRate = 1f;
    private float nextFireTime = 0f;
    private GameManager gameManager;

    // Nueva variable para la velocidad de rotación
    public float rotationSpeed = 5f;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        if (target == null)
        {
            target = GameObject.FindGameObjectWithTag("Player").transform;
        }
        gameManager = FindObjectOfType<GameManager>();
    }

    void Update()
    {
        if (target == null) return;
        
        RotateTowardsTarget();
        MoveTowardsTarget();
        
        if (Time.time >= nextFireTime)
        {
            FireProjectile();
            nextFireTime = Time.time + fireRate;
        }
    }

    void RotateTowardsTarget()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        
        // Interpola suavemente la rotación actual hacia la rotación deseada
        transform.rotation = Quaternion.Slerp(
            transform.rotation,
            lookRotation,
            Time.deltaTime * rotationSpeed
        );
    }

    void MoveTowardsTarget()
    {
        float distanceToTarget = Vector3.Distance(transform.position, target.position);
        if (distanceToTarget > stopDistance)
        {
            // Usar transform.forward para moverse en la dirección que está mirando
            rb.MovePosition(transform.position + transform.forward * speed * Time.deltaTime);
        }
    }

    void FireProjectile()
    {
        if (bulletPrefab != null && shootPoint != null)
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
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        if (deathExplosionEffect != null)
        {
            Instantiate(deathExplosionEffect, transform.position, transform.rotation);
        }
        if (gameManager != null)
        {
            gameManager.YouWin();
        }
        Debug.Log("El enemigo ha muerto.");
        Destroy(gameObject);
    }
}
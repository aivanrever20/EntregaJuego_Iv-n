using UnityEngine;

public class BulletController : MonoBehaviour
{
    public float damage = 10f;   // Daño que hace la bala
    public GameObject explosionEffect; // Prefab de la explosión

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerController player = collision.gameObject.GetComponent<PlayerController>();
            if (player != null)
            {
                player.TakeDamage(damage); // Aplicar daño al jugador
            }
        }

        if (collision.gameObject.CompareTag("Enemy"))
        {
            EnemyController enemy = collision.gameObject.GetComponent<EnemyController>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage); // Aplicar daño al enemigo
            }
        }

        // Instanciar el efecto de explosión al impactar
        if (explosionEffect != null)
        {
            Instantiate(explosionEffect, transform.position, transform.rotation); // Crear la explosión
        }

        Destroy(gameObject); // Destruir la bala después de la colisión
    }
}

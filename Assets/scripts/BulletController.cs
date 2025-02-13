using UnityEngine;

public class BulletController : MonoBehaviour
{
    public float damage = 10f;   // Da�o que hace la bala
    public GameObject explosionEffect; // Prefab de la explosi�n

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerController player = collision.gameObject.GetComponent<PlayerController>();
            if (player != null)
            {
                player.TakeDamage(damage); // Aplicar da�o al jugador
            }
        }

        if (collision.gameObject.CompareTag("Enemy"))
        {
            EnemyController enemy = collision.gameObject.GetComponent<EnemyController>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage); // Aplicar da�o al enemigo
            }
        }

        // Instanciar el efecto de explosi�n al impactar
        if (explosionEffect != null)
        {
            Instantiate(explosionEffect, transform.position, transform.rotation); // Crear la explosi�n
        }

        Destroy(gameObject); // Destruir la bala despu�s de la colisi�n
    }
}

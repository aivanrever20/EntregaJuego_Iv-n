using UnityEngine;
using UnityEngine.SceneManagement;  // Para cargar la escena

public class GameManager : MonoBehaviour
{
    public GameObject panelGameOver;  // El panel de Game Over que aparecerá cuando el jugador muera
    public GameObject panelYouWin;

    void Start()
    {
        panelGameOver.SetActive(false); // Inicialmente ocultamos el panel
        panelYouWin.SetActive(false);
    }

    // Método que se llama cuando el jugador muere
    public void GameOver()
    {
        panelGameOver.SetActive(true); // Mostrar el panel de Game Over
        Cursor.lockState = CursorLockMode.Confined; // Desbloquear el cursor para interactuar con el UI
        Cursor.visible = true;  // Hacer visible el cursor

        // Aquí podrías detener la creación de enemigos si tienes esa lógica
    }

    // Método para reiniciar la escena
    public void LoadSceneLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);  // Cargar la escena actual
    }

    public void YouWin()
    {
        panelYouWin.SetActive(true); // Mostrar el panel de Game Over
        Cursor.lockState = CursorLockMode.Confined; // Desbloquear el cursor para interactuar con el UI
        Cursor.visible = true;  // Hacer visible el cursor

    }

}

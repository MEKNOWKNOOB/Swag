using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;


public class ButtonFunction : MonoBehaviour
{
    [SerializeField] private GameObject menuCanvas;
    public void PlayGame()
    {
        menuCanvas.SetActive(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1); 
    }

    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene(0);
        SceneManager.UnloadSceneAsync(2);
    }
}

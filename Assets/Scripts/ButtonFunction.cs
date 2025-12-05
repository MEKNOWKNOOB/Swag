using UnityEngine;
using UnityEngine.SceneManagement;
public class ButtonFunction : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1); 
    }
}

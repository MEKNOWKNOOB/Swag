using UnityEngine;

public class JukeBox : MonoBehaviour
{
    void Start()
    {
        
        AudioManager.Instance.PlayLoop("Music");
    }
}

using UnityEngine;

public class JukeBox : MonoBehaviour
{
    void Start()
    {
        
        AudioManager.instance.PlayLoop("Music");
    }
}

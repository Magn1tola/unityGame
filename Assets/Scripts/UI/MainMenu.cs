using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void NewStartButton()
    {
        SceneManager.LoadScene("GameScene");
    }
    
    public void ExitButton()
    {
        SceneManager.LoadScene("ExitScene");
    }
}

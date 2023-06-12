using UnityEngine;
using UnityEngine.SceneManagement;

public class Main : MonoBehaviour
{
    public void Lose() => SceneManager.LoadScene(SceneManager.GetActiveScene().name);
}
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject menu;

    [SerializeField] private KeyCode keyForPause;

    private bool _isShowing;

    private void Update()
    {
        if (Input.GetKeyDown(keyForPause))
        {
            _isShowing = !_isShowing;

            if (_isShowing)
            {
                Time.timeScale = 0;
                menu.SetActive(true);
            }
            else
            {
                Time.timeScale = 1;
                menu.SetActive(false);
            }
        }
    }
}
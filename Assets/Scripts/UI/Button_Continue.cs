using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenuUI;
    public Button startButton;

    private void Start()
    {
        // Добавляем обработчик события нажатия кнопки "START"
        startButton.onClick.AddListener(ResumeGame);
    }

    public void ResumeGame()
    {
        // Скрываем меню паузы
        pauseMenuUI.SetActive(false);

        // Восстанавливаем время в игре
        Time.timeScale = 1f;
    }
}
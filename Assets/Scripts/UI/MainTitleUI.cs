using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainTitleUI : MonoBehaviour
{
    [SerializeField] private Button _gameStartButton;
    [SerializeField] private Button _exitButton;
    [SerializeField] private string _nextSceneName;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _gameStartButton.onClick.AddListener(() => {
            GameStart();
        });

        _exitButton.onClick.AddListener(() => {
            GameExit();
        });
    }

    private void GameStart()
    {
        SceneManager.LoadScene(_nextSceneName);
    }

    private void GameExit()
    {
        Application.Quit();
    }
}

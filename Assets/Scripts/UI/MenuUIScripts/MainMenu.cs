using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject _authorPanel;
    private static int _firstLevelBuildIndex = 1;

    private void Awake()
    {
        _authorPanel.SetActive(false);
    }

    public void LoadGame()
    {
        SceneManager.LoadSceneAsync(_firstLevelBuildIndex);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}

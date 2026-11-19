using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject tutorialScreen;

    public void OpenScanner()
    {
        SceneManager.LoadScene("TutorialScene");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void OpenTutorialScreen()
    {
        tutorialScreen.SetActive(true);
    }

    public void CloseTutorialScreen()
    {
        tutorialScreen.SetActive(false);
    }
}

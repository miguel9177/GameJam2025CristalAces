using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void playGame()
    {
        SceneManager.LoadSceneAsync(1);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void playLevel1()
    {
        SceneManager.LoadSceneAsync(1);
    }

    public void playLevel2()
    {
        SceneManager.LoadSceneAsync(2);
    }

    public void playLevel3()
    {
        SceneManager.LoadSceneAsync(3);
    }

    public void playLevel4()
    {
        SceneManager.LoadSceneAsync(4);
    }

    public void playLevel5()
    {
        SceneManager.LoadSceneAsync(5);
    }
}

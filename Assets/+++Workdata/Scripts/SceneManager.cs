using UnityEditor;
using UnityEngine.SceneManagement;
using UnityEngine;

public class SceneManage : MonoBehaviour
{
    public void ExitGame()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
            Application.Quit();
#endif
    }

    public void StartGame()
    {
        SceneManager.LoadScene("Level_Scene");
    }

    public void ReturnMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}

using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
public class MainMenuButtonManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created




    public static void StartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}

using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class VideoManager : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    public RawImage rawImage; // UI element to display video
    public InputAction moveAlong;

    void Start()
    {
        //Allow the player to skip
        moveAlong.Enable();



        // Create a render texture
        RenderTexture renderTexture = new RenderTexture(1920, 1080, 0);
        videoPlayer.targetTexture = renderTexture;
        rawImage.texture = renderTexture;

        // Configure playback
        videoPlayer.loopPointReached += OnVideoEnd;
        videoPlayer.Play();
    }


    void FixedUpdate()
    {
        if (moveAlong.triggered)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }

    void OnVideoEnd(VideoPlayer vp)
    {
        // Load the next scene (by name or index)
        //SceneManager.LoadScene("NextSceneName");
        // OR by build index:
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
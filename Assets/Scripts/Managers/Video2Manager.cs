using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
public class Video2Manager : MonoBehaviour
{
    private VideoPlayer videoPlayer;
    private AudioSource audioSource;
    public InputAction moveAlong;
    void Start()
    {
        moveAlong.Enable();
        videoPlayer = GetComponent<VideoPlayer>();
        audioSource = GetComponent<AudioSource>();

        string url = System.IO.Path.Combine(Application.streamingAssetsPath, "intro.mp4");

        videoPlayer.source = VideoSource.Url;
        videoPlayer.url = url;
        videoPlayer.audioOutputMode = VideoAudioOutputMode.AudioSource;
        videoPlayer.SetTargetAudioSource(0, audioSource);

        videoPlayer.Prepare();
        videoPlayer.prepareCompleted += OnPrepared;
    }

    void OnPrepared(VideoPlayer vp)
    {
        vp.Play();
        audioSource.Play();
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
    static void EndVideo()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}

using UnityEngine;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class OutroController : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    //public string nextSceneName = "MainMenu";

    void Start()
    {
        if (videoPlayer == null)
        {
            Debug.LogError("VideoPlayer not assigned!");
            return;
        }

        // ❗关键：先准备视频
        videoPlayer.Prepare();
        videoPlayer.prepareCompleted += OnPrepared;
    }

    void OnPrepared(VideoPlayer vp)
    {
        vp.Play(); // ✅ 确保准备完成后再播放
        //vp.loopPointReached += OnVideoFinished;
    }

    //void OnVideoFinished(VideoPlayer vp)
    //{
    //    SceneManager.LoadScene(nextSceneName);
    //}
}

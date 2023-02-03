using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class PlayIntroCutscene : MonoBehaviour
{
    VideoPlayer videoPlayer = null;

    private void Start()
    {
        videoPlayer = GetComponent<VideoPlayer>();
        videoPlayer.Play();
        // when video is done playing, call the Load level function
        videoPlayer.loopPointReached += LoadNextLevel;
    }

    void LoadNextLevel(VideoPlayer videoPlayer)
    {
        int currentBuildIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentBuildIndex + 1);
    }
}

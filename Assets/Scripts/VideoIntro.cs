using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;

public class VideoIntro : MonoBehaviour
{
    public VideoPlayer videoPlayer; // Assign this in the inspector
    public GameObject menuUI; // Assign the menu UI that should appear after the video
    public GameObject videoCanvas; // The UI or GameObject containing the video player

    void Start()
    {
        menuUI.SetActive(false); // Hide the menu at the beginning
        videoPlayer.loopPointReached += OnVideoEnd; // Subscribe to event
        videoPlayer.Play(); // Play the video
    }

    void OnVideoEnd(VideoPlayer vp)
    {
        videoCanvas.SetActive(false); // Hide the video canvas
        menuUI.SetActive(true); // Show the menu
    }
}

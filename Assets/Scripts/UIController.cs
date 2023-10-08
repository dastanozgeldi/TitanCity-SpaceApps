using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
    public static UIController instance { get; private set; }

    public GameObject robotPanel;
    public GameObject smallMap;
    public GameObject largeMap;

    public GameObject[] videos;
    public float[] videoTimes;
    public GameObject[] videoObjects;

    public Player player;
    public GameObject backgroundSound;

    void Awake()
    {
        instance = this;
    }

    public void RobotButton()
    {
        backgroundSound.SetActive(false);
        robotPanel.SetActive(true);
        player.isPlaying = false;
    }

    public void RobotCloseButton()
    {
        backgroundSound.SetActive(true);
        robotPanel.SetActive(false);
        player.isPlaying = true;
    }

    public void MapButton()
    {
        smallMap.SetActive(false);
        largeMap.SetActive(true);
    }

    public void MapCloseButton()
    {
        smallMap.SetActive(true);
        largeMap.SetActive(false);
    }

    public void OnPlayVideo(int index)
    {
        videos[index].SetActive(true);
        Debug.Log(index);
        Destroy(videos[index], videoTimes[index]);
    }
}

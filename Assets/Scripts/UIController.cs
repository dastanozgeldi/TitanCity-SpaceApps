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

    void Awake()
    {
        instance = this;
    }

    public void RobotButton()
    {
        robotPanel.SetActive(true);
        player.isPlaying = false;
    }

    public void RobotCloseButton()
    {
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
        Destroy(videos[index], videoTimes[index]);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public bool isPlaying;

    public float speed;
    public float jumpForce;
    public Rigidbody rb;
    public GameObject astronaut;
    public int index;

    public GameObject firstViewCamera;
    bool isFirstView;
    bool isJumping;

    public Animator anim;

    void Start()
    {
        UIController.instance.OnPlayVideo(index);
        Invoke("EnablePlaying", UIController.instance.videoTimes[index]);
        index++;

        if(index<UIController.instance.videos.Length)
        {
            UIController.instance.videoObjects[index-1].SetActive(true);
        }
    }

    void Update()
    {
        if(isPlaying)
        {
            float h = Input.GetAxisRaw("Horizontal");
            float v = Input.GetAxisRaw("Vertical");

            rb.velocity = new Vector3(speed * h, rb.velocity.y, speed*v);

            if(h != 0 || v != 0)
            {
                transform.rotation = Quaternion.LookRotation(rb.velocity);

                anim.SetBool("isRun", true);
            }
            else
            {
                anim.SetBool("isRun", false);
            }

            if(Input.GetKeyDown(KeyCode.C))
            {
                isFirstView = !isFirstView;
                firstViewCamera.SetActive(isFirstView);
                Debug.Log("!");
            }

            if(Input.GetKeyDown(KeyCode.Space))
            {
                rb.AddForce(Vector3.up * jumpForce);
            }
        }
    }

    void EnablePlaying()
    {
        isPlaying = true;
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Video"))
        {
            isPlaying = false;
            UIController.instance.OnPlayVideo(index);
            Invoke("EnablePlaying", UIController.instance.videoTimes[index]);
            index++;

            if(index<UIController.instance.videos.Length)
            {
                UIController.instance.videoObjects[index-1].SetActive(true);
            }
        }
    }
}

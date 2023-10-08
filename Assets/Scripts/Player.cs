using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

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
    public bool isJump;

    public GameObject[] cityElements;

    public TextMeshProUGUI crystalText;
    public int crystalCount;
    public int maxCount;
    public int[] maxCounts;

    public TextMeshProUGUI yearText;
    public int yearCount;

    public GameObject arrow;

    void Start()
    {
        UIController.instance.OnPlayVideo(index);
        Invoke("EnablePlaying", UIController.instance.videoTimes[index]);
        index++;
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
                if(!isJump)
                {
                    transform.rotation = Quaternion.LookRotation(rb.velocity);
                    anim.SetBool("isRun", true);
                }
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
                if(!isJump)
                {
                    isJump = true;
                    rb.AddForce(Vector3.up * jumpForce);
                }
            }
        }
    }

    void EnablePlaying()
    {
        isPlaying = true;
    }

    void OnCollisionEnter(Collision other)
    {
        isJump = false;
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Video"))
        {
            isPlaying = false;
            UIController.instance.OnPlayVideo(index);
            Invoke("EnablePlaying", UIController.instance.videoTimes[index]);
            cityElements[index-2].SetActive(true);
            arrow.SetActive(false);
            yearCount+=200;
            yearText.text = "Year: " + yearCount.ToString();
        }
        else if(other.CompareTag("Crystal"))
        {
            Destroy(other.gameObject);
            crystalCount++;
            crystalText.text = crystalCount.ToString();
            maxCount--;
            if(maxCount == 0)
            {
                UIController.instance.videoObjects[index-1].SetActive(true);
                index++;
                maxCount = maxCounts[index-1];
                arrow.SetActive(true);
                //arrow.GetComponent<Arrow>().target = UIController.instance.videoObjects[index-1].transform;
            }
        }
    }
}

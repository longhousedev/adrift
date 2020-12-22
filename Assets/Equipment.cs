using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Equipment : MonoBehaviour
{
    
    //Torch
    public GameObject torch;
    public AudioClip torchSwitch;

    //Video Camera
    public GameObject camcorder;
    public AudioClip camcorderPlace;
    private bool _placingCamcorder;
    private int _camcorderAmount;

    //Health
    public GameObject healthPack;
    private bool _usingHealthPack;
    private int _healthPackAmount;
    
    // Start is called before the first frame update
    void Start()
    {
        //Not Placing
        _placingCamcorder = false;
        _camcorderAmount = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C) && _camcorderAmount > 0 && !_placingCamcorder) StartCoroutine(nameof(PlaceCamera));
        if (Input.GetKeyDown(KeyCode.L))
        {
            gameObject.GetComponent<AudioSource>().PlayOneShot(torchSwitch, 0.5f);
            if (torch.activeSelf)
            {
                torch.SetActive(false);
            }
            else
            {
                torch.SetActive(true);
            }
        }
    }

    IEnumerator PlaceCamera()
    {
        _placingCamcorder = true;
        float duration = Time.time + 2.5f;
        float elapsed = 0;
        float movement = gameObject.GetComponent<PlayerController>().movementModifier;
        gameObject.GetComponent<AudioSource>().PlayOneShot(camcorderPlace);
        while (elapsed < duration)
        {
            gameObject.GetComponent<PlayerController>().slow = 0.3f;
            yield return new WaitForSeconds(0.5f);
            elapsed = Time.time;
            if (!Input.GetKey(KeyCode.C))
            {
                _placingCamcorder = false; 
                gameObject.GetComponent<AudioSource>().Stop();
                gameObject.GetComponent<PlayerController>().slow = 1;
                yield break;
            }
        }
        _camcorderAmount--;
        var transform2 = transform;
        var transform1 = transform2.position;
        var rotation = transform2.rotation;
        Instantiate(camcorder, new Vector3(transform1.x, 0, transform1.z), rotation);
        gameObject.GetComponent<PlayerController>().slow = 1;
        _placingCamcorder = false;

    }
}

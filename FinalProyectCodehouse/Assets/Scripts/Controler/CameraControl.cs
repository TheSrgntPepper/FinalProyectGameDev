using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public GameObject CameraShowlder;
    public GameObject CameraTop;

    // Start is called before the first frame update
    void Start()
    {
        CameraShowlder.SetActive(true);
        CameraTop.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.E))
        {
            CameraShowlder.SetActive(false);
            CameraTop.SetActive(true);
        }
        if (Input.GetKeyUp(KeyCode.E))
        {
            CameraShowlder.SetActive(true);
            CameraTop.SetActive(false);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFlow : MonoBehaviour
{
    Transform Hero;

    [SerializeField]
    float z_camera = -10f;

    [SerializeField]
    float y_camera = 2f;

    [SerializeField]
    float v_camera = 0.6f;

    Vector3 velocity = Vector3.zero;

    void Start()
    {
        Hero = GameObject.FindGameObjectWithTag("Hero").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if(Hero != null)
        {
            Vector3 targetPosition = new Vector3(Hero.position.x, Hero.position.y + y_camera, Hero.position.z + z_camera);
            transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, v_camera);
        }
    }
}

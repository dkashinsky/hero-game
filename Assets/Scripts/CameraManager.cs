using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public GameObject hero;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (hero.transform.position.x > transform.position.x)
        {
            transform.position = new Vector3(
                hero.transform.position.x,
                transform.position.y,
                transform.position.z
                );
        }
    }
}

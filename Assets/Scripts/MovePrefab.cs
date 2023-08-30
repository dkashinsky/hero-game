using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePrefab : MonoBehaviour
{
    public GameObject hero;
    public GameObject prefabToMove;
    public GameObject sensorToActivate;
    private float deltaX = 76.86374f;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == hero)
        {
            prefabToMove.transform.position = new Vector2(
                prefabToMove.transform.position.x + deltaX,
                prefabToMove.transform.position.y
                );
            sensorToActivate.SetActive(true);
            gameObject.SetActive(false);
        }
    }
}

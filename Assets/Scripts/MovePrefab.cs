using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePrefab : MonoBehaviour
{
    public GameResultScriptableObject gameResult;
    public GameObject secretPrefab;
    public GameObject prefabToMove;
    public GameObject sensorToActivate;
    private float deltaX = 76.86374f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == KnownGameObjects.Player)
        {
            if (gameResult.score > 500)
            {
                secretPrefab.transform.position = new Vector2(
                    prefabToMove.transform.position.x + deltaX,
                    prefabToMove.transform.position.y
                );
            } 
            else 
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
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform playerTransform;
    // Start is called before the first frame update
    void Start()
    {
        if(playerTransform == null)
        {
            playerTransform = FindObjectOfType<PlayerController>().transform;
        }
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = new Vector3(playerTransform.position.x, playerTransform.position.y, transform.position.z);
    }
}

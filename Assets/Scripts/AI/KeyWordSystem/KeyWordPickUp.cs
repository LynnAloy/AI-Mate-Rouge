using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyWordPickUp : MonoBehaviour
{
    //Variables
    [SerializeField] private float moveSpeed;
    [SerializeField] private float timeBetweenCheck;
    //References
    [SerializeField] private PlayerController player;

    private bool isMovingToPlayer;
    private float checkCounter;
    // Start is called before the first frame update
    void Start()
    {
        if (player == null)
        {
            player = PlayerHealthController.Instance.GetComponent<PlayerController>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isMovingToPlayer == true)
        {
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position, moveSpeed * Time.deltaTime);
        }
        else
        {
            checkCounter -= Time.deltaTime;
            if (checkCounter <= 0)
            {
                checkCounter = timeBetweenCheck;
                if (Vector3.Distance(transform.position, player.transform.position) < player.GetPickUpRange())
                {
                    isMovingToPlayer = true;
                    moveSpeed += player.GetPlayerMoveSpeed();
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            KeyWordController.Instance.AddKeyWord();
            Destroy(gameObject);
        }
    }
}

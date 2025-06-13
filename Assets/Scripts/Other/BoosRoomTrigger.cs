using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoosRoomTrigger : MonoBehaviour
{
    public GameObject boss;
    public GameObject bossHealth;
    public GameObject leftDoor;
    public GameObject rightDoor;
    public Sprite leftDoorClose;
    public Sprite rightDoorClose;

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
        if (collision.CompareTag("Player"))
        {
            if (boss != null && bossHealth != null)
            {
                boss.SetActive(true);
                bossHealth.SetActive(true);
            }
            if (leftDoor != null && rightDoor != null)
            {
                leftDoor.GetComponent<SpriteRenderer>().sprite = leftDoorClose;
                leftDoor.GetComponent<BoxCollider2D>().enabled = true;
                rightDoor.GetComponent<SpriteRenderer>().sprite = rightDoorClose;
                rightDoor.GetComponent<BoxCollider2D>().enabled = true;
            }
        }
    }
}

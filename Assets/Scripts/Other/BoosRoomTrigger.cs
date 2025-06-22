using UnityEngine;

public class BoosRoomTrigger : MonoBehaviour
{
    [SerializeField] GameObject boss;
    [SerializeField] GameObject bossHealth;
    [SerializeField] GameObject leftDoor;
    [SerializeField] GameObject rightDoor;
    [SerializeField] Sprite leftDoorClose;
    [SerializeField] Sprite rightDoorClose;

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

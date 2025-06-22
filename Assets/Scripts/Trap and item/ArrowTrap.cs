using System.Collections;
using UnityEngine;

public class ArrowTrap : MonoBehaviour
{
    [SerializeField] GameObject arrowPrefab;

    private float TimeInterval = 2f;

    void Start()
    {
        StartCoroutine(SpawnArrow());
    }

    void Update()
    {
        
    }

    IEnumerator SpawnArrow()
    {
        while (true)
        {
            GameObject arrow = Instantiate(arrowPrefab, transform.position, Quaternion.identity);
            yield return new WaitForSeconds(TimeInterval);
        }
    }
}

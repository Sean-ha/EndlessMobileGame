using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject tempEnemy;

    private float spawnTimer = 0.25f;

    private void Start()
    {
        StartCoroutine(SpawnLoop());
    }

    private IEnumerator SpawnLoop()
    {
        while(true)
        {
            SpawnEnemy();
            yield return new WaitForSeconds(spawnTimer);
        }
    }

    private void SpawnEnemy()
    {
        float yPos = Random.Range(1.5f, 4.4f);

        GameObject enem = Instantiate(tempEnemy, new Vector2(-3.6f, yPos), Quaternion.identity);
        LeanTween.moveLocalX(enem, 3.6f, Random.Range(6f, 9f));
        Destroy(enem, 9);
    }
}

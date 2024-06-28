using UnityEngine;
using UnityEngine.UI;

public class StartWaveButton : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GetComponent<Button>().onClick.AddListener(() =>
        {
            var spawners = FindObjectsOfType<EnemySpawner>();
            foreach (var spawner in spawners)
            {
                spawner.StartSpawning();
            }
            {
                
            }
            Destroy(gameObject);
        });
    }

}

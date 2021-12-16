using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class LevelBuilder : MonoBehaviour
{
    public GameObject[] levelModules;
    public float moduleSize;
    public Transform modulesContainer;
    private int moduleOrder;
    private Queue<GameObject> builtModules;
    public int queueLength;
    public int startModuleAmount;
    public bool spawnEnemies;
    public GameObject enemy;
    public float enemyProbability;

    public PlayerController playerController;

    private void Awake()
    {
        builtModules = new Queue<GameObject>();
        moduleOrder = 0;
    }

    private void Start()
    {
        Clear();

        if (Application.isPlaying)
        {
            bool temp = spawnEnemies;
            spawnEnemies = false;
            for (int i = 0; i < startModuleAmount; i++)
            {
                AddModule();
            }
            spawnEnemies = temp;
        }

        StartCoroutine(Timer());
    }

    IEnumerator Timer()
    {
        float t = moduleSize / (playerController.playerForwardSpeed);

        while (Application.isPlaying)
        {
            yield return new WaitForSeconds(t);
            AddModule();
        }
    }

    [ContextMenu("Add module")]
    public void AddModule()
    {
        Vector3 modulePosition = Vector3.forward * moduleSize * moduleOrder;
        int index = Random.Range(0, levelModules.Length);
        GameObject module = levelModules[index];
        GameObject newModule = Instantiate(module, modulePosition, module.transform.rotation, modulesContainer);
        builtModules.Enqueue(newModule);
        moduleOrder++;

        if (builtModules.Count > queueLength)
        {
            RemoveModule();
        }

        float rnd = Random.Range(0f, 1f);
        rnd += LevelManager.difficulty / 100;
        if (spawnEnemies && rnd < enemyProbability)
        {
            SpawnEnemy();
        }
    }

    private void SpawnEnemy()
    {
        Instantiate(enemy);
    }

    [ContextMenu("Remove module")]
    public void RemoveModule()
    {
        Destroy(builtModules.Dequeue());
    }

    [ContextMenu("Clear")]
    public void Clear()
    {
        for (int i = 0; i < builtModules.Count; i++)
        {
            RemoveModule();
        }

        moduleOrder = 0;
    }
}

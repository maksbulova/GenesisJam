using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    public GameObject[] levelModules;
    public float blockSize;
    public Transform modulesContainer;
    private int moduleOrder;
    private Queue<GameObject> builtModules;
    public int queueLength;
    public int startModuleAmount;

    private void Awake()
    {
        builtModules = new Queue<GameObject>();
    }

    private void Start()
    {
        for (int i = 0; i < startModuleAmount; i++)
        {
            AddModule();
        }
    }

    [ContextMenu("Add module")]
    public void AddModule()
    {
        Vector3 modulePosition = Vector3.forward * blockSize * moduleOrder;
        int index = Random.Range(0, levelModules.Length - 1);
        GameObject module = levelModules[index];
        GameObject newModule = Instantiate(module, modulePosition, module.transform.rotation, modulesContainer);
        builtModules.Enqueue(newModule);
        moduleOrder++;

        if (builtModules.Count > queueLength)
        {
            RemoveModule();
        }
    }

    [ContextMenu("Remove module")]
    public void RemoveModule()
    {
        Destroy(builtModules.Dequeue());
    }
}

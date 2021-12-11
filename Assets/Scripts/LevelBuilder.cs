using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class LevelBuilder : MonoBehaviour
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
        moduleOrder = 0;
    }

    private void Start()
    {
        if (Application.isPlaying)
        {
            for (int i = 0; i < startModuleAmount; i++)
            {
                AddModule();
            }
        }
    }

    [ContextMenu("Add module")]
    public void AddModule()
    {
        Vector3 modulePosition = Vector3.forward * blockSize * moduleOrder;
        int index = Random.Range(0, levelModules.Length);
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

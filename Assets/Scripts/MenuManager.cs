using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    public void StartGame()
    {
        Debug.Log("button");
        SceneManager.LoadScene("VirtualWorld");
        DelayedInit(1);
    }

    IEnumerator DelayedInit(float t)
    {
        yield return new WaitForSeconds(t);

        FindObjectOfType<LevelManager>().Initialize();
    }
}

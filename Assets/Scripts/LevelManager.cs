using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static float difficulty;
    public static float progress;
    public static float timeLeft;

    public PlayerController playerController;

    public string anotherWorld;
    public float maxTime;

    private void Start()
    {
        timeLeft = maxTime;
    }

    IEnumerable Simulation()
    {
        while (Application.isPlaying)
        {
            timeLeft -= Time.deltaTime;

            yield return null;
        }
    }

    public IEnumerator ChangeWorld()
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(anotherWorld);

        // Wait until the asynchronous scene fully loads
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }

    public void AddTime(float time)
    {
        timeLeft += time;
    }
}

using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.SceneManagement;

public  static class Utilities
{
    // Start is called before the first frame update
    public static int PlayerDeaths = 0;
    public static void RestartLevel()
    {
        SceneManager.LoadScene(0);
        Time.timeScale = 1f;
    }
    public static bool RestartLevel(int sceneIndex)
    {
        if (sceneIndex < 0)
        {
            throw new System.ArgumentOutOfRangeException("Scene index cannot be negative");
        }
        Debug.Log("Player death:"+PlayerDeaths);
        string message= UpdateDeathCount(ref PlayerDeaths);
        Debug.Log("Player deaths"+PlayerDeaths);
        Debug.Log(message);

        SceneManager.LoadScene(sceneIndex);
        Time.timeScale = 1f;
        return true;
    }
    public static string UpdateDeathCount(ref int countReference)
    {
        countReference += 1;
        return "Next time you'll be at number"+countReference;
    }
}

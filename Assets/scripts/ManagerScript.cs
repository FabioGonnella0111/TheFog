using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System.Collections;

public class ManagerScript : MonoBehaviour
{
    bool fin = false;
    public float spawnDelay = 1f;
    private List<string> logMessages = new List<string>();
 
    void Update()
    {
        CountMessage();
    }

    public void EndGame()
    {
        if (fin == false)
        {
            fin = true;
            Debug.Log("######Game Over######");
            Restart();
        }
    }

    void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void CustomLog(string message)
    {
        logMessages.Add(message);
         Debug.Log(message);
    }

    public int CountSpecificLog(string specificMessage)
    {
        int count = 0;

        foreach (string message in logMessages)
        {
            if (message.Contains(specificMessage))
            {
                count++;
            }
        }

        return count;
    }

    public int CountMessage()
    {
        int count = CountSpecificLog("morto");

        return count;
    }

}


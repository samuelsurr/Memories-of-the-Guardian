using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextScene : MonoBehaviour
{
    public void ContinueToLevek1()
    {
        SceneManager.LoadSceneAsync("Level1");
    }
    public void ContinueToLevel2()
    {
        SceneManager.LoadSceneAsync("Level2");
    }
    public void ContinueToLevel3()
    {
        SceneManager.LoadSceneAsync("Level3");
    }
    public void ContinueToFINALBOSS()
    {
        SceneManager.LoadSceneAsync("FinalBoss");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            SceneManager.LoadSceneAsync("WIN");
        }
    }
}

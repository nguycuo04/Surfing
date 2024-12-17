using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenceManager : MonoBehaviour
{
 
    public void StartTheGameLevel1()
    {
        SceneManager.LoadScene(0);
    }
    public void NextLevel2 ()
    {
        SceneManager.LoadScene(1);
    }
    public void NextLevel3()
    {
        SceneManager.LoadScene(2);
    }
    public void NextLevel4()
    {
        SceneManager.LoadScene(3);
    }

}

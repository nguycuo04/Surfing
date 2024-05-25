using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenceManager : MonoBehaviour
{
   
    //[SerializeField] private PlayerController playerController;
   
    // Start is called before the first frame update
    void Start()
    {
       // playerController = GameObject.Find(" Player").GetComponent<PlayerController>();
       
    }

    // Update is called once per frame
    void Update()
    {
        //if (playerController.moveNextLevel == true)
        //{
          //  Level2();
        //}
    }
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


}

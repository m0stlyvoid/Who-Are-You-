using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 

public class ButtonScript : MonoBehaviour
{
    
    public void LoadMainScene()
    {
        SceneManager.LoadScene(1);
    }

    private void Update()
    {
      if(Input.GetKeyDown("space"))
        {
            SceneManager.LoadScene(1);
        }
    }
}

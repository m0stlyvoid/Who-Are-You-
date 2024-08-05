using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class spawnButtons : MonoBehaviour
{
    [SerializeField] List<GameObject> Buttons;
    [SerializeField] GameObject spawnThis;
    [SerializeField] GameObject parentCanvas;

    // Start is called before the first frame update
    void Start()
    {
        //Instantiate(Buttons[Random.Range(0, Buttons.Count)], parentCanvas.Transform);    
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

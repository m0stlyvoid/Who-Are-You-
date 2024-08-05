using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class endPopUP : MonoBehaviour
{
    [SerializeField] Image putPersonHere;
    [SerializeField] List<Image> person;
    private Image face ;
   
    
    // Start is called before the first frame update
    void Start()
    {
        // putPersonHere.image = personp[Random.Range(0, person.Count)];
        face = putPersonHere.gameObject.GetComponent<Image>();
       face = person[Random.Range(0, person.Count)];
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

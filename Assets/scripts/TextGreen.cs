using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class TextGreen : MonoBehaviour
{
    [SerializeField] List<string> greenTexts;
    private TextMeshProUGUI textMesh;
    public int numberGreen;
    public GameObject CommentText;
    public comment Comment;


    //SerializeField] private GameObject textObject;
    
    

    // Start is called before the first frame update
    void Start()
    {
        textMesh = gameObject.GetComponent<TextMeshProUGUI>();
        numberGreen = Random.Range(0, greenTexts.Count);
        textMesh.text = greenTexts[numberGreen];
        greenTexts.RemoveAt(numberGreen);
        
    
    }
   
    public void Reload()
    {
        numberGreen = Random.Range(0, greenTexts.Count);
        textMesh.text = greenTexts[numberGreen];
        greenTexts.RemoveAt(numberGreen);
    }
}

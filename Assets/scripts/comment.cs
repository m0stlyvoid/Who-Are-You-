using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class comment : MonoBehaviour
{
    [SerializeField] List<string> redComments;
    private TextMeshProUGUI text;
    [SerializeField] List<string> greenComments;
    [SerializeField] List<string> blueComments;
  
    public textRed TextRed;
    [SerializeField] GameObject red;
    public TextGreen textGreen;
    [SerializeField] GameObject green;
    public textBlue TextBlue;
    [SerializeField] GameObject blue;

    public Kugel movement;
    [SerializeField] GameObject kugel;



    void Awake()
    {
        TextRed = red.GetComponent<textRed>();
        textGreen = green.GetComponent<TextGreen>();
        TextBlue = blue.GetComponent<textBlue>();
        movement = kugel.GetComponent<Kugel>();
    }

    private void Start()
    {
        Debug.Log("comment:" + TextBlue.numberBlue);
    }
    // Start is called before the first frame update
    public void DisplayComment()
    {
        Debug.Log("comment display");
        text = gameObject.GetComponent<TextMeshProUGUI>();

        if (movement.hitRed)
        {
            text.text = redComments[TextRed.numberRed];
            redComments.RemoveAt(TextRed.numberRed);
            TextRed.Reload();         
        }
        
        if (movement.hitBlue)
        {
            text.text = blueComments[TextBlue.numberBlue];
            blueComments.RemoveAt(TextBlue.numberBlue);
            TextBlue.Reload();    
        }   

        if(movement.hitGreen)
        {
            text.text = greenComments[textGreen.numberGreen];
            greenComments.RemoveAt(textGreen.numberGreen);
            textGreen.Reload();
        }   
    }
    
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class textBlue : MonoBehaviour
{
    [SerializeField] List<string> blueTexts;
    private TextMeshProUGUI textMeshBlue;
    public int numberBlue;

    // Start is called before the first frame update
    void Start()
    {
        textMeshBlue = gameObject.GetComponent<TextMeshProUGUI>();
        numberBlue = Random.Range(0, blueTexts.Count);
        textMeshBlue.text = blueTexts[numberBlue];
        blueTexts.RemoveAt(numberBlue);
        Debug.Log("Bluetext:" + numberBlue);
    }

    // Update is called once per frame
    public void Reload()
    {
       // blueTexts.RemoveAt(numberBlue);
        numberBlue = Random.Range(0, blueTexts.Count);
        textMeshBlue.text = blueTexts[numberBlue];
        Debug.Log("reloaded");
        blueTexts.RemoveAt(numberBlue);
    }
}

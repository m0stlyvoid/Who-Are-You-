using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class textRed : MonoBehaviour
{
    [SerializeField] List<string> redTexts;
    private TextMeshProUGUI textMeshRed;
    public int numberRed;

    // Start is called before the first frame update
    void Start()
    {
        textMeshRed = gameObject.GetComponent<TextMeshProUGUI>();
        numberRed = Random.Range(0, redTexts.Count);
        textMeshRed.text = redTexts[numberRed];
        Debug.Log(numberRed);
        redTexts.RemoveAt(numberRed);
    }

   public void Reload()
    {
        numberRed = Random.Range(0, redTexts.Count);
        textMeshRed.text = redTexts[numberRed];
        Debug.Log(numberRed);
        redTexts.RemoveAt(numberRed);
    }
}

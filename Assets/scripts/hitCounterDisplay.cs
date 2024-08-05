using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class hitCounterDisplay : MonoBehaviour
{
    private TextMeshProUGUI textMeshPro;
    public Kugel movement;
    [SerializeField] GameObject kugel;
    

    private void Start()
    {
        movement = kugel.GetComponent<Kugel>();
        textMeshPro = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
       
        textMeshPro.text = movement.hitCount + " / 9";
    }
}

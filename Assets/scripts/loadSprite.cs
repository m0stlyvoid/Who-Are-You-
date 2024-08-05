using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class loadSprite : MonoBehaviour
{
    [SerializeField] List<Sprite> sprite;
     private Image image;
    // Start is called before the first frame update
    void Start()
    {
        image = GetComponent<Image>();
        image.sprite = sprite[Random.Range(0, sprite.Count)];
    }

   public void reloadOnButtonPress()
    {
        image = GetComponent<Image>();
        image.sprite = sprite[Random.Range(0, sprite.Count)];
    }
}

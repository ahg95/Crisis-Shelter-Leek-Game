using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class changeSprint : MonoBehaviour
{
    public Image img;
    public Sprite original;
    public Sprite changed;
    
    // Start is called before the first frame update
    void Start()
    {
        img.sprite = original;
    }

    public void ChangeSprite()
    {
        img.sprite = changed;
    }
    public void ChangeOriginal()
    {
        img.sprite = original;
    }
}

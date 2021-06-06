using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class animationGradControl : MonoBehaviour
{
    public Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S)) 
        {
            anim.SetBool("StartAnim", false);
        }
    }

    public void StartAnim()
    {
        anim.SetBool("StartAnim", true);
    }
}

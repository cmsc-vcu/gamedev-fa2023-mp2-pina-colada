using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MirroredProperties : MonoBehaviour
{
    public static bool grounded = false;
    public static bool invGrounded = false;
    public static bool touch = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(grounded || invGrounded){
            touch = true;
        }
        else
            touch = false;
    }
}

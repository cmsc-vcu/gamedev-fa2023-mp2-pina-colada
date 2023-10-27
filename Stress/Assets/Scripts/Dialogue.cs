using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Dialogue : MonoBehaviour
{

    public TMP_Text dial;
    public TMP_Text dialB;
    public string[] linesA;
    public string[] linesB;
    public float textSpeed;

    public static int index;


    // Start is called before the first frame update
    void Start()
    {
        dial.text = string.Empty;
        dialB.text = string.Empty;
        StartDialogue();
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.x > 150f && index == 0)
        {
            nextLine();
        }
        if(transform.position.x > 300f && index == 1)
        {
            nextLine();
        }
        if(transform.position.x > 450f && index == 2)
        {
            nextLine();
        }
        if(transform.position.x > 600f && index == 3)
        {
            nextLine();
        }
        if(transform.position.x > 750f && index == 4)
        {
            nextLine();
        }
        if(transform.position.x > 1000f && index == 5)
        {
            nextLine();
        }
        if(transform.position.x > 1150f && index == 6)
        {
            nextLine();
        }
        if(transform.position.x > 1300f && index == 7)
        {
            nextLine();
        }     
    }

    void StartDialogue()
    {
        index = 0;
        StartCoroutine(TypeLine());
    }
    
    IEnumerator TypeLine()
    {
        foreach(char c in linesA[index].ToCharArray())
        {
            dial.text += c;
            yield return new WaitForSeconds(textSpeed);
        }
        yield return new WaitForSeconds(2f);
    
        foreach(char c in linesB[index].ToCharArray())
        {
            dialB.text += c;
            yield return new WaitForSeconds(textSpeed);
        }
        yield return new WaitForSeconds(4f);
        dial.text = string.Empty;
        dialB.text = string.Empty;
    }

    void nextLine()
    {
        if(index < linesA.Length - 1)
        {
            index++;
            dial.text = string.Empty;
            dialB.text = string.Empty;
            StartCoroutine(TypeLine());
        }
    }

}

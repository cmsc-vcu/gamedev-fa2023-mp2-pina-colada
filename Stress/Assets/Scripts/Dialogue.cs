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

    private int index;


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
        if(dial.text == linesA[index])
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
        yield return new WaitForSeconds(2f);
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

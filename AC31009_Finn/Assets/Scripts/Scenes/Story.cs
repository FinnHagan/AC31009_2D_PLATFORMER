using System.Collections;
using UnityEngine;
using TMPro;

//https://youtu.be/8oTYabhj248 Reference video

public class Story : MonoBehaviour
{
    public TextMeshProUGUI storyText;
    public string[] newLine;
    public float textSpeed;
    private int index;

    // Start is called before the first frame update
    void Start()
    {
        storyText.text = string.Empty;
        StartDialogue();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (storyText.text == newLine[index])
            {
                NextLine();
            }
            else
            {
                StopAllCoroutines();
                storyText.text = newLine[index];
            }
        }
    }

    void StartDialogue()
    {
        index = 0;
        StartCoroutine(TypeLine());
    }

    IEnumerator TypeLine()
    {
        foreach (char c in newLine[index].ToCharArray())
        {
            storyText.text += c;
            yield return new WaitForSeconds(textSpeed);
        }
    }

    void NextLine()
    {
        if (index < newLine.Length - 1)
        {
            index++;
            storyText.text = string.Empty;
            StartCoroutine(TypeLine());
        }
        else
        {
            gameObject.SetActive(false);
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;

[System.Serializable]
public class UnityStringEvent : UnityEvent<string> { }

public class agent : MonoBehaviour
{
    public UnityStringEvent onAgentEvent;
    public TextMeshPro dialogueText;

    public string testText = "What is 6 times 7?";
    public string prompt;

    public agent otherAgent;
    public bool isStartingSpeaker = false;

    public float responseDelay = 10f;

    Color originalColor;
    // Start is called before the first frame update
    void Start()
    {
        // Example prompt 
        // Formulate a philosophical response to the following text in under 100 words, 
        // relevant to the tradition of German Idealism. Don't mention German Idealism explicitly. 
        // Begin you response with a pondering interjection. Finish your response with a novel question. Keep it varied. 
        // Never use the triad 'Thesis, Antithesis, Synthesis'. The text you should respond to is:  

        originalColor = dialogueText.color;

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T) && isStartingSpeaker)
        {
            onAgentEvent.Invoke(testText);
        }
    }

    public void Response(string message)
    {
        Debug.Log("Agent received message: " + message);
        StartCoroutine(RespondWithDelay(message, responseDelay));
    }

    IEnumerator RespondWithDelay(string message, float delay)
    {
        yield return new WaitForSeconds(delay);
        onAgentEvent.Invoke(prompt + " " + message);
    }

    public void HandleStringEvent(string message)
    { 
        Debug.Log("Agent received message: " + message);
        
        dialogueText.text = message;
        StartCoroutine(FadeText());

        // call response on the other agent
        otherAgent.Response(message);
    }


    IEnumerator FadeText()
        {
            dialogueText.color = new Color(originalColor.r, originalColor.g, originalColor.b, 255f); 
            float elapsedTime = 0f;

            while (elapsedTime < responseDelay)
            {           
                elapsedTime += Time.deltaTime; // increment elapsed time based on delta time
                yield return null; // wait for the next frame

                if (elapsedTime > responseDelay / 2)
                {
                    float alpha = Mathf.Lerp(1f, 0f, (elapsedTime - responseDelay / 2) / responseDelay); // calculate alpha value based on time elapsed
                    Color newColor = new Color(originalColor.r, originalColor.g, originalColor.b, alpha); // create new color with updated alpha value
                    dialogueText.color = newColor; // assign the new color to the textMesh
                }
            }

            dialogueText.color = new Color(originalColor.r, originalColor.g, originalColor.b, 0f); // set the final alpha value to 0
        }


}

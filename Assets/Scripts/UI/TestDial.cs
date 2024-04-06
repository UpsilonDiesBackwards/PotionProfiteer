using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TestDial : MonoBehaviour
{
    [Header("References")]
    public GameObject player;

    [Header("Dialogue")]
    private List<string> currentDialogueLines = new List<string>();
    public TextMeshProUGUI dialogueText;
    public float dialogueTypeSpeed;
    public float advanceDelay = 1;
    private bool typing = false;
    private bool canAdvanceDialogue = true;

    private void Update()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    public void OpenDialogue(List<string> dialogue)
    {
        if (canAdvanceDialogue)
        {
            currentDialogueLines = dialogue;
            StartCoroutine(TypeDialogue());

            canAdvanceDialogue = false;
            player.GetComponent<Player>().frozen = true;
        }
    }

    public void CloseDialogue()
    {
        dialogueText.gameObject.SetActive(false);
        dialogueText.text = "";
    }

    IEnumerator TypeDialogue()
    {
        typing = true;
        dialogueText.gameObject.SetActive(true);

        foreach (string line in currentDialogueLines)
        {
            dialogueText.text = "";

            foreach (char letter in line)
            {

                dialogueText.text += letter;
                yield return new WaitForSeconds(dialogueTypeSpeed);
            }
            yield return new WaitForSeconds(advanceDelay);
        }

        typing = false;
        // currentDialogueLines.Clear();
        canAdvanceDialogue = true;
        player.GetComponent<Player>().frozen = false;

        CloseDialogue();
    }
}
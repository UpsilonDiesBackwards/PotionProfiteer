using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class DialogueBox : MonoBehaviour
{
    public GameObject player;

    [SerializeField] private TextMeshProUGUI _textComp;
    [SerializeField] private string[] _dialLines;
    public float typingSpeed = 0.05f;

    private int _index;
    public bool startDial = false;
    public static bool finishedDial = false;

    private void Start()
    {
       // gameObject.SetActive(false);
    }

    void Update() {

        player = GameObject.FindGameObjectWithTag("Player");
       
        if ( NPC_Shopping.isNPCInShop == true)
        {
            gameObject.SetActive(true);
        }
        NPCTalk();
        if (Input.GetMouseButtonDown(0)) {
            if (_textComp.text == _dialLines[_index]) { //if the dialogue is complete go to the next line
                NextLine();
            } else { //instantly fills up the text box for quicker reading
                StopAllCoroutines();
                _textComp.text = _dialLines[_index];
            }
        }
    }

    void StartDialogue() {
        _textComp.text = "";
        _index = 0;
        StartCoroutine(TypeLines());
    }

    IEnumerator TypeLines() { //types out each letter one by one
        foreach (char c in _dialLines[_index].ToCharArray()) { //breaks the string down into a character array
            _textComp.text += c; //has the text component that is being broken down into its individual letters
            yield return new WaitForSeconds(typingSpeed);
        }
    }

    void NextLine() {
        if (_index < _dialLines.Length -1 ) {
            _index++; //goes to the next line in the array
            _textComp.text = string.Empty; //makes the text empty so its not the previous line
            StartCoroutine(TypeLines()); //types the current array line
        } else {
            gameObject.SetActive(false);
            finishedDial = true;
        }
    }

    void NPCTalk()
    {
        if (NPC_InStore.atCounter == true && finishedDial == false)
        {
            player.GetComponent<Player>().frozen = true;
            
            StartDialogue();
            
        }
       
    }
}

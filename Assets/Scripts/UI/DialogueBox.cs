using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class DialogueBox : MonoBehaviour
{
    public GameObject player;
    public GameObject[] npcs;

    [SerializeField] private TextMeshProUGUI _textComp;
    [SerializeField] private string[] _dialLines;
    public float typingSpeed = 0.05f;

    private int _index;
    public bool startDial = false;
    public static bool finishedDial = false;

    private void Start()
    {
        // gameObject.SetActive(false);

        npcs = GameObject.FindGameObjectsWithTag("NPC");
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
            //SOMEHOW GET NPC_INSTORE HEAD TO EXIT FUNCTION HERE
        }
    }

    void NPCTalk()
    {
        for (int i = 0; i < npcs.Length; i++) //getting the npc index and seeing if that npc is close to the player if they are starts dialogue
        {
            float proximity = 5f;

            Vector3 npcPos = npcs[i].transform.position;
            Vector3 playerPos = player.transform.position;

            float distance = Vector3.Distance(npcPos, playerPos);

            if (distance < proximity)
            {
                player.GetComponent<Player>().frozen = true;

                StartDialogue();
            }
        }
    }
}

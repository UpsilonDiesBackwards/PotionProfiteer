using System.Collections;
using TMPro;
using UnityEngine;

public class DialogueBox : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _textComp;
    [SerializeField] private string[] _dialLines;
    public float typingSpeed = 0.05f;

    private int _index;

    void Start() {
        _textComp.text = "";
        StartDialogue();
    }

    void Update() {
        if (Input.GetMouseButtonDown(0)) {
            if (_textComp.text == _dialLines[_index]) { 
                NextLine();
            } else {
                StopAllCoroutines();
                _textComp.text = _dialLines[_index];
            }
        }
    }

    void StartDialogue() {
        _index = 0;
        StartCoroutine(TypeLines());
    }

    IEnumerator TypeLines() {
        foreach (char c in _dialLines[_index].ToCharArray()) {
            _textComp.text += c;
            yield return new WaitForSeconds(typingSpeed);
        }
    }

    void NextLine() {
        if (_index < _dialLines.Length -1 ) {
            _index++;
            _textComp.tag = "";
            StartCoroutine(TypeLines());
        } else {
            gameObject.SetActive(false);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Word : MonoBehaviour
{
    private string _word;
    private Text _textCom;

    public void SetInitialWord(string line)
    {
        _word = line;
        Text text = GetComponentInChildren<Text>();
        if(text)
        {
            text.text = "";
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        Text _textCom = GetComponentInChildren<Text>();
        if (_textCom)
        {
            _textCom.text = "";
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

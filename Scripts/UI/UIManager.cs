using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager
{
    public const int MESSAGE_MAX_COUNT = 100;
    private GameObject m_WordTemplate;
    private GameManager m_GameManager;
    private GameObject m_Canvas;

    public void Initialize(GameManager gameManager)
    {
        m_GameManager = gameManager;
        m_WordTemplate = SaveLoadManager.GetInstance().LoadWordTemplate();
        if (m_WordTemplate == null)
        {
            throw new System.Exception("Cannot load word template,wrong path?");
        }

        m_Canvas = GameObject.Find("Canvas");
        if(m_Canvas == null)
        {
            throw new System.Exception("Cannot find canvas");
        }
    }

    public bool PopoutNewMessage(GameObject speaker,string words)
    {
        GameObject word = GameObject.Instantiate(m_WordTemplate, m_Canvas.transform);
        Text text = word.GetComponentInChildren<Text>();
        if(text)
        {
            text.text = words;
        }

        Vector3 screenPos = Camera.main.WorldToScreenPoint(speaker.transform.position);

        word.transform.position = screenPos;
        return true;
    }
}

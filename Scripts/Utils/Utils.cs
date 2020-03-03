using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//helper类，用于获得Scene内的Manager
public class Utils
{
    public static GameManager GetGameManager()
    {
        GameObject goGM = GameObject.FindGameObjectWithTag("GameManager");
        if(goGM)
        {
            GameManager gmCom = goGM.GetComponent<GameManager>();
            if(gmCom)
            {
                return gmCom;
            }
        }

        throw new System.Exception("Cannot find GameManager Entity");
    }

    public static InputManager GetInputManager()
    {
        GameObject goGM = GameObject.FindGameObjectWithTag("GameManager");
        if (goGM)
        {
            InputManager inputCom = goGM.GetComponent<InputManager>();
            if (inputCom)
            {
                return inputCom;
            }
        }

        throw new System.Exception("Cannot find GameManager Entity");
    }

    public static EventManager GetEventManager()
    {
        GameObject goGM = GameObject.FindGameObjectWithTag("GameManager");
        if (goGM)
        {
            EventManager eventManager = goGM.GetComponent<EventManager>();
            if (eventManager)
            {
                return eventManager;
            }
        }

        throw new System.Exception("Cannot find GameManager Entity");
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class LineRequestData
{

}

public class LineKey
{
    public int _hash;
    public string _belongsTo;
}


//TODO: implement LineManager.
//Need to pop out correct line based on request type(character name,status,etc.)
public class LineManager
{
    private Dictionary<LineKey, string> _lineLibrary = new Dictionary<LineKey, string>();

    public void Initialize()
    {

    }

    public string RequestLine(LineRequestData data)
    {
        return "";
    }


}

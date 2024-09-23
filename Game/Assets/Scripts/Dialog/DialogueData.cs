using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable] public struct Dialogue{

    public string name1;
    public Sprite character1;
    public string name2;
    public Sprite character2;
    public bool LeftTalk;


    [TextArea(5,10)]
    public string text;
}

[CreateAssetMenu(fileName = "DialogueData", menuName = "ScriptableObjects/TalkScript", order = 1)]
public class DialogueData : ScriptableObject {
    public List<Dialogue> talkScript;

}

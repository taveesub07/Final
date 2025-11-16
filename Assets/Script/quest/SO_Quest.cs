using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Quest", menuName = "Quest System/New Quest")]
public class SO_Quest : ScriptableObject
{
    public string questName;
    [TextArea] public string questDescription;

    public Objective[] objectives;

    public int goldReward;
    public int expReward;
}

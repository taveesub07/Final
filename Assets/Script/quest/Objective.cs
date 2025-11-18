using UnityEngine;

public enum ObjectiveType
{
    Kill,
    Collect,
    Talk
}

[System.Serializable]
public class Objective
{
    public ObjectiveType type;
    public string targetID;
    public int requiredAmount = 1;

    [HideInInspector] public int currentAmount = 0;
    [HideInInspector] public bool isCompleted = false;

    public Objective(Objective copy)
    {
        type = copy.type;
        targetID = copy.targetID;
        requiredAmount = copy.requiredAmount;

        currentAmount = 0;
        isCompleted = false;
    }
}

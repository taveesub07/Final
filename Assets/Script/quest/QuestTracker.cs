using UnityEngine;

[System.Serializable]
public class QuestTracker
{
    public SO_Quest trackedQuest;
    public string questName;
    public string questDescription;
    public Objective[] objectives;
    public bool questCanComplete;

    public QuestTracker(SO_Quest quest)
    {
        trackedQuest = quest;

        if (quest == null) return;

        questName = quest.questName;
        questDescription = quest.questDescription;

        objectives = new Objective[quest.objectives.Length];
        for (int i = 0; i < objectives.Length; i++)
            objectives[i] = new Objective(quest.objectives[i]);
    }

    public void UpdateProgress(ObjectiveType type, string targetID)
    {
        foreach (Objective obj in objectives)
        {
            if (obj.type == type && obj.targetID == targetID)
            {
                obj.currentAmount++;

                if (obj.currentAmount >= obj.requiredAmount)
                    obj.isCompleted = true;
            }
        }

        CheckCompleted();
    }

    private void CheckCompleted()
    {
        foreach (Objective obj in objectives)
        {
            if (!obj.isCompleted)
            {
                questCanComplete = false;
                return;
            }
        }

        questCanComplete = true;
    }
}

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

        // Clone objectives
        objectives = new Objective[quest.objectives.Length];
        for (int i = 0; i < objectives.Length; i++)
            objectives[i] = new Objective(quest.objectives[i]);
    }

    /// <summary>
    /// อัปเดต progress ของ objective
    /// return true ถ้ามีการเปลี่ยนค่า (ต้องอัปเดต UI)
    /// </summary>
    public bool UpdateProgress(ObjectiveType type, string targetID)
    {
        Debug.Log("[Tracker] UpdateProgress called → type=" + type + " id=" + targetID);

        bool changed = false;

        foreach (Objective obj in objectives)
        {
            Debug.Log("[Tracker] Checking Objective → type=" + obj.type + " target=" + obj.targetID);

            if (obj.type == type && obj.targetID == targetID)
            {
                Debug.Log("[Tracker] OBJECTIVE MATCH → current=" + obj.currentAmount);

                if (obj.currentAmount < obj.requiredAmount)
                {
                    obj.currentAmount++;
                    Debug.Log("[Tracker] ++ Amount = " + obj.currentAmount);

                    changed = true;

                    if (obj.currentAmount >= obj.requiredAmount)
                    {
                        obj.isCompleted = true;
                        Debug.Log("[Tracker] Objective Completed!");
                    }
                }
            }
        }

        CheckCompleted();
        return changed;
    }


    /// <summary>
    /// เช็คว่าเควสทำครบทุก objective หรือยัง
    /// </summary>
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

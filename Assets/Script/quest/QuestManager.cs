using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public static QuestManager instance;

    public QuestTracker[] ongoingQuest;
    public QuestUI[] questUI;

    private void Awake()
    {
        if (instance == null) instance = this;
        else { Destroy(gameObject); return; }

        ongoingQuest = new QuestTracker[questUI.Length];
        for (int i = 0; i < questUI.Length; i++)
            questUI[i].ClearValue();
    }

    public void AcceptQuest(SO_Quest quest)
    {
        if (quest == null)
        {
            Debug.LogWarning("Quest is null!");
            return;
        }

        // ตรวจสอบว่าเควสนี้ยังไม่ถูกเพิ่ม หรือถูกทำเสร็จแล้ว
        for (int i = 0; i < ongoingQuest.Length; i++)
        {
            if (ongoingQuest[i] != null && ongoingQuest[i].trackedQuest == quest)
            {
                if (!ongoingQuest[i].questCanComplete)
                {
                    Debug.Log("Cannot accept quest again until it's completed: " + quest.questName);
                    return; // เควสยังไม่เสร็จ → ไม่รับซ้ำ
                }
            }
        }

        // เพิ่มเควสใหม่ใน slot ว่าง
        for (int i = 0; i < ongoingQuest.Length; i++)
        {
            if (ongoingQuest[i] == null)
            {
                ongoingQuest[i] = new QuestTracker(quest);

                if (questUI.Length > i && questUI[i] != null)
                    questUI[i].SetValue(ongoingQuest[i], i);
                else
                    Debug.LogWarning("QuestUI not assigned for index " + i);

                Debug.Log("Accepted quest: " + quest.questName);
                return;
            }
        }

        Debug.LogWarning("No available quest slot!");
    }



    public void UpdateQuestProgress(ObjectiveType type, string targetID)
    {
        for (int i = 0; i < ongoingQuest.Length; i++)
        {
            if (ongoingQuest[i] != null)
            {
                ongoingQuest[i].UpdateProgress(type, targetID);
                questUI[i].UpdateProgress(ongoingQuest[i]);
            }
        }
    }

    public void CompleteQuest(int index)
    {
        ongoingQuest[index] = null;
        questUI[index].ClearValue();
    }

    public void CancelQuest(int index)
    {
        ongoingQuest[index] = null;
        questUI[index].ClearValue();
    }
}

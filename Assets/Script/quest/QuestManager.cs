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

        // เคลียร์ UI ทุกช่องให้ตรงกับ ongoingQuest
        for (int i = 0; i < questUI.Length; i++)
        {
            questUI[i].ClearValue();
        }

        DebugAllSlots("Awake");
    }

    // เช็คซ้ำ
    public bool IsQuestAlreadyAccepted(SO_Quest quest)
    {
        foreach (var q in ongoingQuest)
        {
            if (q != null && q.trackedQuest == quest)
                return true;
        }
        return false;
    }


    // รับเควสตามลำดับช่องว่าง
    public void AcceptQuest(SO_Quest quest)
    {
        if (quest == null)
        {
            Debug.LogWarning("Quest is null!");
            return;
        }

        if (IsQuestAlreadyAccepted(quest))
        {
            Debug.Log("Quest already accepted: " + quest.questName);
            return;
        }

        int firstEmptyIndex = -1;

        // ค้นหาช่องว่างที่แท้จริง
        for (int i = 0; i < ongoingQuest.Length; i++)
        {
            if (ongoingQuest[i] == null)
            {
                firstEmptyIndex = i;
                break;
            }
        }

        if (firstEmptyIndex == -1)
        {
            Debug.LogWarning("No available quest slot!");
            return;
        }

        // รับเควส
        ongoingQuest[firstEmptyIndex] = new QuestTracker(quest);

        // อัปเดต UI ให้ตรงช่อง
        questUI[firstEmptyIndex].SetValue(ongoingQuest[firstEmptyIndex], firstEmptyIndex);

        Debug.Log($"Accepted quest: {quest.questName} → Slot {firstEmptyIndex}");
        DebugAllSlots("After Accept");
    }



    // อัพเดตความคืบหน้าเควส
    public void UpdateQuestProgress(ObjectiveType type, string targetID)
    {
        for (int i = 0; i < ongoingQuest.Length; i++)
        {
            if (ongoingQuest[i] == null) continue;

            bool changed = ongoingQuest[i].UpdateProgress(type, targetID);

            if (changed)
            {
                questUI[i].UpdateProgress(ongoingQuest[i]);
            }
        }
    }


    // เควสเสร็จ
    public void CompleteQuest(int index)
    {
        if (ongoingQuest[index] == null) return;

        Debug.Log("Quest Completed: " + ongoingQuest[index].questName);

        ongoingQuest[index] = null;
        questUI[index].ClearValue();

        DebugAllSlots("After Complete");
    }


    public void CancelQuest(int index)
    {
        ongoingQuest[index] = null;
        questUI[index].ClearValue();

        DebugAllSlots("After Cancel");
    }


    // --------------------------
    // Debug Helper
    // --------------------------
    private void DebugAllSlots(string header)
    {
        string msg = "=== Quest Slots (" + header + ") ===\n";
        for (int i = 0; i < ongoingQuest.Length; i++)
        {
            msg += $"Slot {i} : "
                   + (ongoingQuest[i] != null ? ongoingQuest[i].questName : "EMPTY")
                   + "\n";
        }
        Debug.Log(msg);
    }
}

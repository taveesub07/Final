using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class QuestUI : MonoBehaviour
{
    public int index;

    public TextMeshProUGUI title;
    public TextMeshProUGUI description;
    public TextMeshProUGUI objectiveList;
    public Button completeButton;

    private QuestTracker currentTracker;

    public void SetValue(QuestTracker tracker, int idx)
    {
        index = idx;
        currentTracker = tracker;
        gameObject.SetActive(true);

        if (title != null) title.text = tracker.questName;
        if (description != null) description.text = tracker.questDescription;

        UpdateObjectiveList(tracker);

        // ปุ่ม complete อัปเดตอนุญาต
        if (completeButton != null) completeButton.interactable = tracker.questCanComplete;

        // ---------------------------
        // 🟢 สำคัญสุด: bind ปุ่มใหม่ทุกครั้ง
        // ---------------------------
        if (completeButton != null)
        {
            completeButton.onClick.RemoveAllListeners();
            completeButton.onClick.AddListener(() =>
            {
                Debug.Log("[UI] CompleteButton clicked on index = " + index);
                CompleteQuest();
            });
        }
    }

    public void UpdateProgress(QuestTracker tracker)
    {
        currentTracker = tracker;

        Debug.Log("[UI] Updating UI for quest: " + tracker.questName);

        UpdateObjectiveList(tracker);

        if (completeButton != null)
            completeButton.interactable = tracker.questCanComplete;
    }

    private void UpdateObjectiveList(QuestTracker tracker)
    {
        if (objectiveList == null)
        {
            Debug.LogWarning("objectiveList is not assigned in QuestUI!");
            return;
        }

        objectiveList.text = "";

        if (tracker.objectives != null)
        {
            foreach (Objective obj in tracker.objectives)
            {
                string line = "";

                switch (obj.type)
                {
                    case ObjectiveType.Kill:
                        line = $"ฆ่า {obj.targetID} : {obj.currentAmount}/{obj.requiredAmount}";
                        break;

                    case ObjectiveType.Collect:
                        line = $"เก็บ {obj.targetID} : {obj.currentAmount}/{obj.requiredAmount}";
                        break;

                    case ObjectiveType.Talk:
                        line = $"คุยกับ {obj.targetID} : {(obj.isCompleted ? "เสร็จแล้ว" : "ยังไม่เสร็จ")}";
                        break;
                }

                objectiveList.text += line + "\n";
            }
        }
    }

    public void CompleteQuest()
    {
        if (QuestManager.instance == null) return;

        QuestTracker tracker = QuestManager.instance.ongoingQuest[index];

        if (tracker != null && tracker.questCanComplete)
        {
            QuestManager.instance.CompleteQuest(index);
        }
        else
        {
            Debug.Log("[UI] Cannot complete quest yet!");
        }
    }

    public void CancelQuest()
    {
        if (QuestManager.instance != null)
            QuestManager.instance.CancelQuest(index);
    }

    public void ClearValue()
    {
        currentTracker = null;
        gameObject.SetActive(false);

        if (title != null) title.text = "";
        if (description != null) description.text = "";
        if (objectiveList != null) objectiveList.text = "";

        if (completeButton != null)
        {
            completeButton.interactable = false;
            completeButton.onClick.RemoveAllListeners();   // 🟢 กันฟังก์ชันค้าง
        }
    }
}

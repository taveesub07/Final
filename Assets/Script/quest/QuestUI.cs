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

    public void SetValue(QuestTracker tracker, int idx)
    {
        index = idx;
        gameObject.SetActive(true);

        if (title != null) title.text = tracker.questName;
        if (description != null) description.text = tracker.questDescription;

        UpdateObjectiveList(tracker);

        if (completeButton != null) completeButton.interactable = tracker.questCanComplete;
    }

    public void UpdateProgress(QuestTracker tracker)
    {
        UpdateObjectiveList(tracker);

        if (completeButton != null) completeButton.interactable = tracker.questCanComplete;
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
                objectiveList.text += $"{obj.targetID} : {obj.currentAmount}/{obj.requiredAmount}\n";
            }
        }
    }

    public void CompleteQuest()
    {
        // ตรวจสอบว่า quest สามารถทำเสร็จได้
        QuestTracker tracker = QuestManager.instance.ongoingQuest[index];
        if (tracker != null && tracker.questCanComplete)
        {
            QuestManager.instance.CompleteQuest(index);
        }
        else
        {
            Debug.Log("Cannot complete quest yet!");
        }
    }


    public void CancelQuest()
    {
        if (QuestManager.instance != null)
            QuestManager.instance.CancelQuest(index);
    }

    public void ClearValue()
    {
        gameObject.SetActive(false);

        if (title != null) title.text = "";
        if (description != null) description.text = "";
        if (objectiveList != null) objectiveList.text = "";
        if (completeButton != null) completeButton.interactable = false;
    }
}

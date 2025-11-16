using UnityEngine;

public class ItemGO : MonoBehaviour
{
    public string itemID;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (QuestManager.instance != null)
            {
                QuestManager.instance.UpdateQuestProgress(ObjectiveType.Collect, itemID);
            }
            else
            {
                Debug.LogWarning("QuestManager.instance is null — make sure a QuestManager exists in the scene.");
            }

            gameObject.SetActive(false);
        }
    }
}

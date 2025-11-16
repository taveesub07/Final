using UnityEngine;

public class QuestGiver : MonoBehaviour
{
    public SO_Quest quest;

    private bool playerInRange = false;

    private void Update()
    {
        // ถ้า player อยู่ใน range และกด F
        if (playerInRange && Input.GetKeyDown(KeyCode.F))
        {
            if (QuestManager.instance != null)
            {
                QuestManager.instance.AcceptQuest(quest);
            }
            else
            {
                Debug.LogWarning("QuestManager instance not found in scene!");
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
        }
    }
}

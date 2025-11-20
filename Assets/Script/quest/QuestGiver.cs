using UnityEngine;

public class QuestGiver : MonoBehaviour
{
    public SO_Quest quest;

    private bool playerInRange = false;

    private void Update()
    {
<<<<<<< Updated upstream
        // ถ้า player อยู่ใน range และกด F
        if (playerInRange && Input.GetKeyDown(KeyCode.F))
        {
            if (QuestManager.instance != null)
=======
        base.SetUP();

        if (WordTextUI != null)
            WordTextUI.gameObject.SetActive(false);
    }


    public void Interact(Player player)
    {
        if (!canPress) return;
        StartCoroutine(PressCooldown());

        int questIndex = QuestManager.instance.GetQuestIndex(quest);

        // ================================
        // ⭐ มีเควสต์นี้อยู่แล้ว?
        // ================================
        if (questIndex != -1)
        {
            var tracker = QuestManager.instance.ongoingQuest[questIndex];

            // เควสต์โดนลบเพราะส่งไปแล้ว → เริ่มบทสนทนาใหม่
            if (tracker == null)
            {
                dialogueFinished = false;
                dialogueIndex = 0;
                ShowDialogue(dialogues[0]);
                dialogueIndex = 1;
                return;
            }

            // ⭐ เควสต์เสร็จ → เล่นบทส่งเควสต์
            if (tracker.questCanComplete)
>>>>>>> Stashed changes
            {
                QuestManager.instance.AcceptQuest(quest);
            }
<<<<<<< Updated upstream
            else
=======

            // ⭐ ยังทำไม่เสร็จ
            ShowDialogue("Please finish the task first!!");
            return;
        }

        // ================================
        // ⭐ ยังไม่รับเควสต์ → บทสนทนารับเควสต์
        // ================================
        if (!dialogueFinished)
        {
            ShowDialogue(dialogues[dialogueIndex]);
            dialogueIndex++;

            if (dialogueIndex >= dialogues.Length)
>>>>>>> Stashed changes
            {
                Debug.LogWarning("QuestManager instance not found in scene!");
            }
        }
<<<<<<< Updated upstream
=======

        WordTextUI.gameObject.SetActive(false);
>>>>>>> Stashed changes
    }

    private void OnTriggerEnter(Collider other)
    {
<<<<<<< Updated upstream
        if (other.CompareTag("Player"))
=======
        // ⭐ เล่นบทสนทนาตอนส่งเควสต์
        for (int i = 0; i < completeDialogues.Length; i++)
>>>>>>> Stashed changes
        {
            playerInRange = true;
        }
<<<<<<< Updated upstream
=======

        WordTextUI.gameObject.SetActive(false);

        // ⭐ ส่งเควสต์
        QuestManager.instance.CompleteQuest(questIndex);
        Debug.Log("ส่งเควสต์สำเร็จแล้ว!");

        // ⭐⭐ RESET เพื่อให้รับรอบใหม่ ⭐⭐
        dialogueIndex = 0;
        dialogueFinished = false;

        // เอาเควสต์ออกจาก ongoing เพื่อให้ Interact ตรวจดูว่า "ยังไม่มีเควสต์"
        QuestManager.instance.ongoingQuest[questIndex] = null;

        // NPC พร้อมให้รับอีกครั้ง
        isLock = true;
>>>>>>> Stashed changes
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
        }
    }
<<<<<<< Updated upstream
=======


    void AutoGiveQuest()
    {
        WordTextUI.gameObject.SetActive(false);

        if (QuestManager.instance != null && quest != null)
        {
            QuestManager.instance.AcceptQuest(quest);
            Debug.Log("ให้เควสต์: " + quest.questName);
        }

        FindObjectOfType<AllyController>()?.UnlockAlly();

        // อันนี้ต้องเป็น true เพื่อ "ให้คุยได้ต่อ"
        isLock = true;
    }
>>>>>>> Stashed changes
}

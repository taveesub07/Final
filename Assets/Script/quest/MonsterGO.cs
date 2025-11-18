using UnityEngine;

public class MonsterGO : MonoBehaviour
{
    public string monsterID = "Slime";
    public int maxHealth = 1;
    private int currentHealth;

    private bool questSent = false;  // ป้องกันส่งเควสซ้ำ

    private void Awake()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        Debug.Log("[Monster] TakeDamage → HP = " + currentHealth);

        if (currentHealth <= 0)
        {
            Debug.Log("[Monster] HP <= 0 → เตรียมตาย");
            // ไม่ Destroy ตรงนี้ ให้ Enemy ทำลายแทน
        }
    }

    private void OnDestroy()
    {
        // ป้องกันกรณี Destroy ตอนออกเกม/โหลด scene
        if (!gameObject.scene.isLoaded) return;

        if (!questSent)
        {
            Debug.Log("[Monster] OnDestroy() → ส่งเควส update: " + monsterID);

            if (QuestManager.instance != null)
                QuestManager.instance.UpdateQuestProgress(ObjectiveType.Kill, monsterID);

            questSent = true;
        }
    }
}

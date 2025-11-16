using UnityEngine;

public class MonsterGO : MonoBehaviour
{
    public string monsterID;
    public int maxHealth = 1; // จำนวน HP ของมอนสเตอร์
    private int currentHealth;

    private void Awake()
    {
        currentHealth = maxHealth;
    }

    // ฟังก์ชันให้ Player เรียกเพื่อทำ Damage
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        // อัพเดต quest progress เมื่อมอนถูกฆ่า
        if (QuestManager.instance != null)
        {
            QuestManager.instance.UpdateQuestProgress(ObjectiveType.Kill, monsterID);
        }

        // ทำลาย GameObject มอนสเตอร์
        Destroy(gameObject);
    }
}

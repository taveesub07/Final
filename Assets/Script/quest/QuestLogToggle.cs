using UnityEngine;

public class QuestLogToggle : MonoBehaviour
{
    [Header("Panel that contains all QuestUI elements")]
    public GameObject questPanel; // ใส่ Panel หลักที่มี QuestUI ทั้งหมด

    private bool isOpen = false; // เริ่มต้นให้ UI หายไป

    private void Start()
    {
        if (questPanel != null)
            questPanel.SetActive(isOpen); // เริ่มเกม UI หายไป
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q)) // กด Q เพื่อสลับเปิด/ปิด
        {
            ToggleQuestUI();
        }
    }

    public void ToggleQuestUI()
    {
        if (questPanel == null) return;

        isOpen = !isOpen; // สลับสถานะ
        questPanel.SetActive(isOpen);

        if (isOpen) Debug.Log("Quest UI Opened");
        else Debug.Log("Quest UI Closed");
    }
}

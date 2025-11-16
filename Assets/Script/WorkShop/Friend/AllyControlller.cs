using System.Collections.Generic;
using UnityEngine;


public class AllyController : MonoBehaviour
{
    public Ally ally;
    private AllyCommand currentCommand = AllyCommand.None;

    void Update()
    {
        if (ally == null) return;

        if (Input.GetKeyDown(KeyCode.F))
        {
            currentCommand = AllyCommand.Follow;
            //ally.FollowPlayer();
            Debug.Log("Follow Me!");
        }


        if (Input.GetKeyDown(KeyCode.G))
        {
            currentCommand = AllyCommand.Attack;
            // ally.HandleEnemy();
            Debug.Log("Attack!!");
        }

        if (Input.GetKeyDown(KeyCode.H))
        {
            currentCommand = AllyCommand.CollectItem;
            //ally.TryCollectNearbyItem();
            Debug.Log("Secrch for items");
        }

        switch (currentCommand)
        {
            case AllyCommand.Follow:
                ally.FollowPlayer();
                break;
            case AllyCommand.Attack:
                ally.HandleEnemy();
                break;
            case AllyCommand.CollectItem:
                ally.TryCollectNearbyItem();
                break;
            case AllyCommand.None:
                // ทำอะไรเมื่อไม่มีคำสั่งก็ได้ หรือว่างไว้
                break;
        }
    }
}


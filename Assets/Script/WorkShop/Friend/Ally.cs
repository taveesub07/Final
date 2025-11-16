using UnityEngine;
using UnityEngine.AI;

public enum AllyCommand
{
    None,
    Follow,
    Attack,
    Wait,
    CollectItem
}
[RequireComponent(typeof(NavMeshAgent))]
public class Ally : Character
{
    public Transform _player;
    public Player player;
    public float followDistance = 3f;
    public float attackRange = 2f;
    public float detectRange = 10f;
    public int bondPoints = 0;
    public event System.Action<Ally> OnBondChanged;

    private Item itemTarget;
    private NavMeshAgent agent;
    private Character currentEnemy;
    private AllyCommand currentCommand = AllyCommand.Follow;

    void Start()
    {
        SetUP();
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        agent.speed = movementSpeed;
    }

    void Update()
    {

    }


    //    if (player == null || _player == null) return;

    //    // 1. ตรวจว่ามีศัตรูรึเปล่า
    //    if (currentEnemy != null)
    //    {
    //        HandleEnemy(); // ไล่และโจมตีศัตรู
    //    }
    //    else
    //    {
    //        // 2. ถ้าไม่มีศัตรู ? กลับไปตาม Player
    //        FollowPlayer();

    //        // 3. ตรวจหาศัตรูใหม่รอบตัว
    //        DetectEnemy();
    //    }

    //    // 4. ถ้าอยู่ในโหมดเก็บของ
    //    if (currentCommand == AllyCommand.CollectItem)
    //        TryCollectNearbyItem();

    //    //if (player == null) return;
    //    //HandleEnemy();
    //    //FollowPlayer();

    //    //if (currentCommand == AllyCommand.CollectItem)
    //    //    TryCollectNearbyItem();


    public void HandleEnemy()
    {
        if (currentEnemy == null) DetectEnemy();
        if (currentEnemy != null)
        {
            float dist = Vector3.Distance(transform.position, currentEnemy.transform.position);
            if (dist <= attackRange)
            {
                agent.isStopped = true;
                animator.SetTrigger("Trigger");
                currentEnemy.TakeDamage(Damage);
            }
            else if (dist <= detectRange)
            {
                agent.isStopped = false;
                agent.SetDestination(currentEnemy.transform.position);
            }
            else currentEnemy = null;
        }

    }

    public void FollowPlayer()
    {
        if (_player == null) return;
        float distance = Vector3.Distance(transform.position, _player.transform.position);
        if (distance > followDistance)
        {
            agent.isStopped = false;
            agent.SetDestination(_player.transform.position);
            
            animator.SetFloat("Speed", agent.velocity.magnitude);

        }
        else
        {
            agent.isStopped = true;

            animator.SetFloat("Speed", 0);
        }
    }

    private void DetectEnemy()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, detectRange);
        foreach (var hit in hits)
        {
            Character enemy = hit.GetComponent<Character>();
            if (enemy != null && enemy != this && enemy != player)
            {
                currentEnemy = enemy;
                break;
            }
        }
    }

    public void TryCollectNearbyItem()
    {
        if (itemTarget == null)
        {
            // หา item ใกล้สุด
            Collider[] hits = Physics.OverlapSphere(transform.position, 10f);
            float minDist = Mathf.Infinity;

            foreach (var hit in hits)
            {
                Item item = hit.GetComponent<Item>();
                if (item != null)
                {
                    float dist = Vector3.Distance(transform.position, item.transform.position);
                    if (dist < minDist)
                    {
                        minDist = dist;
                        itemTarget = item;
                    }
                }
            }

            if (itemTarget == null)
            {
                currentCommand = AllyCommand.Follow;
                return;
            }

            agent.isStopped = false;
            agent.SetDestination(itemTarget.transform.position);
            animator.SetBool("Moving", true);

            Debug.Log("Moving to item: " + itemTarget.name);
            return;
        }

        // ตรวจสอบระยะเก็บ
        float distance = Vector3.Distance(transform.position, itemTarget.transform.position);
        Debug.Log("Distance to item: " + distance);

        if (distance <= 2f) // ระยะเก็บ
        {
            if (itemTarget != null)
            {
                // ? ใช้ OnCollect ของ Item แทน AddItem
                itemTarget.OnCollect(player);

                Destroy(itemTarget.gameObject);
                ModifyBond(5);

                Debug.Log("Collected item: " + itemTarget.name);
            }

            itemTarget = null;
            currentCommand = AllyCommand.Follow;

            agent.isStopped = true;
            animator.SetBool("Moving", false);
        }
        else
        {
            agent.isStopped = false;
            agent.SetDestination(itemTarget.transform.position);
            animator.SetBool("Moving", true);
            animator.SetFloat("Velocity", agent.velocity.magnitude);
        }
    }



    public void SetCommand(AllyCommand cmd) => currentCommand = cmd;
    public void ModifyBond(int delta)
    {
        bondPoints = Mathf.Clamp(bondPoints + delta, 0, 100);
        OnBondChanged?.Invoke(this);
    }

    public void SetTarget(Character enemy)
    {
        currentEnemy = enemy;
        currentCommand = AllyCommand.Attack;
    }
}

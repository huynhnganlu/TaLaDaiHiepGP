using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class FinalBossBehaviour : MonoBehaviour
{
    private Animator animator;
    private EnemyController enemyController;
    private Rigidbody2D rb;
    private float lastDash = 0f, chaseDistance = 0f, chaseSpeed = 0f;
    private bool isDash = false, isTeleport = false;
    private Sequence treeBoss;
    private ParticleSystem ps;

    void Start()
    {
        animator = GetComponent<Animator>();
        enemyController = GetComponent<EnemyController>();
        rb = GetComponent<Rigidbody2D>();
        ps = GetComponentInChildren<ParticleSystem>();

        Leaf chase = new(ChasePlayer);
        Leaf dash = new(Dash);
        Leaf shoot = new(Shoot);
        Leaf punch = new(Punch);
        Leaf shootAround = new(ShootAround);
        Leaf shootRage = new(ShootRage);

        List<Node> punchOrShootAttackList = new()
        {
            punch,
            shoot
        };

        Selector punchOrShoot = new Selector(punchOrShootAttackList);

        List<Node> notRageAttackList = new()
        {
            dash,
            punchOrShoot
        };

        Sequence notRage = new Sequence(notRageAttackList);

        List<Node> rageAttackList = new()
        {
            shootRage,
            shootAround,
        };

        Sequence rage = new Sequence(rageAttackList);

        List<Node> attackTypeList = new()
        {
            notRage,
            rage
        };

        Selector attack = new Selector(attackTypeList);


        List<Node> chaseAttackList = new()
        {
            chase,
            attack
        };

        treeBoss = new Sequence(chaseAttackList);
    }
    public Node.NodeStates ChasePlayer()
    {
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Walk"))
        {
            if (enemyController.currentEnemyHP > enemyController.enemyMaxHP / 2)
            {
                chaseDistance = 7.5f;
                chaseSpeed = 2f;
            }
            else
            {
                chaseDistance = 10f;
                chaseSpeed = 3.5f;
            }
            if (Vector2.Distance(MyCharacterController.Instance.transform.position, transform.position) <= chaseDistance)
                return Node.NodeStates.SUCCESS;
            else
            {
                GetComponent<EnemyController>().LookAtPlayer();
                transform.position = Vector2.MoveTowards(transform.position, MyCharacterController.Instance.transform.position, chaseSpeed * Time.deltaTime);
                return Node.NodeStates.FAILURE;
            }
        }
        return Node.NodeStates.RUNNING;
    }
    public Node.NodeStates Dash()
    {
        if (enemyController.currentEnemyHP > enemyController.enemyMaxHP / 2)
        {
            if (Time.time - lastDash > 5f && !isDash)
            {
                StartCoroutine(DashSkill(2.5f));
                animator.speed = 1f;
                return Node.NodeStates.RUNNING;
            }
            else if (isDash)
            {
                return Node.NodeStates.RUNNING;
            }else if(Time.time - lastDash <= 5f && !isDash)
            {
                GetComponent<EnemyController>().LookAtPlayer();
                transform.position = Vector2.MoveTowards(transform.position, MyCharacterController.Instance.transform.position, chaseSpeed * Time.deltaTime);
            }
            return Node.NodeStates.SUCCESS;
        }
        return Node.NodeStates.FAILURE;
    }
    public Node.NodeStates Punch()
    {
        if (Vector2.Distance(MyCharacterController.Instance.transform.position, transform.position) <= 5f && !isDash)
        {
            animator.SetTrigger("Attack1");
            return Node.NodeStates.SUCCESS;
        }
        return Node.NodeStates.FAILURE;
    }
    public Node.NodeStates Shoot()
    {
        if (Vector2.Distance(MyCharacterController.Instance.transform.position, transform.position) < 6.5f && Vector2.Distance(MyCharacterController.Instance.transform.position, transform.position) > 5f && !isDash)
            animator.SetTrigger("Attack2");
        return Node.NodeStates.SUCCESS;
    }
    public Node.NodeStates ShootRage()
    {
        animator.SetTrigger("Attack2");
        return Node.NodeStates.SUCCESS;
    }
    public IEnumerator DashSkill(float stoppingDistance)
    {
        isDash = true;
        ps.Play();
        animator.speed = 0f;
        float dashDuration = 1f;

        Vector3 direction = (MyCharacterController.Instance.transform.position - transform.position).normalized;
        Vector3 startingPosition = transform.position;
        Vector3 targetPosition = MyCharacterController.Instance.transform.position - direction * stoppingDistance;

        float elapsedTime = 0f;
        while (elapsedTime < dashDuration)
        {
            transform.position = Vector3.Lerp(startingPosition, targetPosition, elapsedTime / dashDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.position = targetPosition;
        lastDash = Time.time;
        isDash = false;

    }
    public Node.NodeStates ShootAround()
    {
        if (!isTeleport)
        {
            animator.ResetTrigger("Attack2");
            Vector3 currentPos = transform.position;
            isTeleport = true;
            StartCoroutine(TeleportAround(currentPos));
        }
        return Node.NodeStates.SUCCESS;
    }
    public IEnumerator TeleportAround(Vector3 currentPos)
    {
        Teleport(Vector3.up);
        animator.SetTrigger("Attack2");
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);
        Teleport(Vector3.left);
        animator.SetTrigger("Attack2");
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);
        Teleport(Vector3.down);
        animator.SetTrigger("Attack2");
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);
        Teleport(Vector3.right);
        animator.SetTrigger("Attack2");
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);

        transform.position = currentPos;
        yield return new WaitForSeconds(10f);
        isTeleport = false;
    }
    private void Teleport(Vector3 direction)
    {
        transform.position = MyCharacterController.Instance.transform.position + direction * 5f;
    }
    private void Update()
    {
        treeBoss.Evaluate();
    }
}

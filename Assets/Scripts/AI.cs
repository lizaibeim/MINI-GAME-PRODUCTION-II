using System.Collections;
using System.Collections.Generic;
using System.Security.AccessControl;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Assertions.Must;
using UnityEngine.UI;

public class AI : MonoBehaviour
{
    [Header("Drag in robot prefab here")]
    public NavMeshAgent agent;

    [Header("Drag in player prefab here")]
    public Transform player;

    [Header("Pick ground and player")]
    public LayerMask whatIsGround, whatIsPlayer;

    public float timeLeft = 2;

    Magnetic magnet;

    [Header("Field of View values")]
    public float maxAngle;
    public float maxRadius;
    public float alertRadius;
    private bool isInFov = false;

    //Is this robot patrolling or not patrolling
    [Header("Is this robot patrolling or not?")]
    public bool OnPatroll;

    //Patrolling specific WayPoints
    [Header("Patrolling Waypoints Settings")]
    private Transform target;
    private int wayPointIndex = 0;
    private bool playerSpottet;
    public float IdleAtEachWayPoint;
    public bool WaitingAtWayPoint = false;
    private bool isWaiting = false;


    //Patrolling Randomly
    /*[Header("Patrolling Randomly Settings NOT IN USE!")]
    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;*/

    //Attacking
    [Header("Attacking Settings")]
    public float timeBetweenAttacks;
    public float stunnedDuration;
    public bool stunned;
    bool alreadyAttacked;
    public GameObject projectile;
    public Transform BulletSpawn;
    public float bulletForce = 20f;
    [Tooltip("Determine the bullet spread size")]
    public float accuracy = 1f;
    private bool pickedUpSwarmBot = false;
    [Range(0.5f, 5f)]
    public float throwForceModifier = 1f;
    public float attackRange;
    private bool playerInAttackRange;

    public enum State { Alert, Idle, Patrol, Chase, Attack };
    public State currentState = State.Idle;

    //States
    [Header("AlertRange")]
    public GameObject AlertMark;
    public Transform alertSpawn;
    private bool alert;
    private bool alerted, alertDelayDone;    

    //WayPoints wayPoints;
    [Header("Drag in WayPoint Parent here")]
    public Transform transformParent;
    private Transform[] points;

    //Which bot is this
    [Header("Which bot is this")]
    public bool isSwarmBot;
    public bool isSlowBot;
    public bool isBigBot;

    public delegate void AIevent();
    public AIevent attackEvent;
    public AIevent pickUpSwarmbotEvent;
    public AIevent pickUpMetalEvent;

    [Header("PickUp SwarmBots")]
    private Transform pickedUpObject;
    public Transform swarmBotPrefabHolder;
    private bool throwAble;
    private int alertCount = 0;


    private void Awake()
    {
        player = GameObject.Find("Player").transform;
        agent = GetComponent<NavMeshAgent>();

        points = new Transform[transformParent.childCount];
        for (int i = 0; i < points.Length; i++)
        {
            points[i] = transformParent.GetChild(i);
        }
        currentState = State.Idle;
    }

    private void Start()
    {
        AIManager.allAI.Add(this);

        if (isSwarmBot) AIManager.swarmBots.Add(this);

        magnet = GetComponent<Magnetic>();

        target = points[0];

        playerSpottet = false;
    }

    private void OnDisable()
    {
        if (isSwarmBot) AIManager.swarmBots.Remove(this);

        AIManager.allAI.Remove(this);

        if (AIManager.alertedBots.Contains(this)) AIManager.alertedBots.Remove(this);
    }

    private void Update()
    {
        if (magnet.isAffectedByMagnetism && agent.enabled) DisableNavmeshAgent();
        else if (!magnet.isAffectedByMagnetism && !agent.enabled && throwAble) EnableNavmeshAgent();

        if (!agent.enabled)
        {
            CountDown();
            return;
        }


        //hemm = fovDetection.isInFov;
        isInFov = inFOV(transform, player, maxAngle, maxRadius);

        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);


        StateMachine();

        if (isInFov)
        {
            playerSpottet = true;
        }
    }

    public void StateMachine()
    {
        if (alreadyAttacked)
        {
            agent.isStopped = true;
            return;
        }
        else { agent.isStopped = false; }

        switch (currentState)
        {
            case State.Idle:
                Idle();
                if (OnPatroll)
                {
                    currentState = State.Patrol;
                    break;
                }
                if (playerSpottet || alert)
                    currentState = State.Alert;
                break;
            case State.Patrol:
                if (!WaitingAtWayPoint)
                {
                    PatrollingWayPoints();
                }
                if (WaitingAtWayPoint)
                {
                    if (!isWaiting) StartCoroutine(WayPointWait());
                }
                if (playerSpottet || alert)
                    currentState = State.Alert;
                break;

            case State.Alert:
                if (!alerted)
                {
                    StartCoroutine(Alert());
                }
                if (alertDelayDone)
                    currentState = State.Chase;
                break;

            case State.Chase:
                if (!stunned)
                {
                    if (isSwarmBot || isSlowBot)
                    {
                        ChasePlayer();
                        if (playerSpottet && playerInAttackRange)
                            currentState = State.Attack;
                        break;
                    }
                    else
                    {
                        if (AIManager.swarmBots.Count > 0)
                        {
                            if (Vector3.Distance(transform.position, GetClosestSwarmBot().position) >= 3f && !pickedUpSwarmBot && (Vector3.Distance(transform.position, GetClosestMetalPiece().position) > (Vector3.Distance(transform.position, GetClosestSwarmBot().position))))
                                agent.SetDestination(GetClosestSwarmBot().position);

                            if (Vector3.Distance(transform.position, GetClosestSwarmBot().position) < 3f)
                            {
                                transform.LookAt(GetClosestSwarmBot());
                                pickUpSwarmbotEvent?.Invoke();
                                currentState = State.Attack;
                            }
                        }
                        else
                        {
                            if (Vector3.Distance(transform.position, GetClosestMetalPiece().position) >= 3f && !pickedUpSwarmBot)
                                agent.SetDestination(GetClosestMetalPiece().position);
                            print("hotPotato");
                            if (Vector3.Distance(transform.position, GetClosestMetalPiece().position) < 3f)
                            {
                                transform.LookAt(GetClosestMetalPiece());
                                pickUpMetalEvent?.Invoke();
                                currentState = State.Attack;
                            }
                        }
                    }
                }
                break;

            case State.Attack:
                if (isSwarmBot || isSlowBot)
                {
                    AttackPlayer();
                    if (playerSpottet && !playerInAttackRange)
                        currentState = State.Chase;
                    break;
                }
                else
                {
                    AttackPlayer();

                    break;
                }
            default:
                break;
        }
    }
    private void Idle()
    {
        agent.SetDestination(transform.position);
    }

    public void DisableNavmeshAgent()
    {
        agent.enabled = false;
    }

    public void EnableNavmeshAgent()
    {
        agent.Warp(transform.position);
        agent.enabled = true;
    }

    private void PatrollingWayPoints()
    {
        currentState = State.Patrol;

        agent.SetDestination(target.position);
        if (Vector3.Distance(transform.position, target.position) <= 0.1f)
        {
            GetNextWaypoint();
            transform.LookAt(target);
        }
    }

    private void GetNextWaypoint()
    {
        wayPointIndex++;
        target = points[wayPointIndex];
        if (wayPointIndex == points.Length - 1)
        {
            wayPointIndex = 0 - 1;
        }
        WaitingAtWayPoint = true;
    }

    IEnumerator WayPointWait()
    {
        isWaiting = true;
        agent.SetDestination(transform.position);
        yield return new WaitForSecondsRealtime(IdleAtEachWayPoint);
        WaitingAtWayPoint = false;
        isWaiting = false;
    }

    /*private void PatrollingRandomly()
    {
        if (!walkPointSet) SearchWalkPoint();

        if (walkPointSet)
        {
            agent.SetDestination(walkPoint);
        }

        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        //Walk reached
        if (distanceToWalkPoint.magnitude < 2f)
        {
            walkPointSet = false;
        }
    }

    private void SearchWalkPoint()
    {
        float randomX = Random.Range(-walkPointRange, walkPointRange);
        float randomZ = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround))
        {
            walkPointSet = true;
        }
    }*/

    private void ChasePlayer()
    {
        agent.SetDestination(player.position);
    }

    private void AttackPlayer()
    {
        if(!stunned)
        if (isSwarmBot || isSlowBot)
        {
            transform.LookAt(player);


            if (!alreadyAttacked)
            {
                attackEvent?.Invoke();
                //rb.AddForce(transform.up * 8f, ForceMode.Impulse);

                alreadyAttacked = true;
                Invoke(nameof(ResetAttack), timeBetweenAttacks);
                StartCoroutine(Stunned());
            }
        }
        else
        {
            if (!pickedUpSwarmBot)
            {
                return;
            }

            if (pickedUpSwarmBot)
            {
                agent.SetDestination(player.position);
                if (Vector3.Distance(transform.position, player.position) < attackRange && isInFov)
                {
                    transform.LookAt(player);
                    agent.SetDestination(transform.position);
                    attackEvent?.Invoke();
                    StartCoroutine(Stunned());
                }
            }
        }
    }

    private void ResetAttack()
    {
        alreadyAttacked = false;
    }

    IEnumerator Alert()
    {
        if (alertCount < 1)
        {
            Rigidbody rb = Instantiate(AlertMark, alertSpawn.position, Quaternion.identity).GetComponent<Rigidbody>();
            alerted = true;
            yield return new WaitForSecondsRealtime(0.2f);
            SendAlert();
            yield return new WaitForSecondsRealtime(0.8f);
            alertDelayDone = true;
            alertCount += 1;
        }
        else
        {
            alerted = true;
            yield return new WaitForSecondsRealtime(0.2f);
            SendAlert();
            yield return new WaitForSecondsRealtime(0.8f);
            alertDelayDone = true;
        }
        //Debug.Log("AlertCount: " + alertCount);

    }

    IEnumerator Stunned()
    {
        agent.SetDestination(transform.position);
        stunned = true;
        yield return new WaitForSecondsRealtime(stunnedDuration);
        stunned = false;
    }

    public void SendAlert()
    {
        if (!AIManager.alertedBots.Contains(this))
            AIManager.alertedBots.Add(this);

        Debug.Log("Sending alert");
        Collider[] metals = Physics.OverlapSphere(transform.position, alertRadius, 1 << 10);
        foreach (Collider metal in metals)
        {
            AI robot = metal.transform.parent.GetComponent<AI>();
            if (robot)
            {
                if (!AIManager.alertedBots.Contains(robot))
                    AIManager.alertedBots.Add(robot);
                Debug.Log("Is sent");
                robot.alert = true;
            }
        }
    }

    public Transform GetClosestSwarmBot()
    {
        GameObject[] swarmBots = GameObject.FindGameObjectsWithTag("SwarmBot");
        float minDistance = float.PositiveInfinity;
        GameObject closestSwarmBot = null;
        foreach (GameObject swarmbot in swarmBots)
        {
            float distance = Vector3.Distance(swarmbot.transform.position, transform.position);
            if (distance < minDistance)
            {
                minDistance = distance;
                closestSwarmBot = swarmbot;
            }
        }
        return closestSwarmBot.transform;
    }

    public Transform GetClosestMetalPiece()
    {
        GameObject[] swarmBots = GameObject.FindGameObjectsWithTag("Metal");
        float minDistance = float.PositiveInfinity;
        GameObject closestSwarmBot = null;
        foreach (GameObject swarmbot in swarmBots)
        {
            float distance = Vector3.Distance(swarmbot.transform.position, transform.position);
            Rigidbody swarmBotRB = swarmbot.GetComponent<Rigidbody>();
            if (distance < minDistance && swarmBotRB && swarmBotRB.mass <= 15)
            {
                minDistance = distance;
                closestSwarmBot = swarmbot;
            }
        }
        return closestSwarmBot.transform;
    }

    public void PickUpSwarmBot()
    {
        StartCoroutine(Alert());
       
        if (!GetClosestSwarmBot())
            return;
        pickedUpObject = GetClosestSwarmBot().transform;
        pickedUpObject.GetComponent<NavMeshAgent>().enabled = false;
        pickedUpObject.transform.position = swarmBotPrefabHolder.position;
        pickedUpObject.transform.parent = swarmBotPrefabHolder;
        pickedUpObject.gameObject.layer = LayerMask.NameToLayer("PickedObject");
        pickedUpSwarmBot = true;
    }

    public void PickUpMetalPiece()
    {
        //StartCoroutine(Alert());    
        pickedUpObject = GetClosestMetalPiece().transform;

        if (GetClosestMetalPiece().GetComponent<Rigidbody>() != null)
        {
            Rigidbody SBtempRB = pickedUpObject.GetComponent<Rigidbody>();
            SBtempRB.isKinematic = true;
            SBtempRB.useGravity = false;
        }

        pickedUpObject.transform.position = swarmBotPrefabHolder.position;
        pickedUpObject.transform.parent = swarmBotPrefabHolder;
        pickedUpObject.gameObject.layer = LayerMask.NameToLayer("PickedObject");
        pickedUpSwarmBot = true;
    }

    public void ThrowSwarmBot()
    {
        throwAble = true;
        Rigidbody SBtempRB = pickedUpObject.GetComponent<Rigidbody>();
        pickedUpObject.transform.parent = null;
        SBtempRB.constraints = RigidbodyConstraints.None;
        SBtempRB.isKinematic = false;
        SBtempRB.useGravity = true;

        Vector3 throwDir = (player.position - pickedUpObject.transform.position).normalized;
        float distToPlayer = Vector3.Distance(transform.position, player.position);
        float mass = SBtempRB.mass;
        float throwForce = (mass * 2) * distToPlayer * throwForceModifier;
        SBtempRB.AddForce(throwDir * throwForce, ForceMode.Impulse);
        //SBtempRB.AddForce(pickedUpObject.up * 8);
        //SBtempRB.velocity = pickedUpObject.forward * 8;
        pickedUpObject.gameObject.layer = LayerMask.NameToLayer("Metal");
        pickedUpSwarmBot = false;
        currentState = State.Chase;
    }

    private void OnCollisionEnter(Collision collision)
    {
        return;
    }

    public void ShootBullet()
    {
        Rigidbody rb = Instantiate(projectile, BulletSpawn.position, Quaternion.identity).GetComponent<Rigidbody>();

        Vector3 targetPos = player.position + Random.insideUnitSphere * accuracy;
        rb.AddForce((targetPos - transform.position).normalized * bulletForce, ForceMode.Impulse);
    }

    public void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, maxRadius);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, alertRadius);

        Vector3 fovLine1 = Quaternion.AngleAxis(maxAngle, transform.up) * transform.forward * maxRadius;
        Vector3 fovLine2 = Quaternion.AngleAxis(-maxAngle, transform.up) * transform.forward * maxRadius;
        //Vector3 fovLine3 = Quaternion.AngleAxis(maxAngle, transform.up) * transform.forward * alertRadius;

        Gizmos.color = Color.blue;
        Gizmos.DrawRay(transform.position, fovLine1);
        Gizmos.DrawRay(transform.position, fovLine2);

        if (!isInFov)
            Gizmos.color = Color.red;
        else
            Gizmos.color = Color.green;
        Gizmos.DrawRay(transform.position, (player.position - transform.position).normalized * maxRadius);

        Gizmos.color = Color.black;
        Gizmos.DrawRay(transform.position, transform.forward * maxRadius);
    }

    public bool inFOV(Transform checkingObject, Transform target, float maxAngle, float maxRadius)
    {
        //Collider[] overlaps = new Collider[100];
        //int count = Physics.OverlapSphereNonAlloc(checkingObject.position, maxRadius, overlaps);

        if (Vector3.Distance(target.position, transform.position) < maxRadius)
        {
            Vector3 directionBetween = (target.position - checkingObject.position).normalized;
            directionBetween.y *= 0;

            float angle = Vector3.Angle(checkingObject.forward, directionBetween);

            if (angle <= maxAngle)
            {
                Ray ray = new Ray(checkingObject.position, target.position - checkingObject.position);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit, maxRadius))
                {
                    if (hit.transform == target)
                    {
                        return true;
                    }
                }
            }
        }
        /*
        for (int i = 0; i < count + 1; i++)
        {
            if (overlaps[i] != null)
            {
                if (overlaps[i].transform == target)
                {
                    Vector3 directionBetween = (target.position - checkingObject.position).normalized;
                    directionBetween.y *= 0;

                    float angle = Vector3.Angle(checkingObject.forward, directionBetween);

                    if (angle <= maxAngle)
                    {
                        Ray ray = new Ray(checkingObject.position, target.position - checkingObject.position);
                        RaycastHit hit;

                        if (Physics.Raycast(ray, out hit, maxRadius))
                        {
                            if (hit.transform == target)
                            {
                                return true;
                            }
                        }
                    }
                }
            }
        }*/

        return false;
    }

    public void CountDown()
    {
        if (!agent.enabled)
        {
            timeLeft -= Time.deltaTime;
            if (timeLeft < 0)
            {
                if (gameObject.GetComponent<Rigidbody>())
                    gameObject.GetComponent<Rigidbody>().useGravity = true;
                else
                    gameObject.AddComponent<Rigidbody>();
            }
        }
    }
}
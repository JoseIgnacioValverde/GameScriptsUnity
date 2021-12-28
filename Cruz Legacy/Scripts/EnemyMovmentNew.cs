using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovmentNew : MonoBehaviour
{
    public NavMeshAgent navMeshAgent;
    public Transform player;
    public LayerMask whatIsGround, whatIsPlayer;
    public LayerMask[] obstacles;
    //patroling
    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRangeX;
    public float walkPointRangeZ;
    public float hitpoints=1;
    
    //attacking
    public float timeBetweenAttacks;
    bool alreadyAttacked;
    public Rigidbody arrow, enemyRig;
    public Transform puntaBallesta;
    public float shootingForce=100;
    public EnemyHealthNew health;

    //Vision y rango
    public float sightRange, attackRange, retreatRange;
    public bool playerInSightRange, playerInAttackRange, playerInRetreatRange;

    //animaciones
    public Animator animator;
    private bool isStatic, walking;

    //audio
    public AudioSource shot;
   
    
    private void Awake(){
        player = GameObject.Find("Player").transform;
    }

    void Start ()
    {  
        checkStatic();
        walking=true;
    }
    void fixedUpdate(){
        
    }
    void Update ()
    {
        
        checkDeath();
        if(walking){
        playerInSightRange=Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange=Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);
        playerInRetreatRange=Physics.CheckSphere(transform.position, retreatRange, whatIsPlayer);

        if(!playerInSightRange && !playerInAttackRange /*&&!playerInRetreatRange*/)
            if(!isStatic) Patroling();
        if(playerInSightRange && !playerInAttackRange /*&&!playerInRetreatRange*/) ChasePlayer();
        if(playerInSightRange && playerInAttackRange /*&&!playerInRetreatRange*/) AttackPlayer();
        //if(playerInSightRange && playerInAttackRange &&playerInRetreatRange); Retreat();
        }
        

    }
    void Patroling(){
        
        if(!walkPointSet) SearchWalkPoint();
        if(walkPointSet)
            navMeshAgent.SetDestination(walkPoint);
        Vector3 distanceToWalkPoint = transform.position - walkPoint;
        animator.SetBool("IdleToWalk",true);
        if(distanceToWalkPoint.magnitude < 1f)
            walkPointSet=false;
        
    }
    void SearchWalkPoint(){
        float randomZ = Random.Range(-walkPointRangeZ, walkPointRangeZ);
        float randomX = Random.Range(-walkPointRangeX, walkPointRangeX);

        walkPoint=new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);
        NavMeshPath camino = new NavMeshPath();
        navMeshAgent.CalculatePath (walkPoint, camino);
        if(Physics.Raycast(walkPoint,-transform.up, 2f, whatIsGround)&&!(camino.status==NavMeshPathStatus.PathPartial || camino.status==NavMeshPathStatus.PathInvalid)) walkPointSet=true;
        else walkPointSet=false;
    }
    void ChasePlayer(){
        NavMeshPath camino = new NavMeshPath();
        navMeshAgent.CalculatePath (player.position, camino);
        if(!(camino.status==NavMeshPathStatus.PathPartial || camino.status==NavMeshPathStatus.PathInvalid)){
            animator.SetBool("IdleToWalk",true);
            animator.SetFloat("Speed",1.0f);
            transform.LookAt(player);
            navMeshAgent.SetDestination(player.position);
        }
        else{
            Patroling();
        }
        
    }
    void AttackPlayer(){
       
        NavMeshPath camino = new NavMeshPath();
        navMeshAgent.CalculatePath (player.position, camino);
        if(!(camino.status==NavMeshPathStatus.PathPartial || camino.status==NavMeshPathStatus.PathInvalid)){
            navMeshAgent.SetDestination(transform.position);
            animator.SetBool("IdleToWalk",false);
            animator.SetFloat("Speed",0.0f);
            transform.LookAt(player);
            puntaBallesta.LookAt(player);
            Vector3 direction =(player.position - transform.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
            if(!alreadyAttacked){
                shot.Play();
                Rigidbody arrowInstance = Instantiate(arrow, puntaBallesta.position, lookRotation) as Rigidbody;
                arrowInstance.velocity = shootingForce*puntaBallesta.forward;
                alreadyAttacked=true;
                Invoke(nameof(ResetAttack),timeBetweenAttacks);
            }
        }
        else{
            Patroling();
        }
        
    
    }
    /*void Retreat(){
        float distanceX =Mathf.Abs(player.position.x)- Mathf.Abs(transform.position.x);
        float retreatX=0.0f;
        float distanceZ =Mathf.Abs(player.position.z)- Mathf.Abs(transform.position.z);
        float retreatZ=0.0f;
        if(distanceX<retreatRange){
            if(transform.position.x<0){

            }
        }
        
    }*/
    void ResetAttack(){
        alreadyAttacked=false;
    }
    void checkDeath(){
        if(health.sendDeath()){
            stopMoving();
            animator.SetBool("Dead",true);
            enemyRig.isKinematic=true;
        }
    }
    void OnCollisionEnter(Collision collider){
        if(collider.gameObject.tag=="Bullet"){
            Debug.Log("Bullet");
            if(hitpoints>0){
                hitpoints--;
            }
            
        }
    }
    void checkStatic(){
        if(walkPointRangeX!=0f || walkPointRangeZ!=0){
            isStatic=false;
            animator.SetBool("EnemyStatic",false);
            animator.SetBool("IdleToWalk",true);
        }
        else{
            isStatic=true;
            animator.SetBool("EnemyStatic",true);
            animator.SetBool("IdleToWalk",false);
        }
            

    }
    public void stopMoving(){
        walking=false;
        navMeshAgent.ResetPath();
    }
}

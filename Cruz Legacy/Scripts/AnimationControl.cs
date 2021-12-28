using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationControl : MonoBehaviour
{
    Animator animator;
    EnemyMovement enemyMovement;
    // Start is called before the first frame update
    void Start()
    {
        animator=GetComponent<Animator>();
        enemyMovement=GetComponent<EnemyMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

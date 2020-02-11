using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Enemy : MovingObject

{
    public int enemyDamage;
    public int enemyHP;
    public AudioClip playerHit;
    public AudioClip gameOver;
    public AudioClip gameOverMusic;

    private Animator animator;
    private Transform target;
    private bool skipMove;

    protected override void Start()
    {
        GameManager.instance.AddEnemyToList(this);
        animator = GetComponent<Animator>();
        target = GameObject.FindGameObjectWithTag("Player").transform;
        base.Start();
    }

    protected override void AttemptMove<T>(int xDir, int yDir)
    {
        if (skipMove)
        {
            skipMove = false;
            return;
        }

        base.AttemptMove<T>(xDir, yDir);

        skipMove = true;
    }

    public void MoveEnemy()
    {
        if (this)
        {

            int xDir = 0;
            int yDir = 0;

            if (Mathf.Abs(target.position.x - transform.position.x) < float.Epsilon)
            {
                yDir = target.position.y > transform.position.y ? 1 : -1;
            }
            else
            {
                xDir = target.position.x > transform.position.x ? 1 : -1;
            }

            if (xDir == 1)
                animator.SetTrigger("EnemyRight");
            else if (xDir == -1)
                animator.SetTrigger("EnemyLeft");

            AttemptMove<Player>(xDir, yDir);
        }
    }

    protected override void OnCantMove<T>(T component)
    {

        Player hitPlayer = component as Player;
        hitPlayer.LoseHP(enemyDamage);


        }

	public void CheckDie ()
	{
		if (enemyHP == 0) {
			Destroy (this);
		}
	}
    }






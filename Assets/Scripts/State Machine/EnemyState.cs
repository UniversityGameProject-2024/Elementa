using UnityEngine;
using System.Collections.Generic;
using System.Collections;
public class EnemyState
{
    private string animationName;
    protected Enemy enemy;
    protected StateMachine stateMachine;
    protected Rigidbody2D body;
    protected GameObject gameObject;
    protected bool animTrigger;
    protected float timer;

    public EnemyState(Enemy enemy, StateMachine stateMachine, string animationName, GameObject gameObject)
    {
        this.enemy = enemy;
        this.stateMachine = stateMachine;
        this.animationName = animationName;
        //this.gameObject = gameObject;
    }

    public virtual void Enter()
    {
        Debug.Log("troll enter -" + animationName);

        animTrigger = false;
        enemy.animator.SetBool(animationName, true);

    }

    public virtual void Update()
    {
        Debug.Log("troll in -" + animationName);
        timer -= Time.deltaTime;
    }

    public virtual void Exit()
    {
        Debug.Log("troll exit -" + animationName);
        enemy.animator.SetBool(animationName, false);
    }

    public virtual void IsAnimFinshed()
    {
        animTrigger = true;
    }
}

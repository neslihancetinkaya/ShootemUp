using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    Vector3 motionTarget = new Vector3();
    Vector3 motionDirection = new Vector3();
    public Player player;    
    public float radiusTarget = 100f;
    public float xConstraint = Director.xConstraint;
    public float yConstraint = Director.yConstraint;
    public int counterMoveValue = 0;
    public int targetMoveValue = 2;
    public int speedEnemy = 8;
    public enum State{
        Move,
        Ram,
    }
    public State currentState = State.Move;

    abstract public bool hit(int damage);

    void moveStart(){
        float randX = Random.Range(-xConstraint, xConstraint);
        float randY = Random.Range(-yConstraint, yConstraint);
        motionTarget = new Vector3(randX, randY, 0f);
        motionDirection = (motionTarget - transform.position).normalized;
        counterMoveValue++;
        if(counterMoveValue == targetMoveValue){
            currentState = State.Ram;
        }
    }

    void moveUpdate(){
        if((motionTarget - transform.position).magnitude <= radiusTarget){
            moveStart();
        }
        else{
            transform.position += speedEnemy * motionDirection;
        }
    }

    void ramStart(){

    }

    void ramUpdate(){
        motionTarget = player.transform.position;
        motionDirection = (motionTarget - transform.position).normalized;
        transform.position += speedEnemy*motionDirection;
        if((motionTarget - transform.position).magnitude <= radiusTarget){
            currentState = State.Move;
            counterMoveValue = 0;
        }
    }
    
    // Start is called before the first frame update
    public void enemyStart(Player player)
    {
        this.player = player;
        switch(currentState){
            case State.Move:
            moveStart();
            break;
            case State.Ram:
            ramStart();
            break;
        }
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        switch(currentState){
            case State.Move:
            moveUpdate();
            break;
            case State.Ram:
            ramUpdate();
            break;
        }        
    }
}
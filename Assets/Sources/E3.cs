using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E3 : NewEnemy
{
    bool isE3Collide = false;
    public int E3HealthPoint = 3;
    Wait wait;
    Ram ram;
    Move move;
    Destroyed destroyed;

    public override bool hit(int damage)
    {
        if (!isE3Collide) {
            E3HealthPoint = E3HealthPoint - damage; 
            director.score += scorePayloadWhenHit;
            if(E3HealthPoint <= 0){
                director.score += scorePayloadWhenDestroyed;
                Director.countE3--;
                isE3Collide = true;
                E3HealthPoint = 3;
                stateMachine.currentState = destroyed;
            }
            return true;
        }
        return false;
    }

    public override void enemyStart(Director director, Player player)
    {
        this.director = director;
        this.player = player;    
        destroyed = new Destroyed(this, 720);
        ram = new Ram(this);
        move = new Move(this);
        wait = new Wait(this);

        wait.nextState = move;

        // stateMachine.currentState = move;    
        stateMachine.currentState = wait;    
    }

    protected override void Update()
    {
        base.Update();
        //del
        if(player.isActive){
            if((motionTarget - transform.position).magnitude <= radiusTarget){
               stateMachine.currentState = move;
            }
            if(counterMoveValue == targetMoveValue){
                stateMachine.currentState = ram;
                counterMoveValue = 0;
            }
        }        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E3 : NewEnemy
{
    bool isE3Collide = false;
    public int E3HealthPoint = 3;
    Ram ram;
    Move move;
    Destroyed destroyed;

    public override bool hit(int damage)
    {
        if (!isE3Collide) {
            E3HealthPoint = E3HealthPoint - damage; 
            if(E3HealthPoint <= 0){
                Director.countE3--;
                isE3Collide = true;
                E3HealthPoint = 3;
                stateMachine.currentState = destroyed;
            }
            return true;
        }
        return false;
    }

    public override void enemyStart(Player player)
    {
        this.player = player;    
        destroyed = new Destroyed(this, 720);
        ram = new Ram(this);
        move = new Move(this);

        if(player.isActive){
            stateMachine.currentState = move;        
        }        
    }

    protected override void Update()
    {
        base.Update();
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

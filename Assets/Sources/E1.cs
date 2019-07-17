using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E1 : NewEnemy
{
    bool isE1Collide = false;
    public int E1HealthPoint = 1;
    Wait wait;
    Move move;
    Destroyed destroyed;

    public override bool hit(int damage){
        if (!isE1Collide) {
            E1HealthPoint = E1HealthPoint - damage;
            director.score += scorePayloadWhenHit;
            if(E1HealthPoint <= 0){
                director.score += scorePayloadWhenDestroyed;
                Director.countE1--;
                isE1Collide = true;
                E1HealthPoint = 1;
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
        move = new Move(this);
        wait = new Wait(this);

        wait.nextState = move;

        stateMachine.currentState = wait;
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();        
    }
}
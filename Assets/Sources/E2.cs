using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E2 : NewEnemy
{
    bool isE2Collide = false;
    public int E2HealthPoint = 2;
    Wait wait;
    Ram ram;
    Destroyed destroyed;
   
    public override bool hit(int damage){
        if (!isE2Collide) {
            E2HealthPoint = E2HealthPoint - damage;
            director.score += scorePayloadWhenHit;
            if(E2HealthPoint <= 0){
                director.score += scorePayloadWhenDestroyed;
                Director.countE2--;
                isE2Collide = true;
                E2HealthPoint = 2;
                stateMachine.currentState = destroyed;
            }
            return true;
        }
        return false;
    }

    // Start is called before the first frame update
    public override void enemyStart(Director director, Player player)
    {
        this.director = director;
        this.player = player;    
        destroyed = new Destroyed(this, 720);
        ram = new Ram(this);
        wait = new Wait(this);

        wait.nextState = ram;
        
        stateMachine.currentState = wait; 
    }
    
    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }
}
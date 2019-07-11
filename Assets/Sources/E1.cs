using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E1 : NewEnemy
{
    bool isE1Collide = false;
    public int E1HealthPoint = 1;
    Move move;
    Destroyed destroyed;

    public override bool hit(int damage){
        if (!isE1Collide) {
            E1HealthPoint = E1HealthPoint - damage;
            if(E1HealthPoint <= 0){
                Director.countE1--;
                isE1Collide = true;
                E1HealthPoint = 1;
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
        move = new Move(this);

        if(player.isActive){
            stateMachine.currentState = move;
        }           
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();        
    }
}
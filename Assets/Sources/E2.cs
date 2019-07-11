﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E2 : NewEnemy
{
    bool isE2Collide = false;
    public int E2HealthPoint = 2;
    Ram ram;
    Destroyed destroyed;
   
    public override bool hit(int damage){
        if (!isE2Collide) {
            E2HealthPoint = E2HealthPoint - damage;
            if(E2HealthPoint <= 0){
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
    public override void enemyStart(Player player){
        this.player = player;    
        destroyed = new Destroyed(this, 720);
        ram = new Ram(this);
        
        if(player.isActive){
            stateMachine.currentState = ram; 
        }
    }
    
    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }
}
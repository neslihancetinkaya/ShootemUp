using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static StateMachine;

public partial class Player
{
    protected class DestroyState : PlayerState
    {        
        public float rotationMultiplier = 1; //720
        float tick = 0; 
        public DestroyState(Player player, float rm = 720) : base(player){
            rotationMultiplier = rm;
        }
        public override void enter(State from){
            tick = Time.time;
        } 
        public override void exit(State to){
            player.reset();
        }
        public override State update(){
            player.transform.Rotate(Time.deltaTime * rotationMultiplier, 0, 0);
            float t = Time.time - tick;
            player.transform.localScale = Vector3.Lerp(Vector3.one, Vector3.zero, t);
            if(t >= 1){
                if(player.lifePlayer > 0){
                    return player.ingressState;
                }
                else {
                    player.destroy();
                }
            }
            return null;
        }
    }
}

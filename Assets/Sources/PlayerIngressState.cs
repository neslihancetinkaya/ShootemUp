using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static StateMachine;

public partial class Player
{
    float ingressSpeed = 2;
    protected class IngressState : PlayerState
    {
        float d => player.ingressSpeed;
        Vector3 start;
        Vector3 end;
        float t = 0;
        float newT;
        public IngressState(Player player) : base(player){
        }    
        public override void enter(State from){
            t = Time.time;
            //player.reset();
        } 
        public override void exit(State to){
            player.ingressCompleted();            
        }
        public override State update(){
            newT = Time.time -t;
            Ingress(d);
            return player.transform.position.y >= 0 ? player.pPlayState : null;
        }
        public void Ingress(float duration){
            start = new Vector3(0, -player.yConstraint, 0);
            end = new Vector3(0, 0, 0);
            player.transform.position = Vector3.Lerp(start, end, newT / duration);
        }

    }
}
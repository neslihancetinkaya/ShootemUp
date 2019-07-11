using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static StateMachine;

public partial class Player
{
    protected class IngressState : PlayerState
    {
        float d = 5;
        Vector3 start;
        Vector3 end;

        //bunu playerda ver?

        float t = 0;
        float newT;
        public IngressState(Player player) : base(player){
        }    
        public override void enter(State from){
            t = Time.time;
            State nextState = player.pPlayState;
        } 
        public override void exit(State to){

        }
        public override State update(){
            newT = Time.time -t;
            Ingress(d);            
            return null;
        }
        public void Ingress(float duration){
            start = new Vector3(0, -player.yConstraint, 0);
            end = new Vector3(0, 0, 0);
            player.transform.position = Vector3.Lerp(start, end, newT / duration);
        }

    }
}
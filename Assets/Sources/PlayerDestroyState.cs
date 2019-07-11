using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static StateMachine;

public partial class Player
{
    protected class DestroyState : PlayerState
    {
        public DestroyState(Player player) : base(player){
        }
        public override void enter(State from){

        } 
        public override void exit(State to){

        }
        public override State update(){
            return null;
        }
    }
}

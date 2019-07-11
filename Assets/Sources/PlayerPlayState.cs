using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static StateMachine;

public partial class Player
{
    protected class PPlayState : PlayerState
    {
        
        public PPlayState(Player player) : base(player){

        }
        public override void enter(State from){
            
        }
        public override void exit(State to){

        }
        public override State update(){
            // Right
            if(Input.GetKey(KeyCode.RightArrow) && player.transform.position.x < player.xConstraint){
                player.transform.position = new Vector3(player.transform.position.x + player.speedPlayer * Time.deltaTime, player.transform.position.y, 0);
            }
            Debug.Log("..");
            return null;
        }
    }
}
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
            // Left
            if(Input.GetKey(KeyCode.LeftArrow) && player.transform.position.x > -player.xConstraint){
                player.transform.position = new Vector3(player.transform.position.x - player.speedPlayer*Time.deltaTime, player.transform.position.y, 0f);
            }
            // Up
            if(Input.GetKey(KeyCode.UpArrow) && player.transform.position.y < player.yConstraint){
                player.transform.position = new Vector3(player.transform.position.x, player.transform.position.y + player.speedPlayer*Time.deltaTime, 0f);
            }
            // Down
            if(Input.GetKey(KeyCode.DownArrow) && player.transform.position.y > -player.yConstraint){
                player.transform.position = new Vector3(player.transform.position.x, player.transform.position.y - player.speedPlayer*Time.deltaTime, 0f);
            }
            Debug.Log("..");
            return null;
        }
    }
}
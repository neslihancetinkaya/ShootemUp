using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static StateMachine;

public partial class Player
{    
    void OnCollisionEnter2D(Collision2D collision){
        NewEnemy enemy = collision.gameObject.GetComponent<NewEnemy>();
        if(enemy != null && enemy.hit(3)){
           // return;
            health--;
            if(health <= 0){
                health = 3;
                life--;
                player.stateMachine.currentState = player.destroyState;
            }
            else {
                transform.position = Vector3.zero;
            }
        }
    }
    protected class PPlayState : PlayerState
    {
        void createGreenLaser(){
        Vector3 laserPosition = new Vector3(player.transform.position.x, player.transform.position.y + 20f, 0f);
        GameObject go = Instantiate(player.laserGreenPrefab, laserPosition, Quaternion.identity) as GameObject;
        go.GetComponent<GreenLaser>().laserStart(player); 
        countLaser++;
        }
        void createRedLaser(){
            GameObject go = Instantiate(player.laserRedPrefab, player.transform.position, Quaternion.identity) as GameObject;
            go.GetComponent<RedLaser>().laserStart(player);
            countLaser++;
        }
        
        public PPlayState(Player player) : base(player){
        }

        public override void enter(State from){
            player.fireLaserDelegate = createGreenLaser;
            player.activeStateChanged(true);
            // player.fireLaserDelegate += Osman;
        }   
        // void Osman(){
        //     Debug.Log("osman");
        // }
        public override void exit(State to){
            player.activeStateChanged(false);
            
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

            // Fire
            if(Input.GetKey(KeyCode.Space) && player.tick < Time.time && countLaser < player.maxCountLaser){ 
                player.tick = Time.time + player.delay;
                player.fireLaserDelegate();
            }

            // Fire Green Laser
            if(Input.GetKeyDown(KeyCode.Alpha1)){
                player.fireLaserDelegate = createGreenLaser;
            }

            // Fire Red Laser
            if(Input.GetKeyDown(KeyCode.Alpha2)){
                player.fireLaserDelegate = createRedLaser;
            }     

            return null;
        }
    }
}
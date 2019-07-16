using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static StateMachine;
public abstract class NewEnemy : MonoBehaviour
{
    Vector3 motionDirection = new Vector3();
    protected Vector3 motionTarget = new Vector3();
    public Player player;
    public float radiusTarget = 100f;
    public float xConstraint = Director.xConstraint;
    public float yConstraint = Director.yConstraint;
    public int counterMoveValue = 0;
    public int speedEnemy = 8;
    public int targetMoveValue = 2;
    abstract public bool hit(int damage);

    protected abstract class EnemyState : State
    {
        protected NewEnemy enemy {get; private set;}
        public EnemyState(NewEnemy enemy) {
            this.enemy = enemy;
        }
    }

    protected class Move : EnemyState
    {
        public Move(NewEnemy enemy) : base(enemy) {            
        }

        public override void enter(State from){
            float randX = Random.Range(-enemy.xConstraint, enemy.xConstraint);
            float randY = Random.Range(-enemy.yConstraint, enemy.yConstraint);
            if(enemy.player.isActive){
                enemy.motionTarget = new Vector3(randX, randY, 0);
                enemy.motionDirection = (enemy.motionTarget - enemy.transform.position).normalized;
                enemy.counterMoveValue++;
            }
        }
        public override void exit(State to){

        }
        public override State update(){
            if(enemy.player.isActive){
                if((enemy.motionTarget - enemy.transform.position).magnitude <= enemy.radiusTarget){
                    enter(this);
                }
                else{
                    enemy.transform.position += enemy.speedEnemy * enemy.motionDirection;
                }
            }
            else{
                enemy.transform.position = enemy.transform.position;
            }   

            return null;
        }
    }

    protected class Ram : EnemyState
    {
        public Ram(NewEnemy enemy) : base(enemy) {            
        }

        public override void enter(State from){
        }
        public override void exit(State to){            
        }
        public override State update(){
            if(enemy.player.isActive){
                enemy.motionTarget = enemy.player.transform.position;
                enemy.motionDirection = (enemy.motionTarget - enemy.transform.position).normalized;
                enemy.transform.position += enemy.speedEnemy * enemy.motionDirection;
            }
            return null;
        }
    }

    protected class Destroyed : EnemyState
    {
        public float rotationMultiplier = 1;
        float tick = 0;
        public Destroyed(NewEnemy enemy, float rm = 1) : base(enemy){
            rotationMultiplier = rm;
        }
        public override void enter(State from){
            tick = Time.time;
        }
        public override void exit(State to){
            
        }
        public override State update(){   
            
            enemy.transform.Rotate(0, Time.deltaTime * rotationMultiplier, 0);
            float t = Time.time - tick;
            enemy.transform.localScale = Vector3.Lerp(Vector3.one, Vector3.zero, t); 
 
            if (t >= 1){
                enemy.destroy();
            }
            return null;
        }
    } 

    protected StateMachine stateMachine = new StateMachine();
    abstract public void enemyStart(Player player);
    virtual public void destroy()
    {
        Destroy(gameObject, 0f);
    }
    // {
    //     this.player = player;

    //     move = new Move(this);
    //     ram = new Ram(this);

    //     move.decisions.Add(ram);
    //     ram.decisions.Add(move);

    //     stateMachine.currentState = move; 
    // }

    // Update is called once per frame
    protected virtual void Update()
    {
        stateMachine.update();
    }
}

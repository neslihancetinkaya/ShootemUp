using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static StateMachine;

public partial class Player : MonoBehaviour
{  
    delegate void FireLaserDelegate();
    public Player player;
    public GameObject laserGreenPrefab;             
    public GameObject laserRedPrefab;               
    public float xConstraint = Director.xConstraint;
    public float yConstraint = Director.yConstraint;
    public float tick = 0;                           
    public float delay = 0.3f;     
    public int speedPlayer = 500;
    public int maxCountLaser = 15;
    public int lifePlayer = 3;
    public static int countLaser = 0;
    FireLaserDelegate fireLaserDelegate;

    protected abstract class PlayerState : State
    {
        protected Player player{get; private set;}
        public PlayerState(Player player){
            this.player = player;
        }
    }    

    PPlayState pPlayState; 
    IngressState ingressState;
    DestroyState destroyState;
    StateMachine stateMachine = new StateMachine();

    public bool isActive => stateMachine.currentState == pPlayState;

    virtual public void destroy()
    {
        // Destroy(gameObject, 0f);
        gameObject.SetActive(false);
    }
    
    // Start is called before the first frame update
    void Start()
    {        
        pPlayState = new PPlayState(this);
        ingressState = new IngressState(this);
        destroyState = new DestroyState(this);

        stateMachine.currentState = ingressState;        
    }

    // Update is called once per frame
    void Update()
    {
        stateMachine.update();        
    }
}
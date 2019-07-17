using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static StateMachine;

public class Director : MonoBehaviour
{
    public Text scoreDisplay;
    public Player player;
    public GameObject E1Prefab;
    public GameObject E2Prefab;
    public GameObject E3Prefab;
    public static float xConstraint = 1850f;
    public static float yConstraint = 950f;
    public static int countE1 = 1;
    public static int countE2 = 1;
    public static int countE3 = 1;
    
    StateMachine stateMachine = new StateMachine();

    public abstract class DirectorState : State
    {
        public Director director {get; set;}
        public DirectorState(Director director) {
            this.director = director;
        }
    }

    public class StartState : DirectorState
    {
        bool playerIngressCompleted = false;
        public StartState(Director director) : base(director) {
        }

        public override void enter(State from){
            director.score = 0;
            director.player.ingressCompleted += playerIngressCompletedHandler;
            director.player.ingress();
            //enmey create
            director.createE1();
            director.createE2();
            director.createE3();
        }

        void playerIngressCompletedHandler()
        {
            playerIngressCompleted = true;
        }

        public override void exit(State to)
        {
            director.player.ingressCompleted -= playerIngressCompletedHandler;
        }

        public override State update(){
            ////////////////////////////////
            return playerIngressCompleted ? director.playState : null;
        }
    }

    public class PlayState : DirectorState
    {

        public PlayState(Director director) : base(director) {
        }

        public override void enter(State from){
            // director.createE1();
            // director.createE2();
            // director.createE3();
        }

        public override void exit(State to){            
        }

        public override State update(){

            if(director.player.isActive){
                if(countE1 < 2){
                    director.createE1();
                }
                if(countE2 < 2){
                    director.createE2();
                }
                if(countE3 < 2){
                    director.createE3();
                }
            }
            return null;
        }
    }

    StartState startState;
    PlayState playState;

    public void createE1(){
        float randX = Random.Range(-xConstraint, xConstraint);
        float randY = Random.Range(-yConstraint, yConstraint);
        GameObject go = Instantiate(E1Prefab, new Vector3(randX, randY, 0f), Quaternion.identity) as GameObject;
        go.GetComponent<E1>().enemyStart(this, player);
        countE1++;
    }

    public void createE2(){
        float randX = Random.Range(-xConstraint, xConstraint);
        float randY = Random.Range(-yConstraint, yConstraint);
        GameObject go = Instantiate(E2Prefab, new Vector3(randX, randY, 0),Quaternion.identity) as GameObject;
        go.GetComponent<E2>().enemyStart(this, player);
        countE2++;
    }

    public void createE3(){
        float randX = Random.Range(-xConstraint, xConstraint);
        float randY = Random.Range(-yConstraint, yConstraint);
        GameObject go = Instantiate(E3Prefab, new Vector3(randX, randY, 0),Quaternion.identity) as GameObject;
        go.GetComponent<E3>().enemyStart(this, player);
        countE3++;
    }

    int _score = 0;

    public int score{
        get => _score;
        set {
            Debug.Log("Score : " + value + " " + (value - _score));
            _score = value;
            scoreDisplay.text = "Score : " + _score;
        }
    }

    void Awake()
    {
        startState = new StartState(this);
        playState = new PlayState(this);
    }
    
    // Start is called before the first frame update
    void Start()
    {
        stateMachine.currentState = startState;
    }

    // Update is called once per frame
    void Update()
    {
        stateMachine.update();
    }
}
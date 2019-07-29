using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static StateMachine;

public class Director : MonoBehaviour
{
    [System.Serializable]
    public class EnemyParameters{
        public string id;
        public int minCount;
        public GameObject prefab;
        
        [System.NonSerialized]
        public int count;
    }
    public Text scoreDisplay;
    public Player player;
    // public GameObject E1Prefab;
    // public GameObject E2Prefab;
    // public GameObject E3Prefab;
    public SpriteRenderer background;
    public Transform planet;
    public static float xConstraint = 1850f;
    public static float yConstraint = 950f;
    // public static int countE1 = 1;
    // public static int countE2 = 1;
    // public static int countE3 = 1;

    public EnemyParameters[] enemies = new EnemyParameters[0];
    public AudioSource audioSource;
    
    StateMachine stateMachine = new StateMachine();
    float position = 0f;

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
            //enemy create
            // director.createE1();
            // director.createE2();
            // director.createE3();
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
            // createEnemy(director.);
            return playerIngressCompleted ? director.playState : null;
        }
    }

    public class PlayState : DirectorState
    {
        public StateMachine stateMachine = new StateMachine();

        SectionState[] sections;

        public PlayState(Director director) : base(director) {
            sections = new SectionState[] {
                new SectionState(director, 0, director.enemies[0]),
                new SectionState(director, 0.25f, director.enemies[0], director.enemies[1]),
                new SectionState(director, 0.5f, director.enemies[2]),
                new SectionState(director, 0.75f, director.enemies[0], director.enemies[1], director.enemies[2])
            };

            for (int i = 1; i < sections.Length; i++) {
                sections[i-1].decisions.Add(sections[i]);
            }
        }

        public override void enter(State from){
            // director.createE1();
            // director.createE2();
            // director.createE3();
            stateMachine.currentState = sections[0];
        }

        public override void exit(State to){            
        }

        public override State update(){

            // if(director.player.isActive){
            //     if(countE1 < 2){
            //         director.createE1();
            //     }
            //     if(countE2 < 2){
            //         director.createE2();
            //     }
            //     if(countE3 < 2){
            //         director.createE3();
            //     }
            // }
            stateMachine.update();
            return null;
        }
    }

    public class SectionState : DirectorState, Decision
    {
        public float start {get; protected set;}
        EnemyParameters[] enemies;
        
        public SectionState(Director director, float start, params EnemyParameters[] enemies) : base(director)
        {
            this.start = start;
            this.enemies = enemies;
        }
        
        public State decide(State currentState){
            return director.position >= start ? this : null;
        }

        public override State update() {
            foreach(EnemyParameters enemy in enemies){
                if (enemy.count < enemy.minCount) {
                    director.createEnemy(enemy);
                }
            }

            return null;
        }

    }

    StartState startState;
    PlayState playState;

    public void createEnemy(EnemyParameters enemy){
        float randX = Random.Range(-xConstraint, xConstraint);
        float randY = Random.Range(-yConstraint, yConstraint);
        GameObject go = Instantiate(enemy.prefab, new Vector3(randX, randY, 0f), Quaternion.identity) as GameObject;
        go.GetComponent<NewEnemy>().enemyStart(this, enemy, player);
        enemy.count++;
    }

    // public void createE1(){
    //     float randX = Random.Range(-xConstraint, xConstraint);
    //     float randY = Random.Range(-yConstraint, yConstraint);
    //     GameObject go = Instantiate(E1Prefab, new Vector3(randX, randY, 0f), Quaternion.identity) as GameObject;
    //     go.GetComponent<E1>().enemyStart(this, player);
    //     countE1++;
    // }

    // public void createE2(){
    //     float randX = Random.Range(-xConstraint, xConstraint);
    //     float randY = Random.Range(-yConstraint, yConstraint);
    //     GameObject go = Instantiate(E2Prefab, new Vector3(randX, randY, 0),Quaternion.identity) as GameObject;
    //     go.GetComponent<E2>().enemyStart(this, player);
    //     countE2++;
    // }

    // public void createE3(){
    //     float randX = Random.Range(-xConstraint, xConstraint);
    //     float randY = Random.Range(-yConstraint, yConstraint);
    //     GameObject go = Instantiate(E3Prefab, new Vector3(randX, randY, 0),Quaternion.identity) as GameObject;
    //     go.GetComponent<E3>().enemyStart(this, player);
    //     countE3++;
    // }

    int _score = 0;

    public int score{
        get => _score;
        set {
            Debug.Log("Score : " + value + " " + (value - _score));
            _score = value;
            scoreDisplay.text = "Score : " + _score;
        }
    }

    public void playAudio(AudioClip clip)
    {
        audioSource.clip = clip;
        audioSource.Play();
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
        position += Time.deltaTime * 0.01f;
        stateMachine.update();

        background.material.SetTextureOffset("_MainTex", new Vector2(0, Time.time * 0.8f));
        background.material.SetFloat("transition", position);
        background.material.SetFloat("position", position);

        planet.position = Vector3.Lerp(new Vector3(0, 1300, 0), new Vector3(0, -1300, 0), position);
    }
}
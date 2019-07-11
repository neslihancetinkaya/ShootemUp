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
    public float rotationMultiplier = 720; 
    public int speedPlayer = 500;
    public int maxCountLaser = 15;
    public int lifePlayer = 3;
    public static int countLaser = 0;
    public bool isActive = true;
    FireLaserDelegate fireLaserDelegate;

    protected abstract class PlayerState : State
    {
        protected Player player{get; private set;}
        public PlayerState(Player player){
            this.player = player;
        }
    }

    
    void createGreenLaser(){
        Vector3 laserPosition = new Vector3(transform.position.x, transform.position.y + 20f, 0f);
        GameObject go = Instantiate(laserGreenPrefab, laserPosition, Quaternion.identity) as GameObject;
        go.GetComponent<GreenLaser>().laserStart(player); 
        countLaser++;
    }
    void createRedLaser(){
        GameObject go = Instantiate(laserRedPrefab, transform.position, Quaternion.identity) as GameObject;
        go.GetComponent<RedLaser>().laserStart(player);
        countLaser++;
    }

    void OnCollisionEnter2D(Collision2D collision){
        NewEnemy enemy = collision.gameObject.GetComponent<NewEnemy>();
        if(enemy != null && enemy.hit(3)){
            lifePlayer--;
            if(lifePlayer <= 0){
                gameObject.SetActive(false);
                isActive = false;
            }
            else {
                transform.position = Vector3.zero;
            }
        }
    }

    PPlayState pPlayState; 
    IngressState ingressState;
    DestroyState destroyState;

    StateMachine stateMachine = new StateMachine();
    // Start is called before the first frame update
    void Start()
    {
        // startta ingress state ila başlayacak 
        // update içinde kontrol edilip ingress sonlandırılacak
        // update içinde stateler arası geçişlere karar verilecek

        pPlayState = new PPlayState(this);
        ingressState = new IngressState(this);
        destroyState = new DestroyState(this);

        stateMachine.currentState = ingressState;
        
        fireLaserDelegate = createGreenLaser;
        // fireLaserDelegate += Osman;
    }

    // void Osman(){
    //     Debug.Log("osman");
    // }

    // Update is called once per frame
    void Update()
    {
        stateMachine.update();    

        // Left
        if(Input.GetKey(KeyCode.LeftArrow) && transform.position.x > -xConstraint){
            transform.position = new Vector3(transform.position.x - speedPlayer*Time.deltaTime, transform.position.y, 0f);
        }
        // Up
        if(Input.GetKey(KeyCode.UpArrow) && transform.position.y < yConstraint){
            transform.position = new Vector3(transform.position.x, transform.position.y + speedPlayer*Time.deltaTime, 0f);
        }
        // Down
        if(Input.GetKey(KeyCode.DownArrow) && transform.position.y > -yConstraint){
            transform.position = new Vector3(transform.position.x, transform.position.y - speedPlayer*Time.deltaTime, 0f);
        }

        // Fire
        if(Input.GetKey(KeyCode.Space) && tick < Time.time && countLaser < maxCountLaser){ 
            tick = Time.time + delay;
            fireLaserDelegate();
        }

        // Fire Green Laser
        if(Input.GetKeyDown(KeyCode.Alpha1)){
            fireLaserDelegate = createGreenLaser;
        }

        // Fire Red Laser
        if(Input.GetKeyDown(KeyCode.Alpha2)){
            fireLaserDelegate = createRedLaser;
        }      
        //stateMachine.currentState = pPlayState;

        // transform.Rotate(Time.deltaTime * rotationMultiplier, 0, 0);   
        // float newT = Time.time - t;
        // transform.localScale = Vector3.Lerp(Vector3.one, Vector3.zero, newT);
        // if(newT >= 1){
        //     gameObject.SetActive(false);
        // }        
    }
}
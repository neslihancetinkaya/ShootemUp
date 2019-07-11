using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Director : MonoBehaviour
{
    public Player player;
    public GameObject E1Prefab;
    public GameObject E2Prefab;
    public GameObject E3Prefab;
    public static float xConstraint = 1850f;
    public static float yConstraint = 950f;
    public static int countE1 = 1;
    public static int countE2 = 1;
    public static int countE3 = 1;

    public void createE1(){
        float randX = Random.Range(-xConstraint, xConstraint);
        float randY = Random.Range(-yConstraint, yConstraint);
        GameObject go = Instantiate(E1Prefab, new Vector3(randX, randY, 0f), Quaternion.identity) as GameObject;
        go.GetComponent<E1>().enemyStart(player);
        countE1++;
    }

    public void createE2(){
        float randX = Random.Range(-xConstraint, xConstraint);
        float randY = Random.Range(-yConstraint, yConstraint);
        GameObject go = Instantiate(E2Prefab, new Vector3(randX, randY, 0),Quaternion.identity) as GameObject;
        go.GetComponent<E2>().enemyStart(player);
        countE2++;
    }

    public void createE3(){
        float randX = Random.Range(-xConstraint, xConstraint);
        float randY = Random.Range(-yConstraint, yConstraint);
        GameObject go = Instantiate(E3Prefab, new Vector3(randX, randY, 0),Quaternion.identity) as GameObject;
        go.GetComponent<E3>().enemyStart(player);
        countE3++;
    }
    
    // Start is called before the first frame update
    void Start()
    {
        createE1();
        createE2();     
        createE3();   
    }

    // Update is called once per frame
    void Update()
    {
        if(countE1 < 2){
            createE1();
        }
        if(countE2 < 2){
            createE2();
        }
        if(countE3 < 2){
            createE3();
        }
    }
}
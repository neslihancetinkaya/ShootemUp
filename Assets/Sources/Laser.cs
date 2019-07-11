using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Laser : MonoBehaviour
{
    public Player player;
    public float startTime;
    public float elapsedTime;
    public int speedLaser = 800;
    public int damage = 0;

    // Start is called before the first frame update
    public void laserStart(Player player)
    {
        this.player = player;
        startTime = Time.time;        
    }

    protected abstract void OnCollisionEnter2D(Collision2D collision);    

    // Update is called once per frame
    protected virtual void Update()
    {
        transform.position += Vector3.up * speedLaser * Time.deltaTime;

        //Destroy laser after distance
        if(transform.position.y > 1000f){
            Destroy(gameObject, 0f);
            Player.countLaser--;
        }          

        //Destroy laser after time
        elapsedTime = Time.time - startTime;
        if(elapsedTime > 4f){
            Destroy(gameObject, 0f);
            Player.countLaser--;
        }
    }
}
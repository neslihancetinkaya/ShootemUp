using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenLaser : Laser
{
    protected override void OnCollisionEnter2D(Collision2D collision){
        NewEnemy enemy = collision.gameObject.GetComponent<NewEnemy>();
        if(enemy != null && enemy.hit(1)){
            Destroy(gameObject, 0f);
            Player.countLaser--;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : BulletFather
{
    bool isExploded;
    void Start()
    {
        isRocket = true;
        rig = GetComponent<Rigidbody2D>();
    }


    void FixedUpdate()
    {
        Move();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            if (collision.gameObject.GetComponent<LifeSystem>())
            {
                collision.gameObject.GetComponent<LifeSystem>().OnDamage(damage);
            }
            if (!isExploded)
            {
                rig.velocity = Vector2.zero;
                GetComponent<Animator>().Play("RocketExplosion");//O foguete ira explodir a partir dessa animacao
                isExploded = true;//Para garantir que ele exploda apenas uma vez
                damage = (int)(damage * 0.75f);//E quem for pego na explosao, vai perder menos vida que quem foi atingido pelo foguete
            }
            
        }
    }
    public void DeleteRocket()//Chamado no AnimationEvent, no fim da animacao da explosao
    {
        Destroy(gameObject);
    }
}
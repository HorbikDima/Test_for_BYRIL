using System;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private EnemyData data;
    //Инициализация врага
    public void Init(EnemyData _data)
    {
        data = _data;
        GetComponent<SpriteRenderer>().sprite = data.MainSprite;
    }

    public float Attack
    {
        get { return data.Attack; }
        protected set { }
    }

    public static Action<GameObject> OnEnemyOrderFly;
    private void FixedUpdate()
    {
        transform.Translate(Vector2.up * data.Speed);

        if(transform.position.y < -10 && OnEnemyOrderFly != null)
        {
            OnEnemyOrderFly(gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Laser")
        {
            transform.position = new Vector2(transform.position.x, -6);
        }
    }
}

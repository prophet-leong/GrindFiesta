using UnityEngine;
using System.Collections;

public class Hero : MonoBehaviour {


    public int attack;
    public int upgradeLevel;
    public int price;
    public float previousPrice;
    public float attackTime;
    public float attackTimeConst;
    public string name;
    public SpriteRenderer spriteRenderer;
    public Animator anim;
    public GameObject Unit;//this will hold most of the hero componenets
	// Use this for initialization
	void Start () 
    {
        attack = 1;
        upgradeLevel = 1;
        price = 1;
        attackTimeConst = 1.0f;
        attackTime = attackTimeConst;
        name = "hero";
    }
	
	// Update is called once per frame
	public virtual void Update () 
    {
        attackEnemy();
	}
    public virtual void attackEnemy()
    {
        attackTime -= Time.deltaTime;
        if(attackTime <= 0)
        {
            attackTime = attackTimeConst;
            Attack();
        }
    }
    public virtual void UpgradeStats()
    {
        Debug.Log("UpgradeStatsRan");
        if(UserSingleton.GetInstance().gold - price > 0)
        {
            Debug.Log("UpgradeStatsAddStats");
            attack += (int)(0.5f*UserSingleton.GetInstance().currentStage);
            UserSingleton.GetInstance().gold -= price; 
            price += (int)(price*0.7f);
        }
    }
    public virtual void Attack()
    {
        UserSingleton.GetInstance().enemy.TakeDamage(attack);
    }
}

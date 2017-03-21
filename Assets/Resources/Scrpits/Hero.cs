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
    public void InitPosition()
    {
        if(UserSingleton.GetInstance().mainHero == this)
        {
            Debug.Log("RAN");
            this.SetToMain();
        }
        else
        {
            for (int i = 0; i < UserSingleton.GetInstance().heroList.Count;++i)
            {
                if(UserSingleton.GetInstance().heroList[i] == this)
                {
                    PutToSideLines(i-1);
                    Debug.Log("RAN2");
                    return;
                }
            }
        }
    }
    public void PutToSideLines(int position)
    {
        int direction = position % 2;
        int placement ;
        if(direction == 1)
            placement = -(position +2)/2;
        else
            placement = (position +2) / 2;
        /********move it to the sidelines**********/
        transform.localPosition = new Vector3(placement *0.5f, 0, -1);
        transform.localScale = new Vector3(0.05f, 0.05f, 0.05f);
    }
    public void SetToMain()
    {
        /********move it to the sidelines**********/
        transform.localPosition = new Vector3(0, 0, 0);
        transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
    }
}

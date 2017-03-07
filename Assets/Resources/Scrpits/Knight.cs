using UnityEngine;
using System.Collections;

public class Knight : Hero {

	// Use this for initialization
    public SpriteRenderer spriteRenderer;
    public Animator anim;
    public GameObject Unit;//this will hold most of the hero componenets
	void Start ()
    {
        //basic init
        attack = 1;
        upgradeLevel = 1;
        price = 2;
        attackTimeConst = 100.0f;
        attackTime = attackTimeConst;
        //position for this overall holder
        transform.localPosition = new Vector3(0, 0, -1);
        transform.localScale = new Vector3(0.2f, 0.2f);
        
        /************Make the GameScene the parent of the holder*************/
        transform.SetParent(GameObject.FindGameObjectWithTag("GameScene").transform);
        /**********Make Unit a child of this holder*************/
        Unit = new GameObject();
        Unit.transform.SetParent(transform);
        Unit.transform.localScale = new Vector3(1, 1, 1);
        /***********************Unit componenets**************************/
        Unit.AddComponent<SpriteRenderer>();
        spriteRenderer = Unit.GetComponent<SpriteRenderer>();
        Unit.AddComponent<Animator>();
        anim = Unit.GetComponent<Animator>();
        //Loading of sprites and animations
        anim.runtimeAnimatorController = Resources.Load("Animation/Knight/KnightController") as RuntimeAnimatorController;
        spriteRenderer.sprite = Resources.Load<Sprite>("Sprites/Knight/Idle (1)");
        //anim.SetInteger("State", 0);
        /*****************************************************************/
	}
    public virtual void UpgradeStats()
    {
        if (UserSingleton.GetInstance().gold - price > 0)
        {
            attack += (int)(0.5f * UserSingleton.GetInstance().currentStage);
            UserSingleton.GetInstance().gold -= price;
            price += (int)(price * 1.25f);
        }
    }
    // Update is called once per frame
    public override void Update()
    {
        attackEnemy();
        AutoIdle();
    }
    void AutoIdle()
    {
        if (anim.GetInteger("State") != 1)//runs only when its in Hurt state
        {
            if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1)//if animation is complete set back to 0
            {
                anim.SetInteger("State", 0);// state 0 will always be idle
            }
        }
    }
    public override void attackEnemy()
    {
        attackTime -= Time.deltaTime;
        if (attackTime <= 0)
        {
            attackTime = attackTimeConst;
            Attack();
        }
    }
    public override void Attack()
    {
        UserSingleton.GetInstance().enemy.TakeDamage(attack);
        anim.Play("Attack", 0, 0.0f);
        anim.SetInteger("State", 1);
    }
}

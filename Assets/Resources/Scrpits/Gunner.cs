using UnityEngine;
using System.Collections;

public class Gunner : Hero
{
    /*List of declared variables in Hero
    public int attack;
    public int upgradeLevel;
    public int price;
    public float previousPrice;
    public float attackTime;
    public float attackTimeConst;
    public SpriteRenderer spriteRenderer;
    public Animator anim;
    public GameObject Unit;//this will hold most of the hero componenets
    */
    // Use this for initialization
    public Gunner()
    {
        //basic init
        attack = 1;
        upgradeLevel = 1;
        price = 2;
        attackTimeConst = 1.0f;
        attackTime = attackTimeConst;
        name = "Gunner";

        SpecialAttackCD = 60.0f;
        SpecialAttackCurrentCD = 1.0f;
        specialAttackAvaliable = true;
    }
    void Start()
    {
        //position for this overall holder

        /************Make the GameScene the parent of the holder*************/
        transform.SetParent(GameObject.FindGameObjectWithTag("GameScene").transform);
        /**********Make Unit a child of this holder*************/
        Unit = new GameObject();
        Unit.transform.SetParent(transform);
        Unit.transform.localPosition = new Vector3(0, 0, 0);
        InitPosition();
        Unit.transform.localScale = new Vector3(1, 1, 1);
        /***********************Unit componenets**************************/
        Unit.AddComponent<SpriteRenderer>();
        spriteRenderer = Unit.GetComponent<SpriteRenderer>();
        Unit.AddComponent<Animator>();
        anim = Unit.GetComponent<Animator>();
        //Loading of sprites and animations
        anim.runtimeAnimatorController = Resources.Load("Animation/Gunner/GunnerController") as RuntimeAnimatorController;
        spriteRenderer.sprite = Resources.Load<Sprite>("Sprites/Gunner/Idle (1)");
        //anim.SetInteger("State", 0);
        /*****************************************************************/

        //create the upgrade tab
        UpgradeTabManager.GetInstance().CreateTab(this);
    }
    public override void UpgradeStats()
    {
        if (UserSingleton.GetInstance().gold - price >= 0)
        {
            attack += (int)(0.5f * UserSingleton.GetInstance().currentStage);
            UserSingleton.GetInstance().AddGold(-price);
            price += (int)(price * 1.25f);
        }
    }
    // Update is called once per frame
    public override void Update()
    {
        attackEnemy();
        AutoIdle();
        UpdateSpecial();
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
        if (attackTime <= 0 && UserSingleton.GetInstance().mainHero != this)
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
    public override void SpecialSkill()
    {
            Debug.Log("SPeCIAL ATTACK");
        if(specialAttackAvaliable == true)
        {
            anim.Play("SpecialAttack", 0, 0.0f);
            //anim.SetInteger("State", 2);
            UserSingleton.GetInstance().enemy.TakeDamage(attack * 100);
            UseSpecial();
        }
    }
}

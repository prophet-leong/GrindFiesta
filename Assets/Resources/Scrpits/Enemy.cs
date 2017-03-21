using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class Enemy : MonoBehaviour {

    public enum MobType
    {
        basic,
    };
    public int health;
    public int Max_Health;
    public int lootGold;
    public MobType mobType;
    Animator anim;
    SpriteRenderer spriteRenderer;
    GameObject damageTextHolder;
    GameObject[] damageText;
    GameObject goldText;
    GameObject HealthBar;
    int damageTextIndex;
    bool markDead;
	// Use this for initialization
    void Awake()
    {
        markDead = false;
        mobType = MobType.basic;
        /*Enemy initialize*/
        gameObject.AddComponent<SpriteRenderer>();
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        gameObject.AddComponent<Animator>();
        anim = gameObject.GetComponent<Animator>();
        transform.localPosition = new Vector3(0, 1.5f);
        transform.localScale = new Vector3(0.5f, 0.5f);
        /*DamageText initialize*/
        damageTextIndex = 0;
        //holder
        damageTextHolder = new GameObject();
        damageTextHolder.name = "damageTextHolder";
        damageTextHolder.transform.SetParent(GameObject.FindGameObjectWithTag("Canvas").transform);
        damageTextHolder.transform.position = new Vector3(transform.position.x,transform.position.y,0);
        damageTextHolder.transform.localScale = new Vector3(3, 3, 3);
       
        /**********************gold Text************************/
                         /*Basic initalize*/
        goldText = new GameObject();
        goldText.transform.SetParent(damageTextHolder.transform);
        //text
        goldText.AddComponent<Text>();
        goldText.GetComponent<RectTransform>().rect.Set(0, 0, 160, 30);
        goldText.GetComponent<Text>().font = Resources.GetBuiltinResource(typeof(Font), "Arial.ttf") as Font;
        goldText.GetComponent<Text>().alignment = TextAnchor.MiddleCenter;
        goldText.GetComponent<Text>().fontSize = 20;
        goldText.GetComponent<Text>().color = new Color(1.0f,0.9f,0.0f);//gold color
        //animator
        goldText.AddComponent<Animator>();
        goldText.GetComponent<Animator>().runtimeAnimatorController = Resources.Load("Animation/DamageText/Text") as RuntimeAnimatorController;
        goldText.GetComponent<Animator>().speed = 0.5f;
        goldText.SetActive(false);

        /**********************text********************************/
        damageText = new GameObject[10];
        for (int i = 0; i < damageText.Length;++i)
        {
            /*Basic initalize*/
            damageText[i] = new GameObject();
            damageText[i].transform.SetParent(damageTextHolder.transform);
            //text
            damageText[i].AddComponent<Text>();
            damageText[i].GetComponent<RectTransform>().rect.Set(0,0,160,30);
            damageText[i].GetComponent<Text>().font = Resources.GetBuiltinResource(typeof(Font), "Arial.ttf") as Font;
            damageText[i].GetComponent<Text>().alignment = TextAnchor.MiddleCenter;
            damageText[i].GetComponent<Text>().fontSize = 20;
            //animator
            damageText[i].AddComponent<Animator>();
            damageText[i].GetComponent<Animator>().runtimeAnimatorController = Resources.Load("Animation/DamageText/Text") as RuntimeAnimatorController;
            
            damageText[i].SetActive(false);
        }
        /***********************HealthBar******************************/
        GameObject temp = Resources.Load("Prefabs/HealthBarBack") as GameObject;
        HealthBar = Instantiate(temp);
        HealthBar.transform.SetParent(damageTextHolder.transform);
        HealthBar.transform.localPosition = new Vector3(0, 60, 0);
        HealthBar.transform.localScale = new Vector3(1, 1, 1);
        /***Basic stats**/
        lootGold = 5;
        Max_Health = 10;
        health = Max_Health;
    }
	void Start () 
    {
        //basic stats
        //set the gamescene to parent
        transform.SetParent(GameObject.FindGameObjectWithTag("GameScene").transform);
        //animations and sprites
        anim.runtimeAnimatorController = Resources.Load("Animation/Cat/CatController") as RuntimeAnimatorController;
        anim.SetInteger("State", 1);
        spriteRenderer.sprite = Resources.Load<Sprite>("Sprites/Cat/Idle (1)");
	}
	void Reset()
    {
        Debug.Log("revive");
        //reset the health
        Max_Health += (int)(Max_Health * 0.2f);
        health = Max_Health;
        //reset the enemy animatations
        anim.Play("Idle", 0, 0.0f);
        anim.SetInteger("State", 0);
        //set the dead state to false
        markDead = false;
        //Update the health bar
        UpdateHealthBar();
    }
	// Update is called once per frame
	void Update ()
    {
        if(anim.GetInteger("State") == 1)//runs only when its in Hurt state
        {
            //Debug.Log(anim.GetInteger("State"));
            if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1)//if animation is complete set back to 0
            {
                anim.SetInteger("State", 0);
            }
        }
        else if (anim.GetInteger("State") == 2)
        {
            if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1)//if animation is complete set back to 0
            {
                Reset();
            }
        }
	}
    void UpdateHealthBar()
    {
        float fill = ((float)health / (float)Max_Health);
        HealthBar.transform.GetChild(0).GetComponent<Image>().fillAmount = fill;
        HealthBar.SetActive(!markDead);
    }
    public void TakeDamage(int dmg)
    {
        if (markDead)
            return;
        health -= dmg;
        if(health <= 0)
        {
            health = 0;
            //dead here
            lootGold += (int)(lootGold * 0.2f);
            goldText.SetActive(true);
            goldText.GetComponent<Text>().text = lootGold.ToString() + " Gold";
            goldText.GetComponent<Animator>().Play("DamageText", 0, 0);
            UserSingleton.GetInstance().AddGold(lootGold);
            UserSingleton.GetInstance().NextStage();
            anim.Play("Dead", 0, 0.0f);
            anim.SetInteger("State", 2);
            markDead = true;
        }
        else
        {
            anim.Play("Hurt",0,0.0f);
            anim.SetInteger("State", 1);
        }
        if(damageTextIndex >= damageText.Length)
        {
            damageTextIndex = 0;
        }
        damageText[damageTextIndex].SetActive(true);
        damageText[damageTextIndex].GetComponent<Text>().text = dmg.ToString();
        damageText[damageTextIndex].GetComponent<Animator>().Play("DamageText", 0, 0);
        ++damageTextIndex;
        //Update the health bar fill amount
        UpdateHealthBar();
    }
}

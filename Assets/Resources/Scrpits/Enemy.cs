using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class Enemy : MonoBehaviour {


    public int health;
    Animator anim;
    SpriteRenderer spriteRenderer;
	// Use this for initialization
    void Awake()
    {
        gameObject.AddComponent<SpriteRenderer>();
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        gameObject.AddComponent<Animator>();
        anim = gameObject.GetComponent<Animator>();
        transform.localPosition = new Vector3(0, 1.5f);
        transform.localScale = new Vector3(0.5f, 0.5f);
    }
	void Start () 
    {
        //basic stats
        health = 100;
        //set the gamescene to parent
        transform.SetParent(GameObject.FindGameObjectWithTag("GameScene").transform);
        //animations and sprites
        anim.runtimeAnimatorController = Resources.Load("Animation/Cat/CatController") as RuntimeAnimatorController;
        anim.SetInteger("State", 0);
        spriteRenderer.sprite = Resources.Load<Sprite>("Sprites/Cat/Idle (1)");
	}
	
	// Update is called once per frame
	void Update ()
    {
        if(anim.GetInteger("State") == 1)//runs only when its in Hurt state
        {
            if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1)//if animation is complete set back to 0
            {
                anim.SetInteger("State", 0);
            }
        }
	}

    public void TakeDamage(int dmg)
    {
        health -= dmg;
        if(health <= 0)
        {
            //dead here
        }
        else
        {
            anim.SetInteger("State", 1);
            anim.Play("Hurt",0,0.0f);
        }
    }
}

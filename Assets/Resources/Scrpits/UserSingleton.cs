using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
/*this class is for current data*/
/*if theres a need to add to the history go to UserHistory*/
public class UserSingleton : Singleton<UserSingleton> { 

    public List<Hero> heroList;
    public int currentSelectedHero;
    public Hero mainHero;
    public Enemy enemy;
    public int currentStage;
    public int gold;
    public GameObject Options;// this is used in Upgrade Tab Manager
    GameObject stageNumberUI;
    GameObject goldUI;
    public void SetData(int gold ,int currentstage)
    {
        this.gold = gold;
        this.currentStage = currentstage;
        UpdateStageNumber();
        UpdateGoldNumber();
    }
    public void SetEnemy(int max_health,int lootgold,Enemy.MobType mobtype)
    {
        enemy.Max_Health = max_health;
        enemy.lootGold = lootgold;
        enemy.mobType = mobtype;
        Debug.Log(enemy.lootGold);
    }
    //constructor since the start function wont run
    public UserSingleton()
    {
        heroList = new List<Hero>();
        /**********Gunner********/
        GameObject newHero = new GameObject();
        newHero.AddComponent<Gunner>();
        heroList.Add(newHero.GetComponent<Gunner>());
        /*********Knight*********/
        for (int i = 0; i < 5;++i )
        {
            newHero = new GameObject();
            newHero.AddComponent<Knight>();
            //newHero.GetComponent<Hero>().PutToSideLines(heroList.Count);
            heroList.Add(newHero.GetComponent<Knight>());

        }

        //heroList[0].SetToMain();
        //heroList[1].PutToSideLines(1);

        mainHero = heroList[0];
        GameObject newEnemy = new GameObject();
        newEnemy.AddComponent<Enemy>();
        enemy = newEnemy.GetComponent<Enemy>();//same as the hero warnings//TODO:Solve the Warnings
        currentStage = 1;
        gold = 0;
        InitUI();
    }
    void InitUI()
    {
        GameObject canvas = GameObject.FindGameObjectWithTag("Canvas");

        /************Options backdrop*****************/
        Options = new GameObject();
        Options.transform.SetParent(canvas.transform);
        Options.name = "OptionsBackDrop";
        Options.AddComponent<Image>();
        Options.GetComponent<RectTransform>().sizeDelta = new Vector2(canvas.GetComponent<RectTransform>().sizeDelta.x, canvas.GetComponent<RectTransform>().sizeDelta.y / 2);
        Options.transform.localPosition = new Vector3(0, -canvas.GetComponent<RectTransform>().sizeDelta.y / 4 * 1.2f, 0);
        Options.transform.localScale = new Vector3(1, 1, 1);
        /******************Stage Number*************************/
        stageNumberUI = new GameObject();
        GameObject StageNumberUIPrefab = Resources.Load("Prefabs/StageNumberUI") as GameObject;
        stageNumberUI = GameObject.Instantiate(StageNumberUIPrefab);
        stageNumberUI.transform.SetParent(canvas.transform);
        stageNumberUI.transform.localScale = new Vector3(1, 1, 1);
        stageNumberUI.transform.localPosition = new Vector3(0, canvas.GetComponent<RectTransform>().rect.height / 2 - stageNumberUI.GetComponent<RectTransform>().rect.height / 2, 0);
        UpdateStageNumber();
        /******************Gold UI****************************/
        goldUI = new GameObject();
        GameObject goldUIPRefab = Resources.Load("Prefabs/GoldUIPrefab") as GameObject;
        goldUI = GameObject.Instantiate(goldUIPRefab);
        goldUI.transform.SetParent(Options.transform);
        goldUI.transform.localScale = new Vector3(1, 1, 1);
        goldUI.transform.localPosition = new Vector3(-Options.GetComponent<RectTransform>().rect.height * 0.35f, Options.GetComponent<RectTransform>().rect.height * 0.45f, 0);
        UpdateGoldNumber();
    }
    void UpdateStageNumber()
    {
        stageNumberUI.transform.FindChild("StageNumber").GetComponent<Text>().text = "Stage\n" + currentStage.ToString();
    }
    void UpdateGoldNumber()
    {
        goldUI.GetComponent<Text>().text = gold.ToString();
    }
    public void AddGold(int goldEarned)
    {
        gold += goldEarned;
        UpdateGoldNumber();
    }
    public void NextStage()
    {
        currentStage++;
        UpdateStageNumber();
    }
    public void NextHero()
    {
        ++currentSelectedHero;
        if(currentSelectedHero < heroList.Count)
        {
            mainHero.PutToSideLines(currentSelectedHero);
            mainHero = heroList[currentSelectedHero];
            mainHero.SetToMain();
        }
        else
        {
            currentSelectedHero = 0;
            mainHero.PutToSideLines(currentSelectedHero);
            mainHero = heroList[currentSelectedHero];
            mainHero.SetToMain();
        }
    }
    public void PrevHero()
    {
        --currentSelectedHero;
        if (currentSelectedHero >= 0)
        {
            mainHero.PutToSideLines(currentSelectedHero);
            mainHero = heroList[currentSelectedHero];
            mainHero.SetToMain();
        }
        else
        {
            currentSelectedHero = heroList.Count-1;
            mainHero.PutToSideLines(currentSelectedHero);
            mainHero = heroList[currentSelectedHero];
            mainHero.SetToMain();
        }
    }
}

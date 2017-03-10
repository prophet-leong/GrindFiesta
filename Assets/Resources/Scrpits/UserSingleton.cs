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
    GameObject stageNumberUI;
    //constructor since the start function wont run
    public UserSingleton()
    {
        heroList = new List<Hero>();
        /**********Gunner********/
        GameObject newHero = new GameObject();
        newHero.AddComponent<Gunner>();
        heroList.Add(newHero.GetComponent<Gunner>());
        /*********Knight*********/
        newHero = new GameObject();
        newHero.AddComponent<Knight>();
        heroList.Add(newHero.GetComponent<Knight>());

        heroList[0].SetToMain();
        heroList[1].PutToSideLines(1);

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
        stageNumberUI = new GameObject();
        GameObject StageNumberUIPrefab = Resources.Load("Prefabs/StageNumberUI") as GameObject;
        stageNumberUI = GameObject.Instantiate(StageNumberUIPrefab);
        GameObject canvas = GameObject.FindGameObjectWithTag("Canvas");
        stageNumberUI.transform.SetParent(canvas.transform);
        stageNumberUI.transform.localScale = new Vector3(1, 1, 1);
        stageNumberUI.transform.localPosition = new Vector3(0, canvas.GetComponent<RectTransform>().rect.height / 2 - stageNumberUI.GetComponent<RectTransform>().rect.height / 2, 0);
        UpdateStageNumber();
    }
    void UpdateStageNumber()
    {
        stageNumberUI.transform.FindChild("StageNumber").GetComponent<Text>().text = "Stage\n" + currentStage.ToString();
    }
    public void AddGold(int goldEarned)
    {
        gold += goldEarned;
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

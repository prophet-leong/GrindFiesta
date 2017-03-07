using UnityEngine;
using System.Collections;
using System.Collections.Generic;
/*this class is for current data*/
/*if theres a need to add to the history go to UserHistory*/
public class UserSingleton : Singleton<UserSingleton> { 

    public List<Hero> heroList;
    public Hero mainHero;
    public Enemy enemy;
    public int currentStage;
    public int gold;

    //constructor since the start function wont run
    public UserSingleton()
    {
        heroList = new List<Hero>();
        GameObject newHero = new GameObject();
        newHero.AddComponent<Knight>();
        heroList.Add(newHero.GetComponent<Knight>());//this will not be new hero(), and will solve the warnings//TODO:Solve the Warnings
        mainHero = heroList[0];
        GameObject newEnemy = new GameObject();
        newEnemy.AddComponent<Enemy>();
        enemy = newEnemy.GetComponent<Enemy>();//same as the hero warnings//TODO:Solve the Warnings
        currentStage = 1;
        gold = 0;
    }
}

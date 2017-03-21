using UnityEngine;
using System.Collections;
using Mono.Data.Sqlite;
using System.Data;
using System;
public class DataBase : MonoBehaviour {

    IDbConnection dbconn;
    string name;
    void Start()
    {
        string conn;
#if UNITY_ANDROID && !UNITY_EDITOR
        conn = "URI=file:" + Application.persistentDataPath + "/Assets/StreamingAssets/DataBase.db"; //Path to database.
        Debug.Log(Application.persistentDataPath);
#else
        conn = "URI=file:" + Application.dataPath + "/StreamingAssets/DataBase.db"; //Path to database.
        Debug.Log(Application.persistentDataPath);
#endif
        dbconn = (IDbConnection)new SqliteConnection(conn);
        dbconn.Open(); //Open connection to the database.
        ReadPlayerStats();
        ReadEnemyStats();

        
        //dbconn.Close();
        //dbconn = null;
    }	
	// Update is called once per frame
	void Update () {
        if (Input.GetKey("escape"))
            SaveToDataBase();
	}
    public void SaveToDataBase()
    {
        SavePlayerStats();
        SaveEnemyData();
        SaveHerosData();
        dbconn.Close();
        dbconn = null;
    }
    void SavePlayerStats()
    {
        IDbCommand dbcmd = dbconn.CreateCommand();
        string sqlQuery = "UPDATE User SET gold = " + UserSingleton.GetInstance().gold.ToString() +
                                ", stageNumber = " + UserSingleton.GetInstance().currentStage.ToString() +
                                ", numberOfHeros = " + UserSingleton.GetInstance().heroList.Count.ToString() +
                                " WHERE name = 'user'";
        dbcmd.CommandText = sqlQuery;
        IDataReader reader = dbcmd.ExecuteReader();

        reader.Close();
        reader = null;
        dbcmd.Dispose();
        dbcmd = null;
    }
    void SaveEnemyData()
    {
        IDbCommand dbcmd = dbconn.CreateCommand();
        string sqlQuery = "UPDATE EnemyStats SET MaxHealth = " + UserSingleton.GetInstance().enemy.Max_Health.ToString() +
                                ", LootGold = " + UserSingleton.GetInstance().enemy.lootGold.ToString() +
                                ", MobType = " + ((int)UserSingleton.GetInstance().enemy.mobType).ToString() +
                                " WHERE name = 'user'";
        dbcmd.CommandText = sqlQuery;
        IDataReader reader = dbcmd.ExecuteReader();

        reader.Close();
        reader = null;
        dbcmd.Dispose();
        dbcmd = null;
    }
    void SaveHerosData()
    {
        string sqlQuery = "delete from " + "UnitsData";
        IDbCommand dbcmd = dbconn.CreateCommand();
        dbcmd.CommandText = sqlQuery;
        IDataReader reader = dbcmd.ExecuteReader();

        for (int i = 0; i < UserSingleton.GetInstance().heroList.Count; ++i)
        {
            sqlQuery = "INSERT INTO UnitsData (attack,upgradeLevel,price,previousPrice,attackTime,name,type) VALUES ("
                + UserSingleton.GetInstance().heroList[i].attack.ToString() + ", "
                 + UserSingleton.GetInstance().heroList[i].upgradeLevel.ToString() + ", "
                  + UserSingleton.GetInstance().heroList[i].price.ToString() + ", "
                   + UserSingleton.GetInstance().heroList[i].previousPrice.ToString() + ", "
                    + ((int)UserSingleton.GetInstance().heroList[i].attackTime).ToString() + ", "
                     + "'" + UserSingleton.GetInstance().heroList[i].name + "'" + ", "
                      + "1" + ") ";
            reader.Close();
            reader = null;
            dbcmd.CommandText = sqlQuery;
            reader = dbcmd.ExecuteReader();
        }
        reader.Close();
        reader = null;
        dbcmd.Dispose();
        dbcmd = null;
    }
    void ReadPlayerStats()
    {
        string sqlQuery = "SELECT name,gold, stageNumber " + "FROM User " + "WHERE name = 'user'";
        IDbCommand dbcmd = dbconn.CreateCommand();
        dbcmd.CommandText = sqlQuery;
        IDataReader reader = dbcmd.ExecuteReader();
        if (reader.Read())
        {
            name = reader.GetString(0);
            int gold = reader.GetInt32(1);
            int stageNumber = reader.GetInt32(2);
            UserSingleton.GetInstance().SetData(gold, stageNumber);
            //Debug.Log("value= " + value + "  name =" + name + "  random =" + rand);
        }
        reader.Close();
        reader = null;
        dbcmd.Dispose();
        dbcmd = null;
    }
    void ReadEnemyStats()
    {
        string sqlQuery = "SELECT MaxHealth, LootGold, MobType " + "FROM EnemyStats" + " WHERE name = 'user'";
        IDbCommand dbcmd = dbconn.CreateCommand();
        dbcmd.CommandText = sqlQuery;
        IDataReader reader = dbcmd.ExecuteReader();
        if (reader.Read())
        {
            int health = reader.GetInt32(0);
            int Lootgold = reader.GetInt32(1);
            int MobType = reader.GetInt32(2);
            UserSingleton.GetInstance().SetEnemy(health, Lootgold, (Enemy.MobType)MobType);
            //Debug.Log("value= " + value + "  name =" + name + "  random =" + rand);
        }
        reader.Close();
        reader = null;
        dbcmd.Dispose();
        dbcmd = null;
    }

    void OnApplicationQuit()
    {
        SaveToDataBase();
    }
}

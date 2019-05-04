using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Characterの基本要素を持っているクラス
/// HPや攻撃力を持ったキャラを作る場合にはここから派生
/// </summary>
public class CharaController : MonoBehaviour
{ 
    
    public int HP;      //体力
    public int Attack;  //攻撃力
    protected CharaController()
    {

    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    }

    /// <summary>
    /// キャラクターへダメージを与える。HPは0が下限
    /// </summary>
    /// <param name="attack">ダメージ</param>
    protected void damage(int attack)
    {
        HP -= attack;
        if ( HP < 0)
        {
            HP = 0;
        }
    }

    protected void move()
    {

    }
}

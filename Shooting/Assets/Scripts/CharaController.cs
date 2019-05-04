using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Characterの基本要素を持っているクラス
/// HPや攻撃力を持ったキャラを作る場合にはここから派生
/// </summary>
public class CharaController : MonoBehaviour
{ 
    
    public int HP = 0;      //体力
    public int Attack = 0;  //攻撃力
    
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
    /// HPにダメージを与える。HPは0が下限
    /// </summary>
    /// <param name="damage">ダメージ</param>
    protected void damageHP(int damage)
    {
        HP -= damage;
        if ( HP < 0)
        {
            HP = 0;
        }
    }

    protected void move()
    {

    }
}

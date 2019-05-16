using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Characterの基本要素を持っているクラス
/// HPや攻撃力を持ったキャラを作る場合にはここから派生
/// </summary>
abstract public class CharaController : MonoBehaviour
{
    public enum Routine{
        CHARA_ROUTINE_WAIT,     //待機
        CHARA_ROUTINE_MOVE,     //移動
        CHARA_ROUTINE_SHOT,     //射撃
    }

    public Routine routine = Routine.CHARA_ROUTINE_WAIT; //キャラ用ルーチン
    public int HP = 0;      //体力
    public int Attack = 0;  //攻撃力

    //待機
    public virtual void wait() { }
    //移動
    public virtual void move() { }
    //射撃
    public virtual void shot() { }


    protected CharaController()
    {

    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    virtual protected void Update()
    {
        switch (routine)
        {
            case Routine.CHARA_ROUTINE_WAIT:
                wait();
                break;
            case Routine.CHARA_ROUTINE_MOVE:
                move();
                break;
            case Routine.CHARA_ROUTINE_SHOT:
                shot();
                break;
        }
    }

    /// <summary>
    /// HPにダメージを与える。HPは0が下限
    /// </summary>
    /// <param name="damage">ダメージ</param>
    public void damageHP(int damage)
    {
        HP -= damage;
        if (HP < 0)
        {
            HP = 0;
        }
    }
}

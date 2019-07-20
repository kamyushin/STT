using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Characterの基本要素を持っているクラス
/// HPや攻撃力を持ったキャラを作る場合にはここから派生
/// </summary>
abstract public class CharaController : MonoBehaviour
{
    public enum CharaRoutine
    {
        CHARA_ROUTINE_WAIT,     //待機
        CHARA_ROUTINE_MOVE,     //移動
        CHARA_ROUTINE_SHOT,     //射撃
    }

    [SerializeField]
    private CharaRoutine routine = CharaRoutine.CHARA_ROUTINE_WAIT;
    public CharaRoutine Routine {
        get { return routine; }
        set { routine = value; }
    }

    [SerializeField]
    private int hp = 0;
    public int HP {
        get { return hp; }
        protected set {hp = value; }
    }

    [SerializeField]
    private int attack = 1;
    public int Attack
    {
        get { return attack; }
        set { attack = value; }
    }

    //待機
    public virtual void wait() {}
    //行動
    public virtual void move() { }
    //射撃
    public virtual void shot() { }


    protected Animator animator;

    virtual protected void Start()
    {
        animator = GetComponent<Animator>();
    }
    // Update is called once per frame
    virtual protected void Update()
    {
        if (animator != null)
        {
            animator.SetInteger("Routine", (int)routine);
        }
        switch (routine)
        {
            case CharaRoutine.CHARA_ROUTINE_WAIT:
                wait();
                break;
            case CharaRoutine.CHARA_ROUTINE_MOVE:
                move();
                break;
            case CharaRoutine.CHARA_ROUTINE_SHOT:
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

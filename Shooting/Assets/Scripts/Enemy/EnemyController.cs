using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : CharaController
{

    [SerializeField]
    private EnemyRoutine e_routine = EnemyRoutine.ENEMY_ROUTINE_LINEAR;
    public EnemyRoutine ERoutine
    {
        get { return e_routine; }
        set { e_routine = value; }
    }


    [SerializeField]
    private float speed = 0.5f;
    public float Speed
    {
        get { return speed; }
        set { speed = value; }
    }

    //move中のみ実行される敵独自の行動ルーチン
    public enum EnemyRoutine{
        ENEMY_ROUTINE_LINEAR,           //直線移動
        ENEMY_ROUTINE_SEARCH,           //周り見まわし
        ENEMY_ROUTINE_TOPLAYER,         //プレイヤーに向かっていく
    }


    //視界認識系
    public float ViewAngle { get; protected set; } = 60;      //視野角(角度)
    public float ViewDistance { get; protected set; } = 10;   //視野範囲
    public GameObject TargetPlayer { get; set; } = null;         //targetとなるプレイヤー
    private Vector3 targetAngle = Vector3.zero;     //targetの方向

    

    //タイマー
    public float WaitTimer { get; protected set; } = 3.0f;      //行動関連共通タイマー
    public float LinearTime { get; protected set; } = 3.0f;     //直線移動のタイマー

    /// <summary>
    /// ターゲットを認識しているかどうか
    /// </summary>
    public bool IsTargetRecognize { get; set; }


    override protected void Start()
    {
        base.Start();
    }
    // Update is called once per frame
    override protected void Update()
    {
        base.Update();
        if (HP == 0)
        {
            Destroy(this.gameObject);
        }
    }

    public override void wait() { }
    //行動中
    public override void move()
    {
        if (animator != null)
        {
            animator.SetInteger("E_Routine", (int)e_routine);
        }
        switch (e_routine)
        {
            case EnemyRoutine.ENEMY_ROUTINE_LINEAR:
                linear();
                break;
            case EnemyRoutine.ENEMY_ROUTINE_SEARCH:
                search();
                break;
            case EnemyRoutine.ENEMY_ROUTINE_TOPLAYER:
                toPlayer();
                break;
        }
        
        if (TargetPlayer == null)
        {
            TargetPlayer = GameObject.Find("Player");
        }
        else
        {
            IsTargetRecognize = isTargetView();
        }

        if (IsTargetRecognize)
        {
            e_routine = EnemyRoutine.ENEMY_ROUTINE_TOPLAYER;
        }
        
    }
    
    /// <summary>
    /// 直線移動
    /// </summary>
    public virtual void linear()
    {
        transform.position += transform.forward * Speed * Time.deltaTime;
    }

    public virtual void search(){}

    /// <summary>
    /// ターゲット追従
    /// </summary>
    public virtual void toPlayer()
    {
        transform.forward = targetAngle;
        transform.position += transform.forward * Speed * Time.deltaTime;
    }

    /// <summary>
    /// targetが視界内にいるかどうか確認
    /// </summary>
    /// <returns></returns>
    protected bool isTargetView()
    {
        if (TargetPlayer == null)
        {
            return false;
        }

        targetAngle = TargetPlayer.transform.position - transform.position;
        float distance = targetAngle.magnitude;
        float angle = Vector3.Angle(targetAngle, transform.forward);
        if (angle < ViewAngle / 2 && distance < ViewDistance)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag != "enemy")
            damageHP(1);
    }
}

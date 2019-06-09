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
    private float walkSpeed = 0.5f;
    public float WalkSpeed
    {
        get { return walkSpeed; }
        set { walkSpeed = value; }
    }


    [SerializeField]
    private float changeForwardTime = 3.0f;
    public float ChangeForwardTime
    {
        get { return changeForwardTime; }
        set { changeForwardTime = value; }
    }

    //move中のみ実行される敵独自の行動ルーチン
    public enum EnemyRoutine{
        ENEMY_ROUTINE_LINEAR,           //直線移動
        ENEMY_ROUTINE_SEARCH,           //周り見まわし
        ENEMY_ROUTINE_TOPLAYER,         //プレイヤーに向かっていく
    }


    //視界認識系
    private float viewing_angle = 60;               //視野角(角度)
    private float viewing_distance = 10;             //視野範囲
    private GameObject targetPlayer = null;         //targetとなるプレイヤー
    private Vector3 targetAngle = Vector3.zero;     //targetの方向

    //サーチ角度
    private float searchAngle = 0;              //今見ている方向サーチ開始時の正面基準
    private float searchRad = 90;               //首の回転角度の限界
    private float searchRotateSpeed = 135;      //首の回転速度
    private bool rotationRight = false;         //右回転かどうか

    //行動遷移
    private float waitTimer;                        //タイマー
    private float linerTime = 3.0f;                 //直線移動時

    /// <summary>
    /// ターゲットを認識しているかどうか
    /// </summary>
    public bool IsTargetRecognize { get; set; }                
    
    // Update is called once per frame
    override protected void Update()
    {
        base.Update();
        if (HP == 0)
        {
            Destroy(this.gameObject);
        }
    }

    public override void wait() {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Routine = CharaRoutine.CHARA_ROUTINE_MOVE;
            e_routine = EnemyRoutine.ENEMY_ROUTINE_LINEAR;
            waitTimer = linerTime;
        }
        
    }
    public override void move()
    {
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

        if (targetPlayer == null)
        {
            targetPlayer = GameObject.Find("Player");
        }
        else
        {
            IsTargetRecognize = isTargetView();
        }

        if (IsTargetRecognize)
        {
            e_routine = EnemyRoutine.ENEMY_ROUTINE_TOPLAYER;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Routine = CharaRoutine.CHARA_ROUTINE_WAIT;
            targetPlayer = null;
            IsTargetRecognize = false;
        }
    }

    //直線移動
    public void linear()
    {
        transform.position += transform.forward * walkSpeed * Time.deltaTime;
        waitTimer -= Time.deltaTime;
        if ( waitTimer < 0)
        {
            waitTimer = Random.Range(1.0f, 3.0f);
            e_routine = EnemyRoutine.ENEMY_ROUTINE_SEARCH;
        }
    }

    public void search()
    {
        if (rotationRight)
        {
            searchAngle += searchRotateSpeed * Time.deltaTime;
            transform.Rotate(new Vector3(0, searchRotateSpeed * Time.deltaTime, 0));
        }
        else
        {
            searchAngle -= searchRotateSpeed * Time.deltaTime;
            transform.Rotate(new Vector3(0, -searchRotateSpeed * Time.deltaTime, 0));
        }

        if ( searchAngle < -searchRad / 2)
        {
            rotationRight = true;
        }else if(searchAngle > searchRad / 2)
        {
            rotationRight = false;
        }

        waitTimer -= Time.deltaTime;
        if ( waitTimer < 0)
        {
            waitTimer = linerTime;
            e_routine = EnemyRoutine.ENEMY_ROUTINE_LINEAR;
        }
    }
    public void toPlayer()
    {
        transform.forward = targetAngle;
        transform.position += transform.forward * walkSpeed * Time.deltaTime;
    }

    /// <summary>
    /// targetが視界内にいるかどうか確認
    /// </summary>
    /// <returns></returns>
    bool isTargetView()
    {
        if ( targetPlayer == null)
        {
            return false;
        }

        targetAngle = targetPlayer.transform.position - transform.position;
        float distance = targetAngle.magnitude;
        float angle = Vector3.Angle(targetAngle, transform.forward);
        if (angle < viewing_angle / 2 && distance < viewing_distance)
        {
            return true;
        }
        else
        {
            return false;
        }
    }



    IEnumerator RandomWalk()
    {
        while (true)
        {
            transform.forward = new Vector3(Random.Range(-1.0f, 1.0f), 0, Random.Range(-1.0f, 1.0f)).normalized;
            yield return new WaitForSeconds(ChangeForwardTime);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag != "enemy")
            damageHP(1);
    }
}

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


    private bool isTargetRecognize = false;                 //ターゲットを認識しているか

    private float viewing_angle = 60;               //視野角(角度)
    private float viewing_distance = 10;             //視野範囲

    private GameObject targetPlayer = null;         //targetとなるプレイヤー
    private Vector3 targetAngle = Vector3.zero;     //targetの方向


    

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
        }
        
    }
    public override void move()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Routine = CharaRoutine.CHARA_ROUTINE_WAIT;
            e_routine = EnemyRoutine.ENEMY_ROUTINE_LINEAR;
        }

        if (targetPlayer == null)
        {
            targetPlayer = GameObject.Find("Player");
        }
        else
        {
            targetAngle = targetPlayer.transform.position - transform.position;
            if (!isTargetRecognize)
            {
                float distance = targetAngle.magnitude;
                float angle = Vector3.Angle(targetAngle, transform.forward);
                if (angle <viewing_angle / 2  && distance < viewing_distance )
                {
                    isTargetRecognize = true;
                    e_routine = EnemyRoutine.ENEMY_ROUTINE_TOPLAYER;
                }
            }
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
    }

    //直線移動
    public void linear()
    {
        transform.position += transform.forward * walkSpeed * Time.deltaTime;
    }

    public void search()
    {
    }
    public void toPlayer()
    {
        transform.forward = targetAngle;
        transform.position += transform.forward * walkSpeed * Time.deltaTime;
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

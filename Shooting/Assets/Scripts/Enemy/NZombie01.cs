using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ノーマルゾンビ
/// 歩行→見まわし→歩行
/// 見つけたら追従
/// </summary>
public class NZombie01 : EnemyController
{

    //サーチ角度
    private float searchAngle = 0;                      //今見ている方向サーチ開始時の正面基準
    private float searchRad = 90;                       //首の回転角度の限界
    private float searchRotateSpeed = 135;              //首の回転速度
    private bool rotationRight = false;                 //右回転かどうか

    
    // Start is called before the first frame update
    override protected void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    override protected void Update()
    {
        base.Update();
    }

    public override void wait()
    {
        WaitTimer -= Time.deltaTime;
        if (WaitTimer < 0)
        {
            Routine = CharaRoutine.CHARA_ROUTINE_MOVE;
            ERoutine = EnemyRoutine.ENEMY_ROUTINE_LINEAR;
            WaitTimer = LinearTime;
        }
    }

    public override void linear()
    {
        base.linear();
        WaitTimer -= Time.deltaTime;
        if ( WaitTimer < 0)
        {
            ERoutine = EnemyRoutine.ENEMY_ROUTINE_SEARCH;
            WaitTimer = Random.Range(1.0f, 3.0f);
        }
    }

    public override void search()
    {
        WaitTimer -= Time.deltaTime;
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

        if (searchAngle < -searchRad / 2)
        {
            rotationRight = true;
        }
        else if (searchAngle > searchRad / 2)
        {
            rotationRight = false;
        }

        if (WaitTimer < 0)
        {
            ERoutine = EnemyRoutine.ENEMY_ROUTINE_LINEAR;
            WaitTimer = LinearTime;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoPattern : MonoBehaviour
{
    [Tooltip("使用するムーブポイントプレハブ")]
    public GameObject MovePoints = null;
    [Tooltip("回転速度(Degree)")]
    public float RotationSpeed = 30.0f;
    [Tooltip("移動速度")]
    public float MoveSpeed = 0.5f;

    private PatternMovement pm;
    // 現在向かっているポイントのID
    private int CurrentHeadingPointNo;

    // 現在使用しているコントロールデータ
    private PatternMovement.ControlData NowUsingControlData;
    // 現在のステートチェンジデータ
    private PatternMovement.StateChangeData NowUsingStateChangeData;

    private Vector3 StartPosition;
    private bool IsFinishPattern = false;
    //
    // Start is called before the first frame update
    void Start()
    {
        if (MovePoints != null)
        {
            pm = MovePoints.GetComponent<PatternMovement>();
            NowUsingStateChangeData.initializePatternTracking(gameObject);
            StartPosition = transform.position;
            if (pm.PointNum > 0)
            {
                Vector3 nextPoint = pm.PointList[0] + StartPosition - transform.position;
                NowUsingControlData.setRotateControlData(Vector3.SignedAngle(nextPoint, transform.forward, Vector3.up));
            }
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (pm == null || MovePoints == null)
        {
            return;
        }
        if (!IsFinishPattern)
        {
            if (!doPattern())
            {
                Debug.Log(true);
                NowUsingStateChangeData.initializePatternTracking(gameObject);
                IsFinishPattern = true;
            }
        }
        
    }

    bool doPattern()
    {
        if (NowUsingStateChangeData.CurrentControlID >= pm.PointNum)
        {
            return false;
        }

        if (NowUsingControlData.LimitHeadingChange)
        {
            rotationPattern();
        }
        if (NowUsingControlData.LimitPositionChange)
        {
            movePattern();
        }
        
        NowUsingStateChangeData.dHeading = Vector3.Angle(transform.forward, NowUsingStateChangeData.InitializeHeading);
        NowUsingStateChangeData.dPosition = Vector3.Distance(transform.position, NowUsingStateChangeData.InitializePosition);
        
        return true;
    }

    /// <summary>
    /// 回転終了したかどうか
    /// </summary>
    /// <returns></returns>
    bool rotationPattern()
    {
        int i = NowUsingStateChangeData.CurrentControlID;
        if (NowUsingStateChangeData.dHeading >= NowUsingControlData.dHeadingLimit)
        {
            NowUsingStateChangeData.initializePatternTracking(gameObject);
            NowUsingStateChangeData.CurrentControlID = i;
            NowUsingControlData.setMoveControlData(transform.position, pm.PointList[NowUsingStateChangeData.CurrentControlID] + StartPosition);
            return false;
        }
        else
        { 
            if (NowUsingControlData.LThrusterActive) { 
            transform.Rotate(new Vector3(0, RotationSpeed * Time.deltaTime, 0));
            }
            else if ( NowUsingControlData.RThrusterActive)
            {
                transform.Rotate(new Vector3(0, -RotationSpeed * Time.deltaTime, 0));
            }
            return true;
        }
    }
    
    /// <summary>
    /// 移動終了したかどうか
    /// </summary>
    /// <returns></returns>
    bool movePattern()
    {
        int i = NowUsingStateChangeData.CurrentControlID;
        if (NowUsingStateChangeData.dPosition >= NowUsingControlData.dPositionLimit)
        {
            i++;
            if(i == pm.PointNum)
            {
                NowUsingStateChangeData.CurrentControlID = i;
                return false ;
            }
            Vector3 nextPoint = pm.PointList[i] + StartPosition - transform.position;
            NowUsingStateChangeData.initializePatternTracking(gameObject);
            NowUsingStateChangeData.CurrentControlID = i;
            NowUsingControlData.setRotateControlData(Vector3.SignedAngle(nextPoint, transform.forward, Vector3.up));
            return false;
        }
        else
        {
            transform.position += transform.forward * MoveSpeed * Time.deltaTime;
            return true;
        }
    }

}

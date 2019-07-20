using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// パターンムーブメントのポイント群や構造体を定義するクラス
/// 使用する際はDoPatternをAdd Componentしてください。
/// </summary>
public class PatternMovement :MonoBehaviour
{
    #region パターンムーブメント構造体

    public struct ControlData
    {
        //
        public bool RThrusterActive;
        public bool LThrusterActive;
        public float dHeadingLimit;
        public float dPositionLimit;
        public bool LimitHeadingChange;
        public bool LimitPositionChange;

        public void setRotateControlData(float Angle)
        {
            Debug.Log(Angle);
            if(Angle > 0)
            {
                RThrusterActive = true;
                LThrusterActive = false;
            }
            else if (Angle < 0)
            {
                RThrusterActive = false;
                LThrusterActive = true;
            }

            dHeadingLimit = Mathf.Abs(Angle);
            dPositionLimit = 0;
            LimitHeadingChange = true;
            LimitPositionChange = false;
        }

        public void setMoveControlData(Vector3 start, Vector3 goal)
        {
            RThrusterActive = false;
            LThrusterActive = false;
            dHeadingLimit = 0;
            dPositionLimit = (goal - start).magnitude;
            LimitHeadingChange = false;
            LimitPositionChange = true;
        }
    };

    public struct StateChangeData
    {
        public Vector3 InitializeHeading;
        public Vector3 InitializePosition;
        public float dHeading;
        public float dPosition;
        public int CurrentControlID;
        /// <summary>
        /// 状態変化構造体(StateChangeData)を初期化する
        /// </summary>
        public void initializePatternTracking(GameObject initObject)
        {
            CurrentControlID = 0;
            dPosition = 0;
            dHeading = 0;

            InitializePosition = initObject.transform.position;
            InitializeHeading = initObject.transform.forward;
            InitializeHeading.Normalize();
        }
    };



    #endregion

    [Tooltip("基準となるゲームオブジェクト")]
    public GameObject BaseObject;

    [Tooltip("編集可能")]
    public bool IsEnablePointEdit;

    /// <summary>
    /// ローカルでの座標リスト
    /// </summary>
    [SerializeField,HideInInspector]
    public List<Vector3> PointList;

    /// <summary>
    /// ポイント数表示用
    /// </summary>
    [SerializeField]
    private int pointNum;
    public int PointNum {
        get
        { return pointNum; }
        set
        { pointNum = value; }
    }

    void Awake()
    {
        /*
        Debug.Log(pointNum);
        PointList.Clear();
        for (int i = 0; i < pointNum; i++) {
            PointList.Add(transform.GetChild(i).position);
        }
        */
    }

    void Start()
    {
    }
    void OnDisable()
    {
        /*
        Debug.Log("OnDisable");
        PointList.Clear();
        for (int i = 0; i < pointNum; i++)
        {
            PointList.Add(transform.GetChild(i).position);
        }
        */
    }
}

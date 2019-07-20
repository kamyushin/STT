using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
[CustomEditor(typeof(WayPoint))]
public class WayPointEditor : Editor
{
    private WayPoint _WayPoint_target;
    private int _SelectNo = -1;
    Transform parent;
    PatternMovement _target;
    private void Awake()
    {
        _WayPoint_target = target as WayPoint;
        _SelectNo = _WayPoint_target.PointNo;
        parent = _WayPoint_target.transform.parent;
        _target = parent.GetComponent<PatternMovement>();
    }
    
    void OnSceneGUI()
    {
        _target.PointList[_WayPoint_target.PointNo] = _WayPoint_target.transform.position - parent.position;
    }
    
        void OnDestroy()
    {   
        if (_WayPoint_target != null)
        {
            return;
        }
        //もし親がいなければ消去する
        if (parent == null)
        {
            return;
        }
        
        if (_SelectNo == -1)
        {
            return;
        }

        _target.PointList.RemoveAt(_SelectNo);
        
        if (parent.childCount > _SelectNo)
        {
            WayPoint[] Components = parent.GetComponentsInChildren<WayPoint>();
            for(int i = _SelectNo; i < parent.childCount; i++)
            {
                Components[i].PointNo--;
            }
        }
    }
}
#endif
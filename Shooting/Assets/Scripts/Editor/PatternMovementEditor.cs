using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditorInternal;
#if UNITY_EDITOR
using UnityEditor;
[CustomEditor(typeof(PatternMovement))]
public class PatternMovementEditor : Editor
{
    private PatternMovement _target;
    private bool PointListCountChange;

    ReorderableList reorderableList;

    SerializedProperty prop;

    Vector3 Offset = new Vector3(0, 0, 0);
    void OnEnable()
    {
        Tools.hidden = true;
        if (_target == null)
        {
            _target = target as PatternMovement;
        }

        if ( _target.BaseObject != null && !EditorApplication.isPlaying)
        {
            _target.gameObject.transform.position = _target.BaseObject.transform.position;
        }

        Offset = _target.gameObject.transform.position;

        prop = serializedObject.FindProperty("PointList");
        if (reorderableList == null)
        {
            reorderableList = new ReorderableList(serializedObject,prop);
        }

        
        reorderableList.drawElementCallback = (rect, index, isActive, isFocused) => {
            _target.PointList[index] = 
            EditorGUI.Vector3Field(rect, GUIContent.none, prop.GetArrayElementAtIndex(index).vector3Value);
        };

        // リストをInspecter上で変えた場合
        reorderableList.onChangedCallback = (list) =>
        {
            SceneView.RepaintAll();
            serializedObject.ApplyModifiedProperties();
        };
    }

    void OnSceneGUI()
    {
        if (_target.IsEnablePointEdit)
        {
            for (int i = 0; i < _target.PointList.Count; i++)
            {
                if (_target.transform.childCount == _target.PointList.Count) {
                    serializedObject.Update();
                    _target.PointList[i] = Handles.PositionHandle(_target.PointList[i] + Offset, Quaternion.identity) - Offset;
                    _target.transform.GetChild(i).position = _target.PointList[i] + Offset;
                    prop.GetArrayElementAtIndex(i).vector3Value = _target.PointList[i];
                    serializedObject.ApplyModifiedProperties();
                }
            }
        }
        Handles.color = Color.red;
        if (_target.PointList.Count > 0)
        {
            Handles.DrawLine(_target.transform.position, _target.PointList[0] + Offset);
            Vector3[] drawVec = _target.PointList.ToArray();
            for ( int i = 0;i< drawVec.Length; i++)
            {
                drawVec[i] += Offset;
            }
            Handles.DrawAAPolyLine(drawVec);
        }
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        reorderableList.DoLayoutList();

        if (_target.transform.childCount != _target.PointNum)
        {
            PointListCountChange = true;
        }
        else
        {
            PointListCountChange = false;
        }
        
        if (PointListCountChange)
        {
            changeChildObject();
        }


        _target.PointNum = _target.PointList.Count;


        serializedObject.ApplyModifiedProperties();


    }
    
    void OnDisable()
    {
        Tools.hidden = false;
    }

        private void changeChildObject()
    {
        if (_target.transform.childCount < _target.PointList.Count)
        {
            int n = _target.transform.childCount;

            for (int i = 0; i < _target.PointList.Count - _target.transform.childCount; i++)
            {
                GameObject obj = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                obj.name = "WayPoint";
                obj.transform.parent = _target.transform;
                obj.transform.localScale = new Vector3(0.1f,0.1f,0.1f);
                obj.AddComponent<WayPoint>();
                obj.GetComponent<WayPoint>().PointNo = n + i;
                obj.GetComponent<MeshRenderer>().sharedMaterial = (Material)EditorGUIUtility.Load("Point.mat");
                //obj.hideFlags = HideFlags.HideInHierarchy;
                DestroyImmediate(obj.GetComponent<SphereCollider>());
                
            }
        }else if (_target.transform.childCount > _target.PointNum)
        {
            for (int i = _target.transform.childCount-1; i >= _target.PointList.Count; i--) {
                DestroyImmediate(_target.transform.GetChild(i).gameObject);
            }
        }
    }
    
}
#endif
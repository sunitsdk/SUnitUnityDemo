/*
http://www.cgsoso.com/forum-211-1.html

CG搜搜 Unity3d 每日Unity3d插件免费更新 更有VIP资源！

CGSOSO 主打游戏开发，影视设计等CG资源素材。

插件如若商用，请务必官网购买！

daily assets update for try.

U should buy the asset from home store if u use it in your project!
*/

using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(UbhTimer))]
public class UbhTimerInspector : Editor
{
    float _OrgTimeScale;

    public override void OnInspectorGUI ()
    {
        serializedObject.Update();
        DrawProperties();
        serializedObject.ApplyModifiedProperties();
    }

    void DrawProperties ()
    {
        UbhTimer obj = target as UbhTimer;

        EditorGUILayout.Space();

        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("Pause UniBulletHell")) {
            if (Application.isPlaying && obj.gameObject.activeInHierarchy) {
                UbhTimer.Instance.Pause();
            }
        }
        if (GUILayout.Button("Resume UniBulletHell")) {
            if (Application.isPlaying && obj.gameObject.activeInHierarchy) {
                UbhTimer.Instance.Resume();
            }
        }
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("Pause TimeScale")) {
            if (Application.isPlaying && obj.gameObject.activeInHierarchy) {
                _OrgTimeScale = Time.timeScale;
                Time.timeScale = 0f;
            }
        }
        if (GUILayout.Button("Resume TimeScale")) {
            if (Application.isPlaying && obj.gameObject.activeInHierarchy && Time.timeScale == 0f) {
                Time.timeScale = _OrgTimeScale;
            }
        }
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.Space();

        DrawDefaultInspector();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace Lessons.Architecture.PM
{
    [CustomEditor(typeof(UserInfoSetter))]
    public sealed class UserInfoSetterEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            var installer = (UserInfoSetter)target;

            if (GUILayout.Button("Set username"))
            {
                installer.SetUsername();
            }
            if (GUILayout.Button("Set description"))
            {
                installer.SetDescription();
            }
            if (GUILayout.Button("Set icon"))
            {
                installer.SetIcon();
            }
            if (GUILayout.Button("Set all params"))
            {
                installer.SetAllUserParams();
            }
        }
    }
}
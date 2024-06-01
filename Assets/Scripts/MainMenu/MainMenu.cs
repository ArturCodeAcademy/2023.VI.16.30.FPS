using System;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private BuildScene[] _scenes;

    [Serializable]
    private struct BuildScene
    {
        public string Name;
        public int BuildIndex;
    }
}


#if UNITY_EDITOR

using UnityEditor;
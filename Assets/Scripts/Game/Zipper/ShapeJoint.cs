using System.Collections.Generic;
using Sirenix.OdinInspector;
using System.Collections;
using UnityEngine;
using System;

namespace ZipperGit.Game.ZipperBehaviour
{
    [Serializable]
    public class ShapeJoint
    {
        [TitleGroup("Parameters")]
        [FoldoutGroup("Parameters/Movement Range")] [SerializeField] private Vector2 _startingPos;
        [FoldoutGroup("Parameters/Movement Range")] [SerializeField] private Vector2 _endingPos;
        [FoldoutGroup("Parameters/Movement Speed")] [SerializeField] private AnimationCurve _moveCurve;

        public Vector2 startingPos => _startingPos;
        public Vector2 endingPos => _endingPos;
        public AnimationCurve moveCurve => _moveCurve;
    }
}
using System.Collections.Generic;
using Sirenix.OdinInspector;
using System.Collections;
using UnityEngine;
using System;

namespace Game.Zipper
{
    public class BasicZipper : MonoBehaviour
    {
        [Title("Zipper Parameters")]
        [SerializeField] private float _distance;
        [SerializeField] private float _progress;
        [SerializeField] private Vector2 _startPos;
        [SerializeField] private Vector2 _endPos;

        [Title("Top Shape Parameters")]
        [SerializeField] private int _desiredTopVertex;
        [SerializeField] private float _topStartValue;

        [Title("Bottom Shape Parameters")]
        [SerializeField] private int _desiredBottomVertex;
        [SerializeField] private float _bottomStartValue;

        [Title("References")]
        [SerializeField] private Shapes2D.Shape _topPart;
        [SerializeField] private Shapes2D.Shape _bottomPart;
        [SerializeField] private Transform _mainCanvas;

        #region Properties
        public Vector2 startPos => _startPos;
        public Vector2 endPos => _endPos;
        public float progress
        {
            get => _progress;
            set 
            {
                _progress = value;
                UpdateZipper((value/_distance)/500);
            }
        }

        #endregion

        private void Start()
        {
            _distance = Vector2.Distance(_startPos, _endPos);
        }

        private void UpdateZipper(float newProgress)
        {
            _topStartValue += newProgress;
            _topPart.settings.polyVertices[_desiredTopVertex].y = _topStartValue;
            _topPart.settings.polyVertices = _topPart.settings.polyVertices;

            _bottomStartValue -= newProgress;
            _bottomPart.settings.polyVertices[_desiredBottomVertex].y = _bottomStartValue;
            _bottomPart.settings.polyVertices = _bottomPart.settings.polyVertices;
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.yellow;

            if (_mainCanvas != null)
            {
                Gizmos.matrix = _mainCanvas.localToWorldMatrix;
                Gizmos.DrawSphere(_startPos, 20f);
                Gizmos.DrawSphere(_endPos, 20f);
            }
        }
    }
}
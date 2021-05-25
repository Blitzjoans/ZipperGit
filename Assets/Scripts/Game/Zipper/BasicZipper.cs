using System.Collections.Generic;
using Sirenix.OdinInspector;
using System.Collections;
using UnityEngine;

namespace Game.Zipper
{
    public class BasicZipper : MonoBehaviour
    {
        [Title("Parameters")]
        [SerializeField] private float _distance;
        [SerializeField] private float _progress;
        [SerializeField] private float _speed = 1f;
        [SerializeField] private Vector2 _startPos;
        [SerializeField] private Vector2 _endPos;

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
            set { _progress = value; }
        }
        #endregion

        private void Start()
        {
            _distance = Vector2.Distance(_startPos, _endPos);
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
using System.Collections.Generic;
using Sirenix.OdinInspector;
using System.Collections;
using UnityEngine;
using System;

namespace ZipperGit.Game.ZipperBehaviour
{
    public class BasicZipper : MonoBehaviour
    {
        [TitleGroup("Parameters")]
        [FoldoutGroup("Parameters/General Zipper Info")] [SerializeField] [ReadOnly] private float _distance;
        [FoldoutGroup("Parameters/General Zipper Info")] [SerializeField] [ReadOnly] [ProgressBar(0f, "_distance")] private float _progress;
        [InfoBox("Editor Gizmos for the handle movement range are displayed in YELLOW spheres. This parameters can ONLY be edited in play mode")]
        [FoldoutGroup("Parameters/Handle Movement")] [SerializeField] [DisableInPlayMode] private Vector2 _startPos;
        [FoldoutGroup("Parameters/Handle Movement")] [SerializeField] [DisableInPlayMode] private Vector2 _endPos;

        [InfoBox("Editor Gizmos for the top joints movement range are displayed in RED spheres. Start is SOLID color while the end is WIRED")]
        [FoldoutGroup("Parameters/Top Shape")] [SerializeField] private List<ShapeJoint> _topJoints = new List<ShapeJoint>();

        [InfoBox("Editor Gizmos for the top joints movement range are displayed in BLUE spheres. Start is SOLID color while the end is WIRED")]
        [FoldoutGroup("Parameters/Bottom Shape")] [SerializeField] private List<ShapeJoint> _bottomJoints = new List<ShapeJoint>();

        [TitleGroup("References")]
        [FoldoutGroup("References/Shapes")] [SerializeField] private Shapes2D.Shape _topPart;
        [FoldoutGroup("References/Shapes")] [SerializeField] private Shapes2D.Shape _bottomPart;
        [FoldoutGroup("References/Other")] [SerializeField] private Transform _mainCanvas;

        [TitleGroup("Gizmos")]
        [InfoBox("All Gizmos need for the MainCanvas not to be null in order to be visible")]
        [FoldoutGroup("Gizmos/Handle")] [SerializeField] private float _handleGizmosRadius;
        [InfoBox("All Gizmos need for the MainCanvas not to be null in order to be visible")]
        [FoldoutGroup("Gizmos/Joint")] [SerializeField] private float _jointGizmosRadius;

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

        private List<Vector2> _topPartJoints;
        private List<Vector2> _bottomPartJoints;

        #endregion

        private void Start()
        {
            _distance = Vector2.Distance(_startPos, _endPos);


            _topPartJoints = new List<Vector2>(_topPart.settings.polyVertices.Length);
            for (int i = 0; i < _topPart.settings.polyVertices.Length; i++)
            {
                _topPartJoints.Add(_topPart.settings.polyVertices[i]);
            }

            _bottomPartJoints = new List<Vector2>(_bottomPart.settings.polyVertices.Length);
            for (int i = 0; i < _bottomPart.settings.polyVertices.Length; i++)
            {
                _bottomPartJoints.Add(_bottomPart.settings.polyVertices[i]);
            }
        }

        private void UpdateZipper(float newProgress)
        {

        }

        #region Editor

        private void OnDrawGizmos()
        {
            // EDITOR VISUAL SUPPORT FOR HANDLE MOVEMENT RANGE
            if (_mainCanvas != null)
            {
                Gizmos.color = Color.yellow;
                Gizmos.matrix = _mainCanvas.localToWorldMatrix;
                Gizmos.DrawSphere(_startPos, _handleGizmosRadius);
                Gizmos.DrawSphere(_endPos, _handleGizmosRadius);
            }

            // EDITOR VISUAL SUPPORT FOR TOP JOINTS
            if (_topJoints.Count > 0 && _mainCanvas != null)
            {
                Gizmos.color = Color.red;
                Gizmos.matrix = _mainCanvas.localToWorldMatrix;
                for (int i = 0; i < _topJoints.Count; i++)
                {
                    Vector3 coordStartPosition = new Vector3((_topJoints[i].startingPos.x * _topPart.GetComponent<RectTransform>().rect.width) / 0.5f, 
                        (_topJoints[i].startingPos.y * _topPart.GetComponent<RectTransform>().rect.height) / 0.5f); 
                    Gizmos.DrawSphere(coordStartPosition , _jointGizmosRadius);

                    Vector3 coordEndPostion = new Vector3((_topJoints[i].endingPos.x * _topPart.GetComponent<RectTransform>().rect.width) / 0.5f,
                        (_topJoints[i].endingPos.y * _topPart.GetComponent<RectTransform>().rect.height) / 0.5f);
                    Gizmos.DrawWireSphere(coordEndPostion, _jointGizmosRadius);
                }
            }

            // EDITOR VISUAL SUPPORT FOR BOTTOM JOINTS
            if (_bottomJoints.Count > 0 && _mainCanvas != null)
            {
                Gizmos.color = Color.blue;
                Gizmos.matrix = _mainCanvas.localToWorldMatrix;
                for (int i = 0; i < _bottomJoints.Count; i++)
                {
                    Vector3 coordStartPosition = new Vector3((_bottomJoints[i].startingPos.x * _topPart.GetComponent<RectTransform>().rect.width) / 0.5f,
                        (_bottomJoints[i].startingPos.y * _topPart.GetComponent<RectTransform>().rect.height) / 0.5f);
                    Gizmos.DrawSphere(coordStartPosition, _jointGizmosRadius);

                    Vector3 coordEndPostion = new Vector3((_bottomJoints[i].endingPos.x * _topPart.GetComponent<RectTransform>().rect.width) / 0.5f,
                        (_bottomJoints[i].endingPos.y * _topPart.GetComponent<RectTransform>().rect.height) / 0.5f);
                    Gizmos.DrawWireSphere(coordEndPostion, _jointGizmosRadius);
                }
            }
        } 
        #endregion
    }
}
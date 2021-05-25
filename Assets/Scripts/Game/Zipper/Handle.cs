using System.Collections.Generic;
using UnityEngine.EventSystems;
using Sirenix.OdinInspector;
using System.Collections;
using UnityEngine;

namespace Game.Zipper
{
    public class Handle : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
    {
        [Title("References")]
        [SerializeField] private BasicZipper _basicZipper;

        private Vector2 _lastMousePos;
        private Vector2 _lastHandlePos;

        private void Start()
        {
            transform.localPosition = _basicZipper.startPos;
        }

        #region Handle Dragging
        public void OnBeginDrag(PointerEventData eventData)
        {
            _lastMousePos = eventData.position;
        }

        public void OnDrag(PointerEventData eventData)
        {
            Vector2 _currentMousePos = eventData.position;
            Vector2 _offset = _currentMousePos - _lastMousePos;
            RectTransform _rectTransform = GetComponent<RectTransform>();
            Vector3 _newPos = _rectTransform.position + new Vector3(_offset.x, _offset.y, transform.position.z);
            Vector3 _oldPos = _rectTransform.position;

            _rectTransform.position = new Vector3(_newPos.x, _rectTransform.transform.position.y, _rectTransform.transform.position.z);

            if (!IsRectTransformInsideSreen(_rectTransform) || transform.localPosition.x < _basicZipper.startPos.x || transform.localPosition.x > _basicZipper.endPos.x)
                _rectTransform.position = _oldPos;

            _lastMousePos = _currentMousePos;
            
            _basicZipper.progress = Vector2.Distance(transform.localPosition, _basicZipper.startPos);
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            _lastHandlePos = eventData.position;
        }

        private bool IsRectTransformInsideSreen(RectTransform rectTransform)
        {
            bool _isInside = false;
            Vector3[] _corners = new Vector3[4];
            rectTransform.GetWorldCorners(_corners);
            int _visibleCorners = 0;
            Rect _rect = new Rect(0, 0, Screen.width, Screen.height);

            foreach (Vector3 corner in _corners)
            {
                if (_rect.Contains(corner))
                    _visibleCorners++;
            }

            if (_visibleCorners == 4)
                _isInside = true;

            return _isInside;
        } 
        #endregion
    }
}
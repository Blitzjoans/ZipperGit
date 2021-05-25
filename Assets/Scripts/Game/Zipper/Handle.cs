using System.Collections.Generic;
using UnityEngine.EventSystems;
using Sirenix.OdinInspector;
using System.Collections;
using UnityEngine;

namespace Game.Zipper
{
    public class Handle : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        [Title("References")]
        [SerializeField] private BasicZipper _basicZipper;

        private void Start()
        {
            transform.localPosition = _basicZipper.startPos;
        }

        public void OnPointerDown(PointerEventData eventData)
        {

        }

        public void OnPointerUp(PointerEventData eventData)
        {

        }
    }
}
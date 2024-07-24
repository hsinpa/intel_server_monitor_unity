using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Unity.VisualScripting;
using UnityEditor;

namespace Hsinpa
{
    public class PreviewItemHandler : MonoBehaviour, IPointerMoveHandler, IPointerDownHandler, IPointerUpHandler
    {
        [SerializeField]
        private Transform preview_object;

        [SerializeField, Range(0.1f, 100)]
        private float rotate_strength = 1;

        private bool is_pressed = false;
        private Vector2 last_position;
        private Vector2 delta_position;

        private void Update()
        {
            if (is_pressed == false) return;


        }

        public void OnPointerDown(PointerEventData eventData)
        {
            last_position = eventData.position;
            delta_position = Vector2.zero;

            is_pressed = true;
        }

        public void OnPointerUp(PointerEventData eventData) { 
            is_pressed = false;
            Debug.Log(eventData.position);
        }

        public void OnPointerMove(PointerEventData eventData)
        {
            if (!is_pressed) return;

            delta_position = last_position - eventData.position;
            last_position = eventData.position;

            //Quaternion rotR = Quaternion.AngleAxis(delta_position.y * rotate_strength * Time.deltaTime, Vector3.forward);
            //Quaternion rotU = Quaternion.AngleAxis(delta_position.x * rotate_strength * Time.deltaTime, Vector3.up);

            float pitch = 0;
            float yaw = 0;

            if (Mathf.Abs(delta_position.y) > Mathf.Abs(delta_position.x))
            {
                pitch = delta_position.y;
            } else
            {
                yaw = delta_position.x;
            }

            // pitch
            preview_object.Rotate(-pitch * rotate_strength * Time.deltaTime, 0f, 0f, Space.World);

            // yaw
            preview_object.Rotate(0f, yaw * rotate_strength * Time.deltaTime, 0f, Space.World);
        }
    }
}

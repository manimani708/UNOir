using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CircleButton : Button, ICanvasRaycastFilter {
	
	float radius = 0.9f;

	public bool IsRaycastLocationValid(Vector2 sp, Camera eventCamera) {
		sp = Camera.main.ScreenToWorldPoint (sp);
		return Vector2.Distance (sp, transform.position) < radius;
	}
}


﻿/*
 * @author Valentin Simonov / http://va.lent.in/
 */

using UnityEngine;
using TouchScript.Gestures.TransformGestures;

/// <summary>
/// This component controls camera movement.
/// </summary>
public class TouchCameraController : MonoBehaviour
{
	public ScreenTransformGesture TwoFingerMoveGesture;
	public ScreenTransformGesture ManipulationGesture;
	public float PanSpeed = 200f;
	public float RotationSpeed = 200f;
	public float ZoomSpeed = 10f;

	private Transform pivot;
	private Transform cam;

	private void Awake()
	{
		pivot = transform.Find("CameraPivot");
		cam = transform.Find("CameraPivot/Main Camera");
	}

	private void OnEnable()
	{
		TwoFingerMoveGesture.Transformed += twoFingerTransformHandler;
		ManipulationGesture.Transformed += manipulationTransformedHandler;
	}

	private void OnDisable()
	{
		TwoFingerMoveGesture.Transformed -= twoFingerTransformHandler;
		ManipulationGesture.Transformed -= manipulationTransformedHandler;
	}

	private void manipulationTransformedHandler(object sender, System.EventArgs e)
	{
        //var rotation = Quaternion.Euler(ManipulationGesture.DeltaPosition.y/Screen.height*RotationSpeed,
        //	-ManipulationGesture.DeltaPosition.x/Screen.width*RotationSpeed,
        //	ManipulationGesture.DeltaRotation);
        //pivot.localRotation *= rotation;
        Quaternion rotation = Quaternion.Euler(0.0f,
        	-ManipulationGesture.DeltaPosition.x/Screen.width*RotationSpeed,
        	0.0f);
        pivot.localRotation *= rotation;
        cam.transform.localPosition += Vector3.forward*(ManipulationGesture.DeltaScale - 1f)*ZoomSpeed;
	}

	private void twoFingerTransformHandler(object sender, System.EventArgs e)
	{
		pivot.localPosition += pivot.rotation*TwoFingerMoveGesture.DeltaPosition*PanSpeed;
	}
}
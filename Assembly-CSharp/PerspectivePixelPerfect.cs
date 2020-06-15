﻿using System;
using UnityEngine;

// Token: 0x02000037 RID: 55
public class PerspectivePixelPerfect : MonoBehaviour
{
	// Token: 0x06000132 RID: 306 RVA: 0x00013038 File Offset: 0x00011238
	[ContextMenu("Execute")]
	private void Start()
	{
		Transform transform = base.transform;
		Camera main = Camera.main;
		float nearClipPlane = main.nearClipPlane;
		float farClipPlane = main.farClipPlane;
		float num = Mathf.Lerp(nearClipPlane, farClipPlane, this.bias);
		float fieldOfView = main.fieldOfView;
		float num2 = Mathf.Tan(0.0174532924f * fieldOfView * 0.5f) * num;
		transform.localPosition = new Vector3(0f, 0f, num);
		transform.localScale = new Vector3(num2, num2, 1f);
	}

	// Token: 0x040002C4 RID: 708
	[Tooltip("Bias is a value above 0 that determines how far offset the object will be from the near clip, in percent (near to far clip)")]
	public float bias = 0.001f;
}

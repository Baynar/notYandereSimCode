﻿using System;
using UnityEngine;

// Token: 0x02000061 RID: 97
[ExecuteInEditMode]
[AddComponentMenu("NGUI/Interaction/NGUI Scroll Bar")]
public class UIScrollBar : UISlider
{
	// Token: 0x17000043 RID: 67
	// (get) Token: 0x060002D3 RID: 723 RVA: 0x0001C4BA File Offset: 0x0001A6BA
	// (set) Token: 0x060002D4 RID: 724 RVA: 0x0001C4C2 File Offset: 0x0001A6C2
	[Obsolete("Use 'value' instead")]
	public float scrollValue
	{
		get
		{
			return base.value;
		}
		set
		{
			base.value = value;
		}
	}

	// Token: 0x17000044 RID: 68
	// (get) Token: 0x060002D5 RID: 725 RVA: 0x0001C4CB File Offset: 0x0001A6CB
	// (set) Token: 0x060002D6 RID: 726 RVA: 0x0001C4D4 File Offset: 0x0001A6D4
	public float barSize
	{
		get
		{
			return this.mSize;
		}
		set
		{
			float num = Mathf.Clamp01(value);
			if (this.mSize != num)
			{
				this.mSize = num;
				this.mIsDirty = true;
				if (NGUITools.GetActive(this))
				{
					if (UIProgressBar.current == null && this.onChange != null)
					{
						UIProgressBar.current = this;
						EventDelegate.Execute(this.onChange);
						UIProgressBar.current = null;
					}
					this.ForceUpdate();
				}
			}
		}
	}

	// Token: 0x060002D7 RID: 727 RVA: 0x0001C53C File Offset: 0x0001A73C
	protected override void Upgrade()
	{
		if (this.mDir != UIScrollBar.Direction.Upgraded)
		{
			this.mValue = this.mScroll;
			if (this.mDir == UIScrollBar.Direction.Horizontal)
			{
				this.mFill = (this.mInverted ? UIProgressBar.FillDirection.RightToLeft : UIProgressBar.FillDirection.LeftToRight);
			}
			else
			{
				this.mFill = (this.mInverted ? UIProgressBar.FillDirection.BottomToTop : UIProgressBar.FillDirection.TopToBottom);
			}
			this.mDir = UIScrollBar.Direction.Upgraded;
		}
	}

	// Token: 0x060002D8 RID: 728 RVA: 0x0001C594 File Offset: 0x0001A794
	protected override void OnStart()
	{
		base.OnStart();
		if (this.mFG != null && this.mFG.gameObject != base.gameObject)
		{
			if (!(this.mFG.GetComponent<Collider>() != null) && !(this.mFG.GetComponent<Collider2D>() != null))
			{
				return;
			}
			UIEventListener uieventListener = UIEventListener.Get(this.mFG.gameObject);
			uieventListener.onPress = (UIEventListener.BoolDelegate)Delegate.Combine(uieventListener.onPress, new UIEventListener.BoolDelegate(base.OnPressForeground));
			uieventListener.onDrag = (UIEventListener.VectorDelegate)Delegate.Combine(uieventListener.onDrag, new UIEventListener.VectorDelegate(base.OnDragForeground));
			this.mFG.autoResizeBoxCollider = true;
		}
	}

	// Token: 0x060002D9 RID: 729 RVA: 0x0001C65C File Offset: 0x0001A85C
	protected override float LocalToValue(Vector2 localPos)
	{
		if (!(this.mFG != null))
		{
			return base.LocalToValue(localPos);
		}
		float num = Mathf.Clamp01(this.mSize) * 0.5f;
		float num2 = num;
		float num3 = 1f - num;
		Vector3[] localCorners = this.mFG.localCorners;
		if (base.isHorizontal)
		{
			num2 = Mathf.Lerp(localCorners[0].x, localCorners[2].x, num2);
			num3 = Mathf.Lerp(localCorners[0].x, localCorners[2].x, num3);
			float num4 = num3 - num2;
			if (num4 == 0f)
			{
				return base.value;
			}
			if (!base.isInverted)
			{
				return (localPos.x - num2) / num4;
			}
			return (num3 - localPos.x) / num4;
		}
		else
		{
			num2 = Mathf.Lerp(localCorners[0].y, localCorners[1].y, num2);
			num3 = Mathf.Lerp(localCorners[3].y, localCorners[2].y, num3);
			float num5 = num3 - num2;
			if (num5 == 0f)
			{
				return base.value;
			}
			if (!base.isInverted)
			{
				return (localPos.y - num2) / num5;
			}
			return (num3 - localPos.y) / num5;
		}
	}

	// Token: 0x060002DA RID: 730 RVA: 0x0001C798 File Offset: 0x0001A998
	public override void ForceUpdate()
	{
		if (this.mFG != null)
		{
			this.mIsDirty = false;
			float num = Mathf.Clamp01(this.mSize) * 0.5f;
			float num2 = Mathf.Lerp(num, 1f - num, base.value);
			float num3 = num2 - num;
			float num4 = num2 + num;
			if (base.isHorizontal)
			{
				this.mFG.drawRegion = (base.isInverted ? new Vector4(1f - num4, 0f, 1f - num3, 1f) : new Vector4(num3, 0f, num4, 1f));
			}
			else
			{
				this.mFG.drawRegion = (base.isInverted ? new Vector4(0f, 1f - num4, 1f, 1f - num3) : new Vector4(0f, num3, 1f, num4));
			}
			if (this.thumb != null)
			{
				Vector4 drawingDimensions = this.mFG.drawingDimensions;
				Vector3 position = new Vector3(Mathf.Lerp(drawingDimensions.x, drawingDimensions.z, 0.5f), Mathf.Lerp(drawingDimensions.y, drawingDimensions.w, 0.5f));
				base.SetThumbPosition(this.mFG.cachedTransform.TransformPoint(position));
				return;
			}
		}
		else
		{
			base.ForceUpdate();
		}
	}

	// Token: 0x04000429 RID: 1065
	[HideInInspector]
	[SerializeField]
	protected float mSize = 1f;

	// Token: 0x0400042A RID: 1066
	[HideInInspector]
	[SerializeField]
	private float mScroll;

	// Token: 0x0400042B RID: 1067
	[HideInInspector]
	[SerializeField]
	private UIScrollBar.Direction mDir = UIScrollBar.Direction.Upgraded;

	// Token: 0x0200063A RID: 1594
	private enum Direction
	{
		// Token: 0x040045AC RID: 17836
		Horizontal,
		// Token: 0x040045AD RID: 17837
		Vertical,
		// Token: 0x040045AE RID: 17838
		Upgraded
	}
}

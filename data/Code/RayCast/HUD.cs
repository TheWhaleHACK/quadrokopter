/* Copyright (C) 2005-2023, UNIGINE. All rights reserved.
*
* This file is a part of the UNIGINE 2 SDK.
*
* Your use and / or redistribution of this software in source and / or
* binary form, with or without modification, is subject to: (i) your
* ongoing acceptance of and compliance with the terms and conditions of
* the UNIGINE License Agreement; and (ii) your inclusion of this notice
* in any version of this software that you use or redistribute.
* A copy of the UNIGINE License Agreement is available by contacting
* UNIGINE. at http://unigine.com/
*/

using System;
using System.Collections;
using System.Collections.Generic;
using Unigine;

[Component(PropertyGuid = "2e0134a16e3ddfb3958c12038541e780e2096ce0")]
public class HUD : Component
{
	public AssetLink crosshairImage = null;
	public int crosshairSize = 16;

	private WidgetSprite sprite = null;
	private Gui screenGui = null;
	private ivec2 prev_size = new ivec2(0, 0);


	private void Init()
	{
		screenGui = Gui.GetCurrent();

		sprite = new WidgetSprite(screenGui, crosshairImage.AbsolutePath);
		sprite.Width = crosshairSize;
		sprite.Height = crosshairSize;
		
		screenGui.AddChild(sprite, Gui.ALIGN_CENTER | Gui.ALIGN_OVERLAP);

		sprite.Lifetime = Widget.LIFETIME.WORLD;
	}

	private void Update()
	{
		ivec2 new_size = screenGui.Size;
		if (prev_size != new_size)
		{
			screenGui.RemoveChild(sprite);
			screenGui.AddChild(sprite, Gui.ALIGN_CENTER | Gui.ALIGN_OVERLAP);
		}
		prev_size = new_size;
	}
}
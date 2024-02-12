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

[Component(PropertyGuid = "b2fd62fb5e4dfd419db9dd2dfd67aaee8f746a52")]
public class SelectObject : Component
{
	public CameraCast cameraCast = null;
	private Unigine.Object selectedObject;
	private WidgetCanvas textOfObject;
	private Gui gui;
	private int x, y;

	private void addText(string text){
		textOfObject.SetTextText(x, text);
	}
	
	private void Init()
	{
		gui = Gui.GetCurrent();
		textOfObject = new WidgetCanvas(gui);
		textOfObject.Clear();

		x = textOfObject.AddText(1);

		vec4 color = new vec4(x, 1, 1, 0.5);
		textOfObject.SetTextColor(x, color);
		textOfObject.SetTextSize(x, 30);
		textOfObject.SetTextPosition(x, new vec2(10, 10));
		
		y = textOfObject.AddPolygon(0);
		textOfObject.SetPolygonColor(y, new vec4(0, 0, 0, 0.5));

		textOfObject.AddPolygonPoint(y, new vec3(0, 0, 0));
		textOfObject.AddPolygonPoint(y, new vec3(600, 0, 0));
		textOfObject.AddPolygonPoint(y, new vec3(600, 100, 0));
		textOfObject.AddPolygonPoint(y, new vec3(0, 100, 0));

		gui.AddChild(textOfObject, Gui.ALIGN_EXPAND);
		textOfObject.Hidden = true;
	}
	
	private void Update()
	{
		selectedObject = cameraCast.GetObject();
		if (selectedObject != null && selectedObject.RootNode.Name == "Drone")
		{
			Visualizer.RenderObjectSurfaceBoundBox(selectedObject, 0, vec4.BLUE, 0.05f);

			textOfObject.Hidden = false;
			string info = $"{selectedObject.Name}";
			addText(info);

			Visualizer.RenderMessage3D(selectedObject.WorldPosition + new vec3(0, 0, 0.05f), 0, info, vec4.GREEN, 0, 20,0);
		}
	}
}
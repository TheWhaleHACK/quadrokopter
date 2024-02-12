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

[Component(PropertyGuid = "698ba7f2fee2a9c0d8c971d8312cbadcc4607f0b")]
public class DrugObject : Component
{
	public CameraCast cameraCast = null;
	private Unigine.Object drugObject;
	private Unigine.Object mainObject;
	public MouseHandler mouseHandler = null;
	private dmat4 transform;
	private dmat4 initialTransform;
	private bool flag;
	
	private void Init()
	{
		// write here code to be called on component initialization
		flag = false;
	}
	
	private void Update()
	{
		drugObject = cameraCast.GetObject();
		if (flag)
        {
			mainObject.WorldTransform = cameraCast.shootingCamera.OldWorldTransform * transform;
        }
		string mouse = mouseHandler.IsMousePressed();
		if (drugObject != null && drugObject.RootNode.Name == "Drone")
		{
			if (mouse == "LeftPress")
			{
				if (!flag && drugObject != null && drugObject.RootNode.Name == "Drone")
				{
					mainObject = drugObject;
					transform = cameraCast.shootingCamera.IWorldTransform * drugObject.WorldTransform;
					initialTransform = mainObject.WorldTransform;
				}
				flag = true;
			}
		}
		if (mouse == "LeftUp")
		{
			flag = false;
		}
		if(Input.IsKeyPressed(Input.KEY.X)){
			mainObject.WorldTransform = initialTransform;
		}
	}
}
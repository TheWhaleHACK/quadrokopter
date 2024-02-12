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

[Component(PropertyGuid = "626faa9338f4a690fa702b4244415660adab0e56")]
public class MouseHandler : Component
{
	public string IsMousePressed()
	{
		if (Input.IsMouseButtonPressed(Input.MOUSE_BUTTON.LEFT))
			return "LeftPress";
		if (Input.IsMouseButtonDown(Input.MOUSE_BUTTON.LEFT))
			return "LeftDown";
		if (Input.IsMouseButtonUp(Input.MOUSE_BUTTON.LEFT))
			return "LeftUp";
		return "None";
	}
}
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

[Component(PropertyGuid = "9b39ebd3c0538a27528311980e7637abfd87f887")]
public class CameraCast : Component
{
	public PlayerDummy shootingCamera = null;

	[ParameterMask(MaskType = ParameterMaskAttribute.TYPE.INTERSECTION)]
	public int mask = ~0;
	private Unigine.dvec3 p0, p1;

	private void Init()
	{
		Visualizer.Enabled = true;
	}
	
	private void Update()
	{

	}

	public Unigine.Object GetObject()
	{
		p0 = shootingCamera.WorldPosition;
		p1 = shootingCamera.WorldPosition + shootingCamera.GetWorldDirection() * 100;

		WorldIntersectionNormal hitInfo = new WorldIntersectionNormal();
		Unigine.Object hitObject = World.GetIntersection(p0, p1, mask, hitInfo);

		return hitObject;
	}
}
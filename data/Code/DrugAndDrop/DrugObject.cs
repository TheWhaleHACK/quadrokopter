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
	private Unigine.Object clonedObject = null; // Для хранения 
	private Unigine.Object drugObject;
	private Unigine.Object mainObject;
	public MouseHandler mouseHandler = null;
	private dmat4 transform;
	private dmat4 initialTransform;
	private float thresholdDistance = 0.1f;
	private bool flag;

	// Словарь для хранения исходных позиций деталей
	// private Dictionary<Unigine.Object, dvec3> initialTransforms = new Dictionary<Unigine.Object, dvec3>();
	private Dictionary<Unigine.Object, (dmat4, dvec3)> initialTransforms = new Dictionary<Unigine.Object, (dmat4, dvec3)>();
	private Dictionary<Unigine.Object, (dmat4, dvec3)> initialTransformsDrone2 = new Dictionary<Unigine.Object, (dmat4, dvec3)>();

	private void Init()
	{
		// write here code to be called on component initialization
		flag = false;
		mainObject = node as Unigine.Object;
		//flag2 = false;
		SetOutline(1, mainObject);

// Находим 'Drone2' в сцене
    // Node drone2Node = World.GetNodeByName("Drone2");
    // if (drone2Node != null)
    // {
    //     // Если найден, добавляем его исходное состояние в словарь
    //     Unigine.Object drone2Object = drone2Node as Unigine.Object;
    //     if (drone2Object != null)
    //     {
    //         initialTransformsDrone2.Add(drone2Object, (drone2Object.WorldTransform, drone2Object.WorldPosition));
    //     }
    // }

	}
	private void SetOutline(int enabled, Unigine.Object gameObject)
	{
		for (int i = 0; i < gameObject.NumSurfaces; i++)
			gameObject.SetMaterialState("auxiliary", enabled, i);
	}


	private void Update()
	{
		
 	// Поиск объекта 'Drone2' в сцене
    Node drone2Node = World.GetNodeByName("Drone2");
    if (drone2Node != null)
    {
        // Если объект найден, делаем его невидимым
        drone2Node.Enabled = false;
    }
	



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
				SetOutline(0, drugObject);
				if (!flag && drugObject != null && drugObject.RootNode.Name == "Drone")
				{
					mainObject = drugObject;
					transform = cameraCast.shootingCamera.IWorldTransform * drugObject.WorldTransform;
					// Проверяем, есть ли у этой детали исходная позиция, если нет, то добавляем ее в словарь
					if (!initialTransforms.ContainsKey(mainObject))
					{
						initialTransforms.Add(mainObject, (mainObject.WorldTransform, mainObject.WorldPosition));
					}		
				}

				(dmat4, dvec3) initialValues;
				initialTransforms.TryGetValue(mainObject, out initialValues);
				double distancee = (mainObject.WorldPosition - initialValues.Item2).Length;

				if (distancee < thresholdDistance)
				{

					drone2Node.Enabled = true;
					SetOutline(1, drugObject);
				}
				flag = true;
				
			}
		
			if (mouse == "LeftUp")
			{
				
				flag = false;
				SetOutline(0, mainObject);
				drone2Node.Enabled = false;
				// Проверяем, если текущее расстояние между объектом и его исходным положением меньше порога, то возвращаем его на исходное место;
				(dmat4, dvec3) initialValues;
				initialTransforms.TryGetValue(mainObject, out initialValues);
				double distancee = (mainObject.WorldPosition - initialValues.Item2).Length;
				if (distancee < thresholdDistance)
				{
					mainObject.WorldTransform = initialValues.Item1;
					SetOutline(0, mainObject);
					
					
				}
				//SetOutline(0);
			}
		}

		


	}
}
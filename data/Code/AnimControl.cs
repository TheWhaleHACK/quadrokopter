using System;
using System.Collections;
using System.Collections.Generic;
using Unigine;

[Component(PropertyGuid = "95929a2e4ccd1bb59090799003447faf29e02942")]
public class AnimControl : Component
{
	[ShowInEditor][ParameterAsset(Filter =".anim")]
	private string EngineAnim;

    [ShowInEditor]
    public ObjectMeshSkinned MainEngine; // Основной объект анимации

	private void Init()
	{

        //MainEngine = node as ObjectMeshSkinned;
        MainEngine = (ObjectMeshSkinned)Node.GetNode(989496235); //Айдишник ObjectMeshSkinned (основной объект анимации)
		MainEngine.NumLayers = 1; // Устанавливаем количество слоев анимации в 1


        int _def = MainEngine.AddAnimation(EngineAnim); // Добавляем анимацию к объекту и получаем идентификатор анимации


		MainEngine.SetLayer(0, true, 1); // Устанавливаем параметры для слоя анимации

		MainEngine.SetAnimation(0, _def); // Устанавливаем анимацию для слоя
		
	}
	
	private void Update()
	{
		if (Input.IsKeyDown(Input.KEY.T)) {
			Unigine.Console.WriteLine(MainEngine.GetFrame(0));// 
		}
		if (Input.IsKeyPressed(Input.KEY.E) && MainEngine.GetFrame(0) < 376) // Проверяем нажатие клавиши E и текущий кадр меньше 376 //140
		{
            MainEngine.SetFrame(0, MainEngine.GetFrame(0) + Game.IFps *	30, 0, 380); // Увеличиваем текущий кадр на значение, соответствующее 30 кадрам в секунду
        }
		else if(Input.IsKeyPressed(Input.KEY.Q) && MainEngine.GetFrame(0) > 0) // Проверяем нажатие клавиши Q и текущий кадр больше 0
		{
			// Уменьшаем текущий кадр на значение, соответствующее 30 кадрам в секунду
            MainEngine.SetFrame(0, MainEngine.GetFrame(0) - Game.IFps * 30, 0, 380);
        }

		//Unigine.Console.WriteLine(MainEngine.GetFrame(0));
        // write here code to be called before updating each render frame

    }
}
﻿using DigitalGameStore.Model;
using DigitalGameStore.Tools;
namespace DigitalGameStore.Views;

public class BrowseView
{
	public void LoadingScreen(int time)
	{
		Func.WriteOutput("Loading.");
		Thread.Sleep(time);
		Func.Clear();
		Func.WriteOutput("Loading..");
		Thread.Sleep(time);
		Func.Clear();
		Func.WriteOutput("Loading...");
	}
}

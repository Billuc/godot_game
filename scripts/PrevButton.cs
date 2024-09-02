using System;
using FYBF;
using Godot;

public partial class PrevButton : Button
{
	private SceneTransitionRect _transitionRect;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_transitionRect = GetNode<SceneTransitionRect>("/root/Control/SceneTransitionRect");
		Pressed += OnPressed;
	}

	private async void OnPressed()
	{
		GameState.GetInstance().SetTransportationType(String.Empty);
		await _transitionRect.TransitionTo("res://scenes/transportation_choice.tscn");
	}
}

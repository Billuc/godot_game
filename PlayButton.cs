using Godot;
using System;
using System.Threading.Tasks;

public partial class PlayButton : Button
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
		await _transitionRect.TransitionTo("res://place.tscn");
	}
}

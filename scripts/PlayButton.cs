using FYBF;
using Godot;
using System;
using System.Threading.Tasks;

public partial class PlayButton : Button
{
	private SceneTransitionRect _transitionRect;
	private GameState gameState;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		gameState = GameState.GetInstance();
		_transitionRect = GetNode<SceneTransitionRect>("/root/Control/SceneTransitionRect");
		Pressed += OnPressed;
	}
	
	private async void OnPressed()
	{
		gameState.SetCurrentPlace("Costa Rica");
		await _transitionRect.TransitionTo("res://scenes/place.tscn");
	}
}

using Godot;
using System;
using System.Threading.Tasks;

public partial class NextButton : Button
{
	private SceneTransitionRect _transitionRect;
	private AnimationPlayer _animPlayer;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_animPlayer = GetNode<AnimationPlayer>("AnimationPlayer");
		_transitionRect = GetNode<SceneTransitionRect>("/root/Control/SceneTransitionRect");
		Modulate = new Color(1, 1, 1, 0);
	}

	private async void OnPressed()
	{
		await _transitionRect.TransitionTo("res://scenes/transportation_choice.tscn");
	}

	public async Task ShowWithAnimation()
	{
		Visible = true;
		_animPlayer.Play("show");
		await ToSignal(_animPlayer, "animation_finished");
		Pressed += OnPressed;
	}
}

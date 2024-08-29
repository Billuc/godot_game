using FYBF;
using Godot;
using System;
using System.Threading.Tasks;

public partial class TransportationAnimationScene : Control
{
	double timeElapsed = 0;

	GameState _gameState;
	AnimationPlayer _animPlayer;
	SceneTransitionRect _transitionPlayer;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_gameState = GameState.GetInstance();
		_animPlayer = GetNode<AnimationPlayer>("Panel/Ximena/AnimationPlayer");
		_transitionPlayer = GetNode<SceneTransitionRect>("SceneTransitionRect");

		TextureRect transportationImage = GetNode<TextureRect>("Panel/Transportation");
		string transportationType = _gameState.GetTransportationTypeForAnimation();

		if (transportationType == "Pied")
		{
			transportationImage.Texture = new Texture2D();
		}
		else
		{
			transportationImage.Texture = GD.Load<Texture2D>("res://assets/" + transportationType + ".png");
		}
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		timeElapsed += delta;

		if (timeElapsed > 0.5)
		{
			PlayAnimationAndChangeScene();
		}
	}

	private async void PlayAnimationAndChangeScene()
	{
		string transportationType = _gameState.GetTransportationTypeForAnimation();

		_animPlayer.Play("enter_" + transportationType.ToLower());
		await ToSignal(_animPlayer, "animation_finished");

		await Task.Delay(500);

		if (string.IsNullOrEmpty(_gameState.GetCurrentCharacter().Character))
		{
			await _transitionPlayer.TransitionTo("res://transportation_choice.tscn");
		}
		else
		{
			await _transitionPlayer.TransitionTo("res://place.tscn");
		}
	}
}

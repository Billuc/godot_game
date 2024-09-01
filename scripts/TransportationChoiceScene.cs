using FYBF;
using Godot;
using System;

public partial class TransportationChoiceScene : Control
{
	private GameState gameState;
	private Node choiceContainer;
	private SceneTransitionRect transitionRect;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		gameState = GameState.GetInstance();

		Label placeName = GetNode<Label>("Panel/PlaceName");
		choiceContainer = GetNode("Panel/VBoxContainer");
		transitionRect = GetNode<SceneTransitionRect>("/root/Control/SceneTransitionRect");

		string[] transportationOptions = gameState.GetCurrentTransportationTypes();

		placeName.Text = gameState.GetCurrentPlaceName();

		foreach (string option in transportationOptions) {
			CreateOptionButton(option);
		}
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	private void CreateOptionButton(string transportation) {
		Button button = new Button();
		button.Text = gameState.FormatTransportation(transportation);
		button.Icon = GD.Load<Texture2D>("res://assets/" + transportation + ".png");
		button.Theme = GD.Load<Theme>("res://themes/transportation_button_theme.tres");
		button.SizeFlagsHorizontal = SizeFlags.ShrinkCenter;

		button.Pressed += async () => {
			gameState.SetTransportationType(transportation);
			await transitionRect.TransitionTo("res://scenes/destination_choice.tscn");
		};

		choiceContainer.AddChild(button);
	}
}

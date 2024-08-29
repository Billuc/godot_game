using FYBF;
using Godot;
using System;

public partial class DestinationChoiceScene : Control
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
		Label transportationLabel = GetNode<Label>("Panel/VBoxContainer/TransportationChoice");
		transitionRect = GetNode<SceneTransitionRect>("/root/Control/SceneTransitionRect");

		Connection[] connectionOptions = gameState.GetCurrentConnections();

		placeName.Text = gameState.GetCurrentPlaceName();
		transportationLabel.Text += gameState.GetSelectedTransportationType();

		foreach (Connection option in connectionOptions)
		{
			CreateOptionButton(option);
		}
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	private void CreateOptionButton(Connection connection)
	{
		Button button = new Button();
		button.Text = connection.Indication;
		button.Theme = GD.Load<Theme>("res://button_theme.tres");

		button.Pressed += async () =>
		{
			TransitionData transitionData = gameState.SetCurrentPlace(connection.To);

			if (transitionData.TransportationChanged)
			{
				await transitionRect.TransitionTo("res://transportation_animation.tscn");
			}
			else
			{
				if (string.IsNullOrEmpty(gameState.GetCurrentCharacter().Character))
				{
					await transitionRect.TransitionTo("res://transportation_choice.tscn");
				}
				else
				{
					await transitionRect.TransitionTo("res://place.tscn");
				}
			}
		};

		choiceContainer.AddChild(button);
	}
}

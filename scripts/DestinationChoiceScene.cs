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
		button.Icon = GetIconFromIndication(connection.Indication);
		button.Theme = GD.Load<Theme>("res://scenes/transportation_button_theme.tres");
		button.SizeFlagsHorizontal = SizeFlags.ShrinkCenter;

		button.Pressed += async () =>
		{
			TransitionData transitionData = gameState.SetCurrentPlace(connection.To);

			if (transitionData.TransportationChanged)
			{
				await transitionRect.TransitionTo("res://scenes/transportation_animation.tscn");
			}
			else
			{
				if (string.IsNullOrEmpty(gameState.GetCurrentCharacter().Character))
				{
					await transitionRect.TransitionTo("res://scenes/transportation_choice.tscn");
				}
				else
				{
					await transitionRect.TransitionTo("res://scenes/place.tscn");
				}
			}
		};

		choiceContainer.AddChild(button);
	}

	private Texture2D GetIconFromIndication(string indication) {
		string prefix = "res://assets/";
		string suffix = ".png";
		string name = MapIndicationToIconName(indication);

		return GD.Load<Texture2D>(prefix + name + suffix);
	}

	private string MapIndicationToIconName(string indication) {
		switch (indication) {
			case "Retour à la réalité":
				return "France";
			case "C'est les vacances !":
				return "CostaRica";
			case "Vers l'Est":
				return "Est";
			case "Vers l'Ouest":
				return "Ouest";
			case "Vers le Nord":
				return "Nord";
			case "Vers le Sud":
				return "Sud";
			case "Vers le Nord-Est":
				return "NordEst";
			case "Vers le Nord-Ouest":
				return "NordOuest";
			case "Vers le Sud-Est":
				return "SudEst";
			case "Vers le Sud-Ouest":
				return "SudOuest";
			default:
				return ""; 
		}
	}
}

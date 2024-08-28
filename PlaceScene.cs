using FYBF;
using Godot;
using System;

public partial class PlaceScene : Control
{
	private static double ANIMATION_DURATION = 5.0;

	private GameState gameState;
	private double TimeElapsed;
	private string Interaction;
	Label DialogText;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		gameState = GameState.GetInstance();
		gameState.SetCurrentPlace("Dambelin");

		Label placeName = GetNode<Label>("Panel/PlaceName");
		Label char2Name = GetNode<Label>("Panel/Char2/Char2Name");
		DialogText = GetNode<Label>("Panel/Char2/SpeechBubble/DialogText");

		var charInfo = gameState.GetCurrentCharacter();
		int numberOfLines = charInfo.Interaction.Count("\n") + 1;
		Interaction = charInfo.Interaction;
		TimeElapsed = 0.0;

		placeName.Text = gameState.GetCurrentPlaceName();
		char2Name.Text = charInfo.Character;
		DialogText.Text = "";
		DialogText.LabelSettings.FontSize = (int)(0.9 * DialogText.Size[1] / (numberOfLines + 1)); // Keep one more line in case of text wrapping
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if (TimeElapsed > ANIMATION_DURATION) return;

		TimeElapsed += delta;

		GD.Print(delta);

		int numberOfChars = (int)(Interaction.Length * TimeElapsed / ANIMATION_DURATION);
		DialogText.Text = Interaction.Substring(0, numberOfChars);
	}
}

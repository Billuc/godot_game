using FYBF;
using Godot;
using System;
using System.Linq;

public partial class PlaceScene : Control
{
	private const double ANIMATION_DURATION = 5.0;
	private const int AMPLIFICATION_FACTOR = 15;

	private GameState gameState;
	private double TimeElapsed;
	private string Interaction;
	Label DialogText;
	NextButton NextButton;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		gameState = GameState.GetInstance();

		Label placeName = GetNode<Label>("Panel/PlaceName");
		Node char2Rect = GetNode("Panel/Char2");
		Label char2Name = GetNode<Label>("Panel/Char2/Char2Name");
		DialogText = GetNode<Label>("Panel/Char2/SpeechBubble/DialogText");
		NextButton = GetNode<NextButton>("Panel/NextButton");

		var charInfo = gameState.GetCurrentCharacter();
		int numberOfLines = charInfo.Interaction.Count("\n") + 1;
		Interaction = charInfo.Interaction;
		TimeElapsed = 0.0;

		placeName.Text = gameState.GetCurrentPlaceName();

		if (string.IsNullOrEmpty(charInfo.Character))
		{
			char2Rect.Free();
			return;
		}

		char2Name.Text = charInfo.Character;
		DialogText.Text = "";
		DialogText.LabelSettings.FontSize = (int)(0.9 * DialogText.Size[1] / (numberOfLines + 1)); // Keep one more line in case of text wrapping
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override async void _Process(double delta)
	{
		if (string.IsNullOrEmpty(Interaction) || TimeElapsed > ANIMATION_DURATION) return;

		TimeElapsed += delta;

		string interactionWithAmplifiedBreaks = Interaction.Replace("\n", new string('\n', AMPLIFICATION_FACTOR));

		int numberOfChars = (int)(interactionWithAmplifiedBreaks.Length * TimeElapsed / ANIMATION_DURATION);
		DialogText.Text = interactionWithAmplifiedBreaks.Substring(0, numberOfChars).Replace(new string('\n', AMPLIFICATION_FACTOR), "\n").TrimEnd('\n');

		if (TimeElapsed > ANIMATION_DURATION) {
			await NextButton.ShowWithAnimation();
		}
	}
}

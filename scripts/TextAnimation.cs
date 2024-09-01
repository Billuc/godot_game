using System;
using Godot;

namespace FYBF
{
    public class TextAnimation
    {
        private const int AMPLIFICATION_FACTOR = 15;

        private bool Started { get; set; } = false;
        private CanvasItem RootNode { get; set; }
        private Label LabelNode { get; set; }
        private double AnimationDuration { get; set; }
        private string TextToDisplay { get; set; } = String.Empty;
        private double TimeElapsed { get; set; }

        public TextAnimation(CanvasItem rootNode, Label labelNode, double animationDuration = 5.0)
        {
            RootNode = rootNode;
            LabelNode = labelNode;
            AnimationDuration = animationDuration;
            TimeElapsed = 0.0;
        }

        public void SetTextToDisplay(string textToDisplay)
        {
            if (Started)
            {
                throw new System.Exception("Do not set text while animation is ongoing");
            }

            TextToDisplay = textToDisplay;
        }

        public void Start()
        {
            int numberOfLines = TextToDisplay.Count("\n") + 1;
            RootNode.Modulate = new Color(1.0f, 1.0f, 1.0f, 1.0f);
            LabelNode.LabelSettings.FontSize = (int)(0.9 * LabelNode.Size[1] / (numberOfLines + 1)); // Keep one more line in case of text wrapping

            TimeElapsed = 0.0;
            Started = true;
        }

        public bool Process(double delta)
        {
            if (!Started) return false;

            TimeElapsed += delta;

            string textWithAmplifiedBreaks = TextToDisplay.Replace("\n", new string('\n', AMPLIFICATION_FACTOR));

            int numberOfChars = (int)(textWithAmplifiedBreaks.Length * TimeElapsed / AnimationDuration);
            LabelNode.Text = textWithAmplifiedBreaks.Substring(0, numberOfChars).Replace(new string('\n', AMPLIFICATION_FACTOR), "\n").TrimEnd('\n');

            if (TimeElapsed > AnimationDuration)
            {
                Started = false;
            }

            return TimeElapsed > AnimationDuration;
        }

        public void Hide()
        {
            RootNode.Modulate = new Color(1.0f, 1.0f, 1.0f, 0.0f);
            TimeElapsed = 0.0;
            Started = false;
        }
    }
}
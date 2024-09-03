# DevLog

## 27/08/2024

- Moved to Godot
- Project setup
- Main scene
- Play button leading to the next scene
- Fade transition between scenes. The way it works with a ColorRect on each scene is not something I like + it jitters a bit

## 28/08/2024

- Using data from GameScene in place scene
- Added scenes for dialogs, transportation choice and destination choice
- Added animation on text display (by writing characters one by one)
- Added delay on newlines (by amplifying the importance of newline chars in the string)
- Added all the connections
- Added pngs for all the characters and transportation types
- Added icons on transportation choice buttons
- The basis on the game is done

## 29/08/2024

- Added an animation scene when changing transportation type
- Created the last pngs for the buttons
- Added the icons to the buttons

## 01/09/2024

- Reorganized a bit the project to work more efficiently
- Added constraints on connections
- Modified the DestinationChoiceScene to display a message when a constraint is met
- I haven't tested this yet though
- Created a class to handle TextAnimations, might create more of those
- I discovered Custom anchor layouts which respond to my needs for a lot of things

## 02/09/2024

- Using custom anchors to fix many position problems
- Fixed some styling issues discovered afterwards
- Fixed transportation animation played twice
- Fixed image placement in Place scene
- Fixed ConstraintLabel not showing
- Added "Previous" button to return to transportation type selection

## 03/09/2024

- D-1
- Replaced Transportation Change Animation
- Fixed scene transitions
- There is a flicker occuring between scenes, gotta investigate
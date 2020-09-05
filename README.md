## 🧩 Flipping Cubes

![ver](https://img.shields.io/badge/version-0.2.0-blue.svg)
![unity](https://img.shields.io/badge/unity-2019.4.8f1-green.svg)
![flavor](https://img.shields.io/badge/flavor-win32%20x86-brightgreen.svg)
![size](https://img.shields.io/badge/size-51.2%20MB-yellow.svg)
![license](https://img.shields.io/badge/license-MIT-blueviolet.svg)

This is a simple puzzle game intrudocued and demonstrated by Mr. Chien in Taiwan Puzzle Community (TPC) 2020. Players try to move the combination of four cubes to some specific targets by flipping them iteratively.

![Demo.gif](https://i.imgur.com/6THO3rH.gif)

 
### 📜 Rules
1. Flip the cubes using keyboard.
2. For each stage, try to move the cubes to the correct place with corresponding colors.
3. The moving area is restricted to the map itself and the remaining spaces.
4. The less steps you use, the better ranking you get.


### ▶️ Getting Started
Pre-built binaries can be found in [released zip](https://github.com/der3318/flipping-cubes/releases/download/v0.2.0/FlippingCubes.zip). Double click <kbd>FlippingCubes.exe</kbd> to start a game. Game stages and score board can be added or modified by changing the txt configuration files:

| GameSettings/ | Usage |
| :- | :- |
| Background.jpg | background image including score board area |
| ScoreBoard.txt | all the socres are stored here having one socre per line |
| Stage{0,1:D3}.txt | stage info indicating the target postion of red, green, blue and yellow cube respectively |


### ⚙ Unity Scripts
The game is built under Unity3D engine. Here is an overview of the main scripts:

| Assets/ | Description |
| :- | :- |
| [Cubes.cs](https://github.com/der3318/flipping-cubes/blob/master/Assets/Cubes.cs) | input control, boundary check and flipping logic |
| [GameMap.cs](https://github.com/der3318/flipping-cubes/blob/master/Assets/GameMap.cs) | dynamic map genration with different styles of tiles |
| [GameSettings.cs](https://github.com/der3318/flipping-cubes/blob/master/Assets/GameMap.cs) | global game settings, status and IO operations |
| [ScoreBoard.cs](https://github.com/der3318/flipping-cubes/blob/master/Assets/ScoreBoard.cs) | score board updating |
| [StepCount.cs](https://github.com/der3318/flipping-cubes/blob/master/Assets/StepCount.cs) | step counting |


### 🔗 Referenced Resources
| Name |Site | Description |
| :- | :- | :- |
| VectorStock | https://www.vectorstock.com/ | background image |
| CoolText | https://cooltext.com/ | font family |
| IconFinder | https://www.iconfinder.com/ | launcher icon |


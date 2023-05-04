# 🪨 Unity Clonk  Importer
## 📚 Description
The Render Unity Clonk Importer is a tool to import Clonk Sprite animations into the Unity Engine. In general, it should be noted that this plugin is aimed more at Clonkers who like the clonk workflow and also work with Unity, like me.
## 🎮What is a Clonk?
Clonk is a 2D game series from Matthes Bender and has a long history of Modding. While Clonk is a completeley different game without any relation to the Unity Engine, the Clonk Importer allows you to use the Clonk Sprites Workflows to create 2D Animations for Unity.
Since there is a wonderful Blender plugin for Clonk, which could render spritesheets for Clonk characters and takes many useful aspects into account, it seemed useful to me to extend the potential of the Clonk workflow to other projects like Unity Games. I mainly want to render non clonk-specific character animations with RenderClonks, but the tool allows importing any kind of clonk Spritesheet.

## ⁉️How to use?
![enter image description here](https://raw.githubusercontent.com/valentinwinkelmann/Clonk-Unity-Importer/main/GithubResources/Screenshot_01.png)
### 🔌Installation
Just download the latest release and import the unitypackage into your Unity Project.
### 💾Importing a Clonk Spritesheet
1. Import one or more Clonk Spritesheet( typically a PNG file called "graphics.png") into your Unity Project in the folder where you want to store the animations.
2. Import the Clonk ActMap.txt file into your Unity Project in the same or anoter folder.
3. Optional: Import the Clonk DefCore.txt file into your Unity Project in the same or anoter folder. ( Contains information about the Title Image)
4. Go to Tools/Render Clonk Importer and drag the Spritesheet(s) and the ActMap.txt and DefCore.txt file into the corresponding fields.
5. Clink on "Generate Spritesheet" and it generates the Spritesheet for the Sprite. The Spriteseet contains the Clonk animation itself and the name of the animation folowed by _[FRAME NUMBER] for each frame.
### How can i make Spritesheets?
You can use the Blender Plugin for Clonk to render Spritesheets. You can find it here: [Clonk Blender Plugin](https://github.com/RoboClonk/RenderClonkAddon)
You can use Anigrab as many clonkers did before to pack their mod spritesheets. You can find it here: [Official Clonk Developer Documentation](https://clonk.de/developer.php?lng=en)


## 📃License
The Unity Clonk Importer is licensed under the MIT License. See `LICENSE` for more information.
Im not affiliated with Matthes Bender or Clonk in any way. This dosn't allow you to use the Original Clonk Spritesheets or others Mod Spritesheets in any way. It's meant to be used with your own Spritesheets.
'Clonk' is a registered trademark of Matthes Bender.

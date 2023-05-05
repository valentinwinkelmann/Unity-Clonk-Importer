# 🪨 Unity Clonk  Importer
## 📚 Description
The Render Unity Clonk Importer is a tool to import Clonk Sprite animations into the Unity Engine. In general, it should be noted that this plugin is aimed more at Clonkers who like the clonk workflow and also work with Unity, like me.
## 🎮What is a Clonk?
Clonk is a 2D game series from Matthes Bender and has a long history of Modding. While Clonk is a completeley different game without any relation to the Unity Engine, the Clonk Importer allows you to use the Clonk Sprites Workflows to create 2D Animations for Unity.
Since there is a wonderful Blender plugin for Clonk, which could render spritesheets for Clonk characters and takes many useful aspects into account, it seemed useful to me to extend the potential of the Clonk workflow to other projects like Unity Games. I mainly want to render non clonk-specific character animations with RenderClonks, but the tool allows importing any kind of clonk Spritesheet.

## ⁉️How to use?
![enter image description here](https://raw.githubusercontent.com/valentinwinkelmann/Clonk-Unity-Importer/main/GithubResources/Screenshot_01.png)
### 📦Requirements
- Unity 2021.3 or higher ( it should work with older versions, but i didn't test it )
- Unity 2D Animation Package - com.unity.2d.animation
- Knowledge about the Clonk Workflow
### 🔌Installation
Just download the latest release and import the unitypackage into your Unity Project.
### 💾Importing a Clonk Spritesheet
1. Import one or more Clonk Spritesheet( typically a PNG file called "graphics.png") into your Unity Project in the folder where you want to store the animations.
2. Import the Clonk ActMap.txt file into your Unity Project in the same or anoter folder.
3. Optional: Import the Clonk DefCore.txt file into your Unity Project in the same or anoter folder. ( Contains information about the Title Image)
4. Go to Tools/Render Clonk Importer and drag the Spritesheet(s) and the ActMap.txt and DefCore.txt file into the corresponding fields.
5. Clink on "Generate Spritesheet" and it generates the Spritesheet for the Sprite. The Spriteseet contains the Clonk animation itself and the name of the animation folowed by _[FRAME NUMBER] for each frame.
### 🎨How can i make Spritesheets?
You can use the Blender Plugin for Clonk to render Spritesheets. You can find it here: [Clonk Blender Plugin](https://github.com/RoboClonk/RenderClonkAddon)
You can use Anigrab as many clonkers did before to pack their mod spritesheets. You can find it here: [Official Clonk Developer Documentation](https://clonk.de/developer.php?lng=en)


## 📦Features
### 🎨Script Based Animation
To animate Sprites in a Clonky Way i create a basic `UnityClonkAnimation.cs` Component and a basic ScriptableObject `UnityClonkActMap.cs`. The `UnityClonkActMap` object contains all the information about the animations like a `ActMap.txt`, but instead of storing the animation frame coordinates, it stores each Frame as Sprite refferecne in a Dictionary structure `<ActionName, Frame[]>`, This allows to easily retrieve any frame based on its action name. You can use the UnityClonkAnimation Component to play the animations from the `UnityClonkActMap` Object directly or implement your own Animation System.

### 🎞️SpriteLibrary Asset
In Clonk it was easy to swap the graphics of a Clonk. In Unity Sprite Swapping is a bit more complicated. To make it easier to swap Sprites of the same Animation set (ActMap) but with different graphics, the Importer generates a SpriteLibrary Asset for each ActMap.txt file. The SpriteLibrary Asset contains all the animations from the ActMap.txt file. You can use the SpriteLibrary Asset to swap the animations of a Sprite. ( Follow this [Tutorial](https://www.youtube.com/watch?v=6mNak-mQZpc) to learn how to use SpriteLibrary Assets in Animations). I Put example animations in the TestSprite folder.

### 🔮Sprite Shader
I Build a very basic URP Shader which uses the Overlay Texture of the Spritesheet, if it exists. For this, the Overlay has to be applied to the Spritesheet(`_MainTex`) as a Secondary Texture(`_Overlay`).
1. Open the Sprite Editor of the primary Texture of the Spritesheet, which is used for the Sprite Renderer.
2. In the Sprite Editor Window, click on the Upper Right Dropdown and select "`Secondary Textures`".
3. Click on the "+" Button and select the Overlay Texture of the Spritesheet and set the Name to "`_Overlay`".
4. Click on "Apply" and the Shader should work.

## 🎯Todo
- [x] Import Clonk Spritesheets into Unity and split into ActMap Animations via ActMap.txt
- [x] Import Title Image into Unity via DefCore.txt
- [x] Support for multiple Spritesheets
- [X] Sprite Shader which understands the Clonk Overlay Texture
- [ ] Add extra sprites like Overlay.png as [Secondary Textures](https://docs.unity3d.com/Manual/SpriteEditor-SecondaryTextures.html)
- [X] Generate [Sprite Library Asset](https://docs.unity3d.com/Packages/com.unity.2d.animation@7.0/manual/SpriteSwapIntro.html) for Sprite Swapping.
- [ ] Let Animations be placed ontop of Graphics, like Single Door Spritesheet Animations on a House.
- [ ] Example Clonk Player Controller just for Fun🕹️



## 📃License
The Unity Clonk Importer is licensed under the MIT License. See `LICENSE` for more information.
Im not affiliated with Matthes Bender or Clonk in any way. This Importer Plugin dosn't allow you to use the Original Clonk Spritesheets or others Mod Spritesheets in any way. It's meant to be used with your own Spritesheets.
'Clonk' is a registered trademark of Matthes Bender.

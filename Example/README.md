# DISCLAIMER

It is very likely, that this README is incomplete, incorrect or simply outdated.

This description is intended for people comfortable with writing/modifying/compiling code, reading online manuals and finding answers by using Google (or whatever search engine you prefer).

While my goal with creating ModComponent is to make creating new items for The Long Dark as easy as possible, this cannot be a substitute for learning how to do modding.  
You will need to understand how The Long Dark works. ("You can't change what you don't understand")  
You will need to understand how Unity works.  
You will need to understand how C# works.  

> By all means ask how to do certain things before spending weeks trying to figure it out all on your own.  
> If you think this README or the sample project or ModComponent contains an error or could/should be improved: I'd love to hear that.

# Sample Project "Rubber Duck"

This sample project demonstrates how to use ModComponent to create new in-game items.  
It is based on the template project.


The two parts are
- the Unity project, containing the 3D model, Localization, Icons and Prefabs required.
- the Visual Studio project, containing all the game logic and glue code.

## Unity Project

The Unity project will create an AssetBundle that contains all assets for the mod to work.

The AssetBundle is created using the Editor Script "Assets/Editor/BuildAssetBundles", which creates the new menu entry "Assets > Build AssetBundles".
There's no need to edit or understand that script in any way.
Everything can be configured with the Editor UI and clicking that menu entry is entirely sufficient.

All configured AssetBundles will be created in a directory "AssetBundles" (as a sibling to "Assets")

## Visual Studio Project

The Visual Studio project will create a DLL that contains all game logic and glue code for the mod to work.

You will need to set reference paths to both "TheLongDark/tld_Data/Managed" and "TheLongDark/mods" and have the ModLoader and prerequisite mods ("AssetLoader", "ModComponent") installed.
The DLL will be created in the usual "bin/Release" directory.



## Distribution / Installation

The DLL and the AssetBundle need to be packaged together to be installed.

The sample project makes use of the [Auto-Mapper](https://github.com/WulfMarius/ModComponent/wiki/Auto-Mapper) feature of ModComponent, so no glue code is required.  
Instead the files only need to be put into the `auto-mapped` folder and will be loaded automatically.

The SoundBank is already pre-created (This process involves using the right version of WWise and audio files. I might explain this process later).

When you update the AssetBundle or the DLL you will need to copy those manually (or create your own tool/script for doing this).
The final layout can be seen in directory "Distributable":

	auto-mapped/Rubber-Duck/Rubber-Duck.dll
	auto-mapped/Rubber-Duck/Rubber-Duck.unity3d
	auto-mapped/Rubber-Duck/Rubber-Duck.bnk

To install this mod, you need to copy the contents of the "Distributable" folder to "TheLongDark/mods".
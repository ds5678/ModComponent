# DISCLAIMER

It is very likely, that this README is incomplete, incorrect or simply outdated.

This description is intended for people comfortable with writing/modifying/compiling code, reading online manuals and finding answers by using Google (or whatever search engine you prefer).

While my goal with creating ModComponent is to make creating new items for The Long Dark as easy as possible, this cannot be a substitute for learning how to do modding.

* You will need to understand how The Long Dark works.
* You will need to understand how Unity works.
* You will need to understand how C# works.

> By all means ask how to do certain things before spending weeks trying to figure it out all on your own.  
> If you think this README or the template project or ModComponent contains an error or could/should be improved: I'd love to hear that.

# Template Project

This is a template project for the Unity Editor for creating new item mods.

It contains the basic folder structure and minimal assets and scene hierarchy suitable for such a project.


# Folder Structure

- **ClothingPaperDoll** - The folder for the textures to be used for clothing on the paper doll.

- **Editor** - This folder contains editor scripts. The contents of this folder will not be exported by Unity.
  - **BuildAssetBundles.cs** - This script is for exporting Asset Bundles and creates the menu item "Assets > Build AssetBundles".  
It should cover all your needs and there is probably no need for you to adjust it.

- **InventoryGridItems** - The folder for the textures to be used as inventory icons of the items.

- **Models** - The folder for importing 3d models, materials, textures, shaders, etc.

- **Prefabs** - The folder for the actual prefabs of the items.

- **Localization.json** - An example file to be used for in-game texts.

- **Template.unity** - The template scene containing "Main Camera" for creating item icons and "Weapon Camera" for creating equippable items (Note: Equippable items don't work right now).


# Exporting Asset Bundles

The editor script "BuildAssetBundles.cs" creates the menu item "Assets > Build AssetBundles" and selecting this menu item will export all assets into their assigned asset bundle.  
If you use the suggested folder structure, you will only need to export the folders "ClothingPaperDoll", IventoryGridItems", and "Prefabs", as well as "Localization.json".

Assign each of those items the asset bundle name that you want to use. (See [AssetBundles and the AssetBundle Manager](https://unity3d.com/learn/tutorials/topics/scripting/assetbundles-and-assetbundle-manager) - especially "Working with AssetBundles" - for how to do this.)  
The export script will automatically append the file extension ".unity3d", which is necessary if you want to use the asset bundle with the [Auto-Mapper](https://github.com/WulfMarius/ModComponent/wiki/Auto-Mapper) of ModComponent.

The asset bundle is exported to the folder "AssetBundles" next to the folder "Assets". You can right-click on the "Assets" node in the "Project" view and select "Show in Explorer" to easily navigate to the "AssetBundle" folder.

When your asset bundle was built, you need to copy it into the folder "TheLongDark/mods/auto-mapped" for the Auto-Mapper to find and load it.

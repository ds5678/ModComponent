This page explains how to get started using ModComponent.

# Internal Zip File Hierarchy

`someItemPackName.modcomponent`<br/>
|----`auto-mapped`<br/>
|----|---- all the component json files and any Unity3d asset bundles go here or in any subfolders<br/>
|----`blueprints`<br/>
|----|---- any blueprint json files go here or in any subfolders<br/>
|----`gear-spawns`<br/>
|----|---- any gearspawn text files go here or in any subfolders<br/>

## auto-mapped

This folder and any subfolders contain all the asset bundles from Unity Editor and all the component json files for the prefabs in the asset bundles. The asset bundle names do not matter, but the names of the prefabs in the Unity Editor do matter. An asset bundle can contain multiple item prefabs.

Each json file contains the component data for exactly one prefab. If an asset bundle contains multiple prefabs, each prefab will have its own json file. The naming of these json files is very important. If a prefab in Unity Editor is named `GEAR_SampleItem.prefab`, its corresponding json file must be named `sampleitem.json`. If a json file is missing or misnamed, it will cause an error. The format of these json files is subject to change. A folder has been created in the repository containing up-to-date templates for component jsons.

## blueprints

This folder and any subfolders contain any json files for adding blueprints to the game. Each json contains the data for only one blueprint. The names of these blueprint jsons do not matter.

## gear-spawns

This folder and any subfolders contain text files for altering loot tables and adding gear spawns to the game. These changes only take effect when the game generates all of its "vanilla" loot (this typically happens on the start of a new save or when a scene is first entered). The information for creating these files can most easily be acquired with the [Coordinates-Grabber](https://github.com/ds5678/Coordinates-Grabber).

# Unity Editor Hierarchy

`Assets`<br/>
|----`ClothingPaperDoll`<br/>
|----|----`Male`<br/>
|----|----`Female`<br/>
|----`Editor`<br/>
|----`Models`<br/>
|----`InventoryGridIcons`<br/>
|----`Prefabs`<br/>
|----`Localization.json`<br/>

> Any missing/nonexistent files or folders must be manually created

## ClothingPaperDoll

The subfolders of this folder contain all the images for the clothing menu. If the user is not designing clothing items, this folder is unnecessary. Otherwise, it needs to be included in the asset bundle.

## Editor

This folder should contain [`BuildAssetBundles.cs`](https://github.com/ds5678/Food-Pack/blob/master/Unity/Assets/Editor/BuildAssetBundles.cs). If it doesn't, you need to add it. This folder is not included in the asset bundle.

## Models

This folder contains all the 3d models, materials, and textures for your new items. It is not included in the asset bundle.

## InventoryGridIcons

This folder contains the icons shown in the inventory menu. These icons are 512x512 PNG images with a transparent background. They must be named with the following format: `ico_GearItem__SampleItem.png`. Note the double underscore. This folder is included in the asset bundle.

## Prefabs

This folder contains your item prefabs. Their names need to be formatted like `GEAR_SampleItem.prefab`. Note that Unity Editor often doesn't display file extensions; be sure that you didn't name your file ~~GEAR_SampleItem.prefab.prefab~~. Prefabs need to have a transform component and a collider component (a box collider is normally good enough). If you're porting 1.56 mods, any old API components must be removed; their component data is now entered in a json file outside of Unity Editor. This folder is included in the asset bundle.

## Localization.json

This file is where you define any in-game text. It is not necessary to have an entry for every localization. Item names typically carry an ID with the format `GAMEPLAY_SampleItem`. Similarly, item descriptions typically carry an ID with the format `GAMEPLAY_SampleItemDescription`. This file is included in the asset bundle.

## Plugins

If your asset folder contains this folder, delete it and any contents. It used to contain the old API file. It no longer does.

## Building the Asset Bundle

If everything is in its proper place and configured correctly, you should be able to build the asset bundle at any time by right clicking in the Unity Editor File Explorer. The asset bundle should be created outside the Assets folder in a folder called `AssetBundles`. It will have the file extension `unity3d`.
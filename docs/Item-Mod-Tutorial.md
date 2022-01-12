Creating your own item mod with ModComponent involves 5 steps:

1. Having 3D models, sounds and icons for the items you want to make
2. Building an asset bundle with the Unity Editor
3. Creating json files for the items you want to make
4. Installing your mod
5. Testing and fine-tuning

# Assets (3D Models, Sounds, Textures and Icons)

## 3D Models

See [3D Models](3D-Models.md).

## Sounds

Sounds files are imported into WWise where they are configured and assembled into a sound bank.

Only one version of WWise will work. It is listed on [For Developers](For-Developers.md)

## Textures

Textures are regular images that are used in the UI (e.g. representing items in the player's inventory, a backdrop in the skills list), applied to 3D objects to make them look more realistic or basically anywhere else in the game.

> The difference between a texture and an icon in this tutorial is in the technical way they are provided.  
> Some things in TLD are called "icons" because that's they represent (Wikipedia: "a pictogram or ideogram displayed on a computer screen"), but technically they are still provided as a texture.  
> So while an item needs an "inventory icon", you still need to provide a "texture" for this.

Unity can handle textures in many different image formats (PNG, JPEG, BMP, etc.) and they can be edited with any regular image manipulation software.  
Unity will automatically export textures that are used by exported prefabs. Textures that are only referenced by scripts (either your own, plugins or TLD directly) need to be assigned to an Asset Bundle or in a folder that's assigned to an Asset Bundle.

When you have a suitable image, import it as a new asset and configure its settings in the Inspector.  
For "inventory icons" the settings can usually be left at their defaults.

#### Alpha Is Transparency

Use the "alpha" channel of the texture and interpret is as transparency. That means the texture might have areas that are completely invisible (it will have "holes"). If the image does not have an alpha channel (e.g. JPEG, BMP), there will be no invisible areas.

This setting should be enabled when using textures for "icons" and basically everywhere, where a texture is displayed over some background.


#### Generate Mip Maps

Mipmaps are pre-calculated smaller versions of the original texture. At runtime the most suitable size of the texture is used, which improves rendering speed and quality, since the mipmaps can be produced using are "more expensive" algorithms.

The only downside of this is increased size (the total size of the mipmaps will be 1/3 of the original texture).

Usually there's no reason to disable mipmaps.

## Icons

Many icons need to be in PNG format for ModComponent to read them.

Inventory Grid Icons are 512x512

# Building an Asset Bundle

> For this step you need the correct version of Unity installed. (listed on [For Developers](For-Developers.md)).

The easiest way to start your Unity project is to use the template project: https://github.com/ds5678/ModComponent_TemplateProject

Ignore the contents of the folder "Editor" for now, but do not delete it - It is required!

> Remember that you need to save your scene and project to prevent loss of work and data.  
Save when you feel like you completed a step and are confident that the current state of the project is good.  
Consider using a versioning system like [Git](https://git-scm.com/), even if you don't intend to share your mod (which you should really do!)

You can take a look at existing item packs to see how the finished result might look.


Decide which name you want to use for your asset bundle and assign the folders `InventoryGridIcons` and `Prefabs` and the file `Localization.json` to the asset bundle with that name. Variants are not required.  
See section "Working with AssetBundles" in https://unity3d.com/learn/tutorials/topics/scripting/assetbundles-and-assetbundle-manager for how to do this.  

(If your mod requires additional assets, you will need to assign them to the asset bundle as well.)

For each item you want to make, repeat the following steps:
  * Import your 3D model into the "Models" folder.
  * Create a new GameObject in the scene using the imported model
  * Add a collider.This collider is used for detecting items under the reticle. A BoxCollider is usually good enough.
  * Create a [prefab](https://docs.unity3d.com/Manual/Prefabs.html) from your finished GameObject and name it `GEAR_...`, e.g. `GEAR_CarKey`. You can create a prefab by dragging an item from the Hierarchy to the Project Window.
This is very important, because a lot of the functions in TLD expect items to have a name like that.
  * Move your new prefab into the `Prefabs` folder.
  * Add a suitable texture as inventory icon in the folder `InventoryGridIcons`. (For details see [[Basic Item Configuration]])
  * Add the required texts to `Localization.json`. (For details see [[Basic Item Configuration]])


* Export the asset bundle with the menu item "Assets > Build AssetBundles"  
The asset bundle will be created as new file in the folder `AssetBundles` in your Unity project. The file will automatically receive the extension `.unity3d`. Do not remove this extension - it is required by the Auto-Mapper.  
(The folder `AssetBundles` will be next to the `Assets` folder, so you won't see it in the Unity Editor. Use a file explorer instead.)

* Create a json file for your item. Its naming is important. If your item is called `GEAR_CarKey`, then the json file should be named `carkey.json`.
* Add the [component](Basic-Information-about-Components.md) and [behaviours](Basic-Information-about-Behaviours.md) from ModComponent that you need/want and configure them accordingly.


# Installing your Mod

> Make sure you have the latest version of ModComponent and its dependencies installed.

* Copy your asset bundle and its corresponding jsons into `TheLongDark/mods/auto-mapped`.  
* You can create additional subfolders inside `auto-mapped`, this will keep it nice and tidy.*

Optional:
* Create a [gear spawn configuration](Gear-Spawns.md) for your item(s).  
* If you do not provide a spawn configuration, the only way to obtain your item(s) will be console commands and crafting.
* Create a [[blueprint configuration|Blueprints]] for your item(s).  
* If you do not provide a blueprint configuration, the only way to obtain your item(s) will be console commands and finding it somewhere in the game.
* Copy additional resources into the "auto-mapped" folder, e.g. sound banks, DLLs.  
* This is only required if your mod needs those additional resources.


# Testing and Fine-Tuning

If you completed all of the steps above, you are ready to test your mod.  
Start The Long Dark and either resume a saved game or start a new one.

Using the console, you can add a new item to your inventory with  
`add [gearname]`

The "gearname" for your item(s) is either the "Console Name" you configured in the Unity Editor, or the name of the prefab without the leading "GEAR_".  
So `add carkey` (the gearname is case-insensitive) would add a new car key to your inventory (assuming such an item exists).  
(The console provides auto-complete for all known gearnames.)

Look into your inventory and you should see the item.  
It should have an icon, a name, description and properties (weight, etc.) according to your configuration.  
*This tests your basic configuration, icons and localization*

Drop the item and see if the reticle will react to it and show its name.  
*This tests your collider*

Move the item around and compare it with other existing items. See if its size, color and texture is good and matches what you wanted to have. Pick it up and rotate it in the inspect view.  
*This tests the size and look of your model*

The item should be back in your inventory now. See if it still has the correct icon and properties.  
*This tests if the item works properly when being found and picked up*

If you created an item with a primary function, e.g. food, test if you can use the item and see if it behaves correctly. Depending on what type of item you created, this can be more complex and require multiple steps. E.g. for clothing you might want to try and combine it with different other items to see how they overlap on the paper doll.

If you find anything that you want to change:
* quit The Long Dark
* go back to the Unity Editor and adjust the **prefab** (only the prefab is exported, not the GameObject in the scene)  
or the inventory icon in `InventoryGridIcons`  
or the texts in `Localization.json`
* export the asset bundle
* copy the new asset bundle to your development `auto-mapped` folder
* make your `.modcomponent` file
* copy your `.modcomponent` file to the mods folder
* start testing again
* repeat until you're satisfied with the result.

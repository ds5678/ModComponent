See also [[Clothing Item Configuration]]

# Localized Texts

All of the texts displayed in the game are localized in one way or another.  
In the configuration of an item only the localization keys are supplied and those keys are replaced with the actual text matching the selected language of the user.

Localized texts are provided as a JSON file, a template for which can be found in the Template Project
They can all be provided as a CSV file, with languages as columns and keys/texts as rows.  
The order of the languages is not important and the `AssetLoader` will automatically assign the texts to the languages available in-game.

If the user uses a language not provided in the localization of the mod, those texts will be displayed in English (which is considered to be the default language).  
Localization keys without any assigned text will rendered as the key itself.

It's a good idea to quote all texts, even if it isn't necessary in all cases.

The localization file must be called `Localization.json` or `Localization.csv`!  
[<img src="https://raw.githubusercontent.com/ds5678/ModComponent/master/Images/item-configuration/localization.png" height="200">](https://raw.githubusercontent.com/ds5678/ModComponent/master/Images/item-configuration/localization.png)

> If a localization key is used more than once, only the "last one" will win as they replace each other.  
> This can also be used to redefine pre-existing localization keys in the vanilla game.


# Inventory Icon

The inventory icon is a Default 2D texture and should be 512x512 pixels in size.  
The background of that texture should be transparent.  
Since this is purely a UI texture, mip maps can be disabled (saving space) and Wrap Mode and Filter Mode are irrelevant.

The inventory icon is located by convention.  
The path to the texture is produced by taking the item's name and replacing "GEAR_" with "/InventoryGridIcons/ico_GearItem__" (`two underscores!`).

So the item "GEAR_RubberDuck" must have its inventory icon texture at "/InventoryGridIcons/ico_GearItem__RubberDuck".

[<img src="https://raw.githubusercontent.com/ds5678/ModComponent/master/Images/item-configuration/inventory-icon-name.png" height="200">](https://raw.githubusercontent.com/ds5678/ModComponent/master/Images/item-configuration/inventory-icon-name.png)

> Since the lookup is purely by convention, this can also be used to override pre-existing inventory icons by simply providing icons with matching paths.


# Inspect Mode Model

"Inspect Mode" happens when the user picks up an item.  
The item will be shown in the center of the screen, allowing to user to "inspect" it and its properties.

## Default
The default way is to always show the same model for the same item.  
In this case `InspectModel` and `NormalModel` are left empty and the item carries the `Renderer` and `Collider`.

[<img src="https://raw.githubusercontent.com/ds5678/ModComponent/master/Images/item-configuration/inspect-default-01.png" height="200">](https://raw.githubusercontent.com/ds5678/ModComponent/master/Images/item-configuration/inspect-default-01.png)

## Alternative Inspect Model
It is possible to show a different model during inspect.  
In order to do this, the item itself **must not** carry a `Renderer` or `Collider` directly.  
[<img src="https://raw.githubusercontent.com/ds5678/ModComponent/master/Images/item-configuration/inspect-alt-01.png" height="200">](https://raw.githubusercontent.com/ds5678/ModComponent/master/Images/item-configuration/inspect-alt-01.png)

Instead add one child with a `Renderer` for the "Inspect Mode Model" and another child with a `Renderer` and `Collider` for the "Normal Mode Model".  
[<img src="https://raw.githubusercontent.com/ds5678/ModComponent/master/Images/item-configuration/inspect-alt-02.png" width="220">](https://raw.githubusercontent.com/ds5678/ModComponent/master/Images/item-configuration/inspect-alt-02.png) [<img src="https://raw.githubusercontent.com/ds5678/ModComponent/master/Images/item-configuration/inspect-alt-03.png" width="220">](https://raw.githubusercontent.com/ds5678/ModComponent/master/Images/item-configuration/inspect-alt-03.png) [<img src="https://raw.githubusercontent.com/ds5678/ModComponent/master/Images/item-configuration/inspect-alt-04.png" width="220">](https://raw.githubusercontent.com/ds5678/ModComponent/master/Images/item-configuration/inspect-alt-04.png)

Take note of their names. You will need that information for the item's json file.
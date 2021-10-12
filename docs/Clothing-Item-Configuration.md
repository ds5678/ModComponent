Configures items that can be worn.

Uses all properties of [[Basic Items|Basic Item Configuration]]

## Cheat Sheet

[<img src="https://raw.githubusercontent.com/ds5678/ModComponent/master/Images/item-configuration/clothing_cheatsheet.png" height="500">](https://raw.githubusercontent.com/ds5678/ModComponent/master/Images/item-configuration/clothing_cheatsheet.png)

The words are the Layer and Region of that slot. The number is the default draw layer for that layer and region.

## Region
Where the item can be worn

`Head`, `Chest`, `Hands`, `Legs`, `Feet`, `Accessory`

## Layer

Most regions offer multiple "slots" on the clothing screen.  
Each of these slots represents a `Layer`. The layers are `Base`, `Mid`, `Top`, `Top2`.

Examples:

* Boots are `Feet`, `Top`
* Gloves are `Hands`, `Base`
* Jackets are `Chest`, `Top` and `Top2`
* Hats are `Head`, `Base` and `Mid`

## Textures

The representation on the clothing screen is done with additional textures (images) and not derived from their 3D models!

There are different textures for the female and male clothing model and for damaged and undamage items.  
This means there must be 4 additional textures for each clothing item.  
The 4 textures must share a basename and be organized in a specific folder structure:

| Path | Texture Type |
| --------------| :-----: |
| `ClothingPaperDoll/Male/[main_basename]_HMM_.png` | (male + undamaged) |
| `ClothingPaperDoll/Male/[main_basename]_HMM__dmg.png` | (male + damaged) |
| `ClothingPaperDoll/Female/[main_basename]_HMF_.png` | (female + undamaged) |
| `ClothingPaperDoll/Female/[main_basename]_HMF__dmg.png` | (female + damaged) |

The field `MainTexture` in the item's json file is to be set to `[main_basename]_HMM_`.

Two additional **grayscale** textures (one for female, one for male) determine how to blend between the undamaged and damaged textures.  
The darker a blend area the less damage is required to render the damaged texture instead of the undamaged one.  
White areas will always render the undamaged texture, black areas will always render the damaged area.  


* ClothingPaperDoll/Male/[blend_basename]\_M\_.png *(male)*
* ClothingPaperDoll/Female/[blend_basename]\_F\_.png *(female)*

The field `BlendTexture` in the item's json file is to be set to `[blend_basename]_M_`
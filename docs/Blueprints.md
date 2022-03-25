Blueprints (crafting recipes) can be configured to appear additionally to the ones already present in the vanilla game.

ModComponent will look for configuration files in the `blueprints` directory within the item pack zip file.

Any files in that directory (or any subdirectories) ending with `.json` will be treated as a blueprint definition file and processed accordingly. The names of the files does not matter.

> The `AutoMapper` will automatically add those blueprints in addition to the ones from the directory mentioned above.

## Definition

Each file containes one blueprint definition in JSON.  
Example:  
```
{
    "Name": "A name to show in error messages",
    "RequiredGear": ["GEAR_Cloth"],
    "RequiredGearUnits": [4],
    "KeroseneLitersRequired": 0.0,
    "GunpowderKGRequired": 0.0,
    "RequiredTool": "GEAR_SewingKit",
    "OptionalTools": ["GEAR_HookAndLine"],
    "RequiredCraftingLocation": "Anywhere",
    "RequiresLitFire": false,
    "RequiresLight": true,
    "CraftedResult": "GEAR_Balaclava",
    "CraftedResultCount": 1,
    "DurationMinutes": 30,
    "CraftingAudio": "Play_CraftingCloth",
    "AppliedSkill" : "None",
    "ImprovedSkill" : "None"
}
```

> Important: 
> RequiredGear and RequiredGearUnits must be of the same length of the array. <br>
> (Example) <br>
> "RequiredGear": ["GEAR_ArrowHead", "GEAR_ArrowShaft", "GEAR_CrowFeather"], <br>
> "RequiredGearUnits": [1,1,3], <br>
>  <br>
> If you want an empty array of strings, write it like this : [], <br>
> This will result in an error : [""], <br>
>  <br>
> Item names can be obtained in-game by using the [Coordinates-Grabber](https://github.com/ds5678/Coordinates-Grabber/releases/latest)

## Parameters

### Name
An optional name for the blueprint. Only used in error messages.


### RequiredGear
The name of each gear needed to craft this item (e.g. GEAR_Line)


### RequiredGearUnits
### How many of each item are required? <br/>
This list has to match the RequiredGear list.


### KeroseneLitersRequired<br/>
How many liters of kerosene are required?


### GunpowderKGRequired
How much gunpowder is required? (in kilograms)


### RequiredTool
Tool required to craft (e.g. GEAR_Knife)


### OptionalTools
List of optional tools to speed the process of crafting or to use in place of the required tool.


### RequiredCraftingLocation
Where to craft? (Anywhere,Workbench,Forge,AmmoWorkbench)

### RequiresLitFire
Requires a lit fire in the ammo workbench to craft?


### RequiresLight
Requires light to craft?


### CraftedResult
The name of the item produced.


### CraftedResultCount
Number of the item produced.



### DurationMinutes
Number of in-game minutes required.


### CraftingAudio
Audio to be played.


### AppliedSkill
The skill associated with crafting this item. (e.g. Gunsmithing)<br>
Firestarting,
CarcassHarvesting,
IceFishing,
Cooking,
Rifle,
Archery,
ClothingRepair,
ToolRepair,
Revolver,
Gunsmithing
<br>
**If you do not need to apply the skill, use "None" instead of "" . ("" causes an error)**

### ImprovedSkill
The skill improved on crafting success.



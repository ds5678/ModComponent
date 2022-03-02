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
    "CraftingAudio": "PLAY_ClothingRepair",
    "AppliedSkill" : "None",
    "ImprovedSkill" : "None"
}
```

> Important: RequiredGear and RequiredGearUnits must be of the same length  
> Item names can be obtained in-game by using the [Coordinates-Grabber](https://github.com/ds5678/Coordinates-Grabber/releases/latest)

# Template
```
{
    "ModToolComponent" : {
                            "DisplayNameLocalizationId" : "GAMEPLAY_SampleItem",
                            "DescriptionLocalizatonId" : "GAMEPLAY_SampleItemDescription",
                            "InventoryActionLocalizationId" : "",
                            "WeightKG": 0.5,
                            "DaysToDecay" : 0,
                            "MaxHP" : 100,
                            "InitialCondition" : "Random",
                            "InventoryCategory" : "Tool",
                            "PickUpAudio" : "",
                            "PutBackAudio" : "",
                            "StowAudio" : "Play_InventoryStow",
                            "WornOutAudio" : "",
                            "InspectOnPickup" : true,
                            "InspectDistance" : 0.4,
                            "InspectAngles" : [0, 0, 0],
                            "InspectOffset" : [0, 0, 0],
                            "InspectScale" :  [1, 1, 1],
                            "NormalModel" : "",
                            "InspectModel" : "",
                            
                            "EquippedModelPrefab" : "",
                            "ImplementationType" : "",
                            "EquippingAudio" : "",

                            "ToolType" : "None",
                            "DegradeOnUse" : 0.0,
                            "Usage" : "All",
                            "SkillBonus" : 0,
                            "CraftingTimeMultiplier" : 1.0,
                            "DegradePerHourCrafting" : 0.0,
                            "BreakDown" : false,
                            "BreakDownTimeMultiplier" : 1.0,
                            "ForceLocks" : false,
                            "ForceLockAudio" : "",
                            "IceFishingHole" : false,
                            "IceFishingHoleDegradeOnUse" : 0.0,
                            "IceFishingHoleMinutes" : 30,
                            "IceFishingHoleAudio" : "",
                            "CarcassHarvesting" : false,
                            "MinutesPerKgMeat" : 10,
                            "MinutesPerKgFrozenMeat" : 10,
                            "MinutesPerHide" : 30,
                            "MinutesPerGut" : 30,
                            "DegradePerHourHarvesting" : 0.0,
                            "StruggleBonus" : false,
                            "DamageMultiplier" : 1.0,
                            "FleeChanceMultiplier" : 1.0,
                            "TapMultiplier" : 1.0,
                            "CanPuncture" : false,
                            "BleedoutMultiplier" : 1.0
    }
}
```

# Parameters

This component uses all the parameters from the [[Generic Equippable Component Documentation]].

## ToolType
*ToolKind* <br/>
The type of the tool item. This determines for which actions it can be used.<br/>
E.g. 'Knife' for cutting, 'Hammer' for pounding, etc.<br/>

### TookKind
`None`, `HackSaw`, `Hatchet`, `Hammer`, `Knife`


## DegradeOnUse
*float* <br/>
How many condition points per use does this tool item lose?<br/>
Certains actions have their own time driven degrade value, <br>
e.g. DegradePerHourCrafting, which applies only for those actions.<br/>


## Usage
*ToolUsage* <br/>
Can this item be used for crafting, repairing or both?<br/>

### ToolUsage
`All`, `CraftOnly`, `RepairOnly`


## SkillBonus
*int* <br/>
Bonus to the relevant skill when using this item. <br>
E.g. the sewing kit gives a bonus of +20.<br/>


## CraftingTimeMultiplier 
*float* <br/>
Multiplier for crafting and repair times. Represents percent. <br>
0% means 'finishes instantly'; 100% means 'same time as without tool'.<br/>


## DegradePerHourCrafting
*float* <br/>
How many condition points does the tool degrade while being used for crafting?<br/>


## BreakDown
*bool* <br/>
Can this tool be used to break down items? If not enabled, the other settings in this section will be ignored.<br/>


## BreakDownTimeMultiplier 
*float* <br/>
Multiplier for the time required to break down an item.<br/>
Represents percent. 0% means 'finishes instantly'; 100% means 'same time as without tool'.<br/>


## ForceLocks
*bool* <br/>
Can this tool item be used to open locked containers? If not enabled, the other settings in this section will be ignored.<br/>


## ForceLockAudio
*string* <br/>
Sound to play while forcing a lock. Leave empty for a sensible default.<br/>


## IceFishingHole
*bool* <br/>
Can this tool item be used to clear ice fishing holes? If not enabled, the other settings in this section will be ignored.<br/>


## IceFishingHoleDegradeOnUse
*float* <br/>
How many condition points does the tool lose when completely clearing an ice fishing hole?<br/>


## IceFishingHoleMinutes
*int* <br/>
How many in-game minutes does it take to completely clear an ice fishing hole?<br/>


## IceFishingHoleAudio
*string* <br/>
Sound to play while clearing an ice fishing hole. Leave empty for a sensible default.<br/>


## CarcassHarvesting
*bool* <br/>
Can this tool item be used to harvest carcasses? If not enabled, the other settings in this section will be ignored.<br/>


## MinutesPerKgMeat
*int* <br/>
How many in-game minutes does it take to harvest one kg of unfrozen meat?<br/>


## MinutesPerKgFrozenMeat
*int* <br/>
How many in-game minutes does it take to harvest one kg of frozen meat?<br/>


## MinutesPerHide
*int* <br/>
How many in-game minutes does it take to harvest one hide?<br/>


## MinutesPerGut
*int* <br/>
How many in-game minutes does it take to harvest one gut?<br/>


## DegradePerHourHarvesting
*float* <br/>
How many condition points does the tool degrade while being used for harvesting carcasses?<br/>


## StruggleBonus
*bool* <br/>
Can this tool item be used during a struggle with wildlife? If not enabled, the other settings in this section will be ignored.<br/>



## DamageMultiplier 
*float* <br/>
Multiplier for the damage dealt.<br/>


## FleeChanceMultiplier 
*float* <br/>
Multiplier for the chance the animal will flee (breaking the struggle before the 'struggle bar' is filled).<br/>


## TapMultiplier 
*float* <br/>
Multiplier for the amount of the 'struggle bar' that is filled with each hit.<br/>


## CanPuncture
*bool* <br/>
Can this tool cause a puncture wound? If enabled, this will cause the animal to bleed out.<br/>


## BleedoutMultiplier 
*float* <br/>
iplier for the time it takes the animal to bleed out after receiving a puncture wound.<br/><br/>




# Template
```
{
    "ModCookableComponent" : {
                            "DisplayNameLocalizationId" : "GAMEPLAY_HotCocoaBox",
                            "DescriptionLocalizatonId" : "GAMEPLAY_HotCocoaBoxDescription",
                            "InventoryActionLocalizationId" : "GAMEPLAY_HotCocoaBoxAction",
                            "WeightKG": 0.32,
                            "DaysToDecay" : 0,
                            "MaxHP" : 100,
                            "InitialCondition" : "Random",
                            "InventoryCategory" : "Food",
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

                            "Cooking" : true,
                            "CookingMinutes" : 13,
                            "CookingUnitsRequired" : 1,
                            "CookingWaterRequired" : 0.25,
                            "CookingResult" : "GEAR_HotCocoaCup",
                            "BurntMinutes" : 30,
                            "Type" : "Liquid",
                            "CookingAudio" : "",
                            "StartCookingAudio" : ""
                        }
}
```

# Parameters

This component, like the others, uses all the parameters from the [[Generic Component Documentation]].

## Cooking
*bool*<br/>
Can this be cooked/heated? If not enabled, the other settings in this section will be ignored.

## CookingMinutes
*int*<br/>
How many in-game minutes does it take to cook/heat this item?

## CookingUnitsRequired
*int*<br/>
How many units of this item are required for cooking?

## CookingWaterRequired
*float*<br/>
How many liters of water are required for cooking this item? Only potable water applies.

## CookingResult
*string*<br/>
Convert the item into this item when cooking completes. Leave empty to only heat the item without converting it.

## BurntMinutes
*int*<br/>
How many in-game minutes until this items becomes burnt after being 'cooked'?

## Type
`Meat`, `Grub`, or `Liquid`<br/>
What type of cookable is this? Affects where and how this item can be cooked.

## CookingAudio
*string*<br/>
Sound to use when cooking/heating the item. Leave empty for a sensible default.

## StartCookingAudio
*string*<br/>
Sound to use when putting the item into a pot or on a stove. Leave empty for a sensible default.
# Template

```
{
    "ModGenericComponent": {
                                "DisplayNameLocalizationId" : "GAMEPLAY_SampleItem",
                                "DescriptionLocalizatonId" : "GAMEPLAY_SampleItemDescription",
                                "InventoryActionLocalizationId" : "GAMEPLAY_SampleItemAction",
                                "WeightKG": 0.1,
                                "DaysToDecay" : 0,
                                "MaxHP" : 100,
                                "InitialCondition" : "Perfect",
                                "InventoryCategory" : "Auto",
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
                                "InspectModel" : ""
                            }
}
```

# Parameters

All components use these parameters.

## DisplayNameLocalizationId
*string*<br/>
Localization key to be used for the in-game name of the item.

## DescriptionLocalizatonId
*string*<br/>
Localization key to be used for the in-game description of the item.

## InventoryActionLocalizationId
*string*<br/>
Localization key to be used for the 'Action' (e.g. 'Equip', 'Eat', ...) button in the inventory. The text is purely cosmetic and will not influcence the action the button triggers. Leave empty for a sensible default.

## WeightKG
*float*<br/>
The weight of the item in kilograms.

## DaysToDecay
*int*<br/>
The number of days it takes for this item to decay - without use - from 100% to 0%. Leave at 0 if the item should not decay over time.

## MaxHP
*float*<br/>
The number of hit points an item has. May affect the percent condition lost in a struggle.

## InitialCondition
`Random`, `Perfect`, `High`, `Medium`, or `Low`<br/>
The initial condition of the item when found or crafted.

## InventoryCategory
`Auto`, `Clothing`, `FirstAid`, `Firestarting`, `Food`, `Material`, or `Tool`<br/>
The inventory category to be used for this item. Leave at `Auto` for a sensible default.

## PickUpAudio
*string*<br/>
Sound to play when the item is picked up.

## PutBackAudio
*string*<br/>
Sound to play when the item is dropped.

## StowAudio
*string*<br/>
Sound to play when the item is holstered.

## WornOutAudio
*string*<br/>
Sound to play when the item wore out during an action.

## InspectOnPickup
*bool*<br/>
Will the item be inspected when picked up? If not enabled, the item will go straight to the inventory.

## InspectDistance
*float*<br/>
Distance from the camera during inspect.

## InspectAngles
*3 float numbers in an array*<br/>
Each vector component stands for a rotation by the given degrees around the corresponding axis.

## InspectOffset
*3 float numbers in an array*<br/>
Offset from the center during inspect.

## InspectScale
*3 float numbers in an array*<br/>
Scales the item during inspect.

## NormalModel
*string*<br/>
Model to show when not inspecting the item. Leave empty to have the normal model and inspect model be the same.

## InspectModel
*string*<br/>
Model to show when inspecting the item. Leave empty to have the normal model and inspect model be the same.

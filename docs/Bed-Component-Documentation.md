# Template
```
{
    "ModBedComponent" : {
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
                            
                            "ConditionGainPerHour" : 1,
                            "AdditionalConditionGainPerHour" : 0,
                            "WarmthBonusCelsius" : 0,
                            "DegradePerHour" : 0,
                            "BearAttackModifier" : 0,
                            "WolfAttackModifier" : 0,
                            "OpenAudio" : "",
                            "CloseAudio" : "",
                            "PackedMesh" : "",
                            "UsableMesh" : ""
    }
}
```

# Parameters

This component, like the others, uses all the parameters from the [[Generic Component Documentation]].

## ConditionGainPerHour
*float*<br/>
How many condition points are restored per hour by sleeping in this bed?<br/>
This is the base rate and applied for the first hour.<br/>
The second and following hours will benefit from 'AdditionalConditionGainPerHour'.

## AdditionalConditionGainPerHour
*float*<br/>
Additionally restored condition points restored per hour.<br/>
The n-th hour of sleeping gives (n - 1) * AdditionalConditionGainPerHour additional health points.

## WarmthBonusCelsius
*float*<br/>
Warmth bonus of the bed in °C.

## DegradePerHour
*float*<br/>
How much condition does this bed item lose per hour of use?

## BearAttackModifier
*float*<br/>
Modifier for the chance of bear encounters during sleep. <br/>
Positive values decrease the chance; negative values increase the chance.

## WolfAttackModifier
*float*<br/>
Modifier for the chance of wolf encounters during sleep. <br/>
Positive values decrease the chance; negative values increase the chance.

## OpenAudio
*string*<br/>
Sound to be played when beginning to sleep in this bed. <br/>
Leave empty for a sensible default.

## CloseAudio
*string*<br/>
Sound to be played when ending to sleep in this bed. <br/>
Leave empty for a sensible default.

## PackedMesh
*string*<br/>
Optional game object to be used for representing the bed in a 'packed' state.

## UsableMesh
*string*<br/>
Optional game object to be used for representing the bed in a 'usable' state.
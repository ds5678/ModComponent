# Template
```
{
    "ModPowderComponent": {
                                "DisplayNameLocalizationId" : "GAMEPLAY_SampleItem",
                                "DescriptionLocalizatonId" : "GAMEPLAY_SampleItemDescription",
                                "InventoryActionLocalizationId" : "",
                                "WeightKG": 0,
                                "DaysToDecay" : 0,
                                "MaxHP" : 100,
                                "InitialCondition" : "Perfect",
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

                                "PowderType" : "Gunpowder",
                                "CapacityKG" : 0.2,
                                "ChanceFull" : 75
                            }
}
```

# Parameters

This component, like the others, uses all the parameters from the [[Generic Component Documentation]].

## PowderType
`Gunpowder`<br/>
The type of powder this container holds. `Gunpowder` is the only option right now.

## CapacityKG
*float*<br/>
The maximum weight this container can hold.

## ChanceFull
*float*<br/>
The percent probability that this container will be found full.
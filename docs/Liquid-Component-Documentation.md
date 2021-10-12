# Template
```
{
    "ModLiquidComponent": {
                                "DisplayNameLocalizationId" : "GAMEPLAY_MetalWaterBottle",
                                "DescriptionLocalizatonId" : "GAMEPLAY_MetalWaterBottleDescription",
                                "InventoryActionLocalizationId" : "",
                                "WeightKG": 0,
                                "DaysToDecay" : 0,
                                "MaxHP" : 100,
                                "InitialCondition" : "Perfect",
                                "InventoryCategory" : "Food",
                                "PickUpAudio" : "",
                                "PutBackAudio" : "",
                                "StowAudio" : "Play_InventoryStow",
                                "WornOutAudio" : "",
                                "InspectOnPickup" : true,
                                "InspectDistance" : 0.4,
                                "InspectAngles" : [0, 0, 0],
                                "InspectOffset" : [0, -0.12, 0],
                                "InspectScale" :  [1, 1, 1],
                                "NormalModel" : "",
                                "InspectModel" : "",

                                "LiquidType" : "Water",
                                "LiquidCapacityLiters" : 0.75,
                                "RandomizedQuantity" : false,
                                "LiquidLiters" : 0.75
                            }
}
```

# Parameters

This component, like the others, uses all the parameters from the [[Generic Component Documentation]].

## LiquidType
`Water` or `Kerosene`<br/>
What type of liquid does this container hold?

## LiquidCapacityLiters
*float*<br/>
The maximum capacity (in liters) of the container.

## RandomizedQuantity
*bool*<br/>
Should the amount be randomized?

## LiquidLiters
*float*<br/>
The initial amount of liquid this container contains. Does nothing if the quantity is randomized.
# Template
```
{
    "ModGenericEquippableComponent" : {
                            "DisplayNameLocalizationId" : "GAMEPLAY_SampleItem",
                            "DescriptionLocalizatonId" : "GAMEPLAY_SampleItemDescription",
                            "InventoryActionLocalizationId" : "GAMEPLAY_SampleItemAction",
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
                            "EquippingAudio" : ""
    }
}
```

# Parameters

This component, like the others, uses all the parameters from the [[Generic Component Documentation]].

## EquippedModelPrefab
*string*<br/>
The GameObject to be used for representing the item while it is equipped.<br/>
The position, rotation and scale of this prefab will be used for rendering. <br/>
Use the 'Weapon Camera' to tune the values.

## ImplementationType
*string*<br/>
The name of the type implementing the specific game logic of this item.<br/>
If this is an assembly-qualified name (Namespace.TypeName,Assembly) it will be loaded from the given assembly.<br/>
If the assembly is omitted (Namespace.TypeName), the type will be loaded from the first assembly that contains a type with the given name.<br/>
Leave empty if this item needs no special game logic.

## EquippingAudio
*string*<br/>
The audio that plays when this item is equipped.
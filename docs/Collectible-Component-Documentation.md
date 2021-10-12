# Template
```
{
    "ModCollectibleComponent": {
                                "DisplayNameLocalizationId" : "GAMEPLAY_SampleNote",
                                "DescriptionLocalizatonId" : "GAMEPLAY_SampleNoteDescription",
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

                                "HudMessageLocalizationId" : "GAMEPLAY_SampleNoteHudMessage",
                                "NarrativeTextLocalizationId" : "GAMEPLAY_SampleNoteNarrativeText",
                                "TextAlignment" : "Center"
                            }
}
```

# Parameters

This component, like the others, uses all the parameters from the [[Generic Component Documentation]].

## HudMessageLocalizationId
*string*<br/>
The localization id for the hud message displayed after this item is picked up.

## NarrativeTextLocalizationId
*string*<br/>
The localization id for the narrative content of the item.

## TextAlignment
`Automatic`, `Left`, `Center`, `Right`, and `Justified`<br/>
The alignment of the narrative text.
> Disclaimer: Not well-tested, but appears to be working

# Template
```
{
    "ModFirstAidComponent": {
                                "DisplayNameLocalizationId" : "GAMEPLAY_SampleItem",
                                "DescriptionLocalizatonId" : "GAMEPLAY_SampleItemDescription",
                                "InventoryActionLocalizationId" : "",
                                "InitialCondition" : "Perfect",
                                "WeightKG": 0.1,
                                "DaysToDecay" : 0,
                                "MaxHP" : 100,
                                "InventoryCategory" : "FirstAid",
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

                                "ProgressBarMessage" : "",
                                "RemedyText" : "",
                                "InstantHealing" : 0,
                                "FirstAidType" : "Antibiotics",
                                "TimeToUseSeconds" : 4,
                                "UnitsPerUse" : 1,
                                "UseAudio" : ""
                            }
}
```

# Parameters

This component, like the others, uses all the parameters from the [[Generic Component Documentation]].

## ProgressBarMessage
*string*<br/>
Localization key to be used to show a description text while using the item. Should be something like 'Taking Antibiotics', 'Applying Bandage', etc.

## RemedyText
*string*<br/>
Localization key to be used to indicate what action is possible with this item. E.g 'Take Antibiotics', 'Apply Bandage'. This is probably not used.

## InstantHealing
*int*<br/>
Amount of condition instantly restored after using this item.

## FirstAidType
`Antibiotics`, `Bandage`, `Disinfectant`, or `Painkiller`<br/>
What type of treatment does this item provide?

## TimeToUseSeconds
*int*<br/>
Time in seconds to use this item. Most vanilla items use 2 or 3 seconds.

## UnitsPerUse
*int*<br/>
How many items are required for one dose or application?

## UseAudio
*string*<br/>
Sound to play when using the item.
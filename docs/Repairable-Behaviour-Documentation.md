# Template
```
{
    "ModRepairableBehaviour" : {
                                "Audio" : "",
                                "Minutes" : 10,
                                "Condition" : 20,
                                "RequiredTools" : ["GEAR_SimpleTools","GEAR_HighQualityTools"],
                                "MaterialNames" : ["GEAR_ScrapMetal"],
                                "MaterialCounts" : [1]
                            }
}
```

# Parameters

> Note: cannot be used without a [Component](Basic-Information-about-Components.md).

## Audio
*string*<br/>
The audio to play while repairing

## Minutes
*int*<br/>
How many in-game minutes does it take to repair this item?

## Condition
*int*<br/>
How much condition does repairing restore?

## RequiredTools
*Array of string text*<br/>
The name of the tools suitable for repair. At least one of those will be required for repairing. Leave empty, if this item should be repairable without tools.

## MaterialNames
*Array of string text*<br/>
The names of the materials required for repair

## MaterialCounts
*Array of int numbers*<br/>
The number of the materials required for repair
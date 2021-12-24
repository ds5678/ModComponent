# Template
```
{
    "ModHarvestableBehaviour": {
                                "Audio" : "",
                                "Minutes" : 10,
                                "YieldCounts" : [1],
                                "YieldNames" : ["GEAR_ScrapMetal"],
                                "RequiredToolNames" : []
                            }
}
```

# Parameters

> Note: cannot be used without a [Component](Basic-Information-about-Components.md).

## Audio
*string*<br/>
The audio to play while harvesting.

## Minutes
*int*<br/>
How many in-game minutes does it take to harvest this item?

## YieldCounts
*Array of int numbers*<br/>
The numbers of each Gear Item that harvesting will yield.

## YieldNames
*Array of string text*<br/>
The names of the Gear Items that harvesting will yield.

## RequiredToolNames
*Array of string text*<br/>
The names of the ToolItems that can be used to harvest. Leave empty for harvesting by hand.
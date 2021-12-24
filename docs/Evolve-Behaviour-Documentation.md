> Disclaimer: Not tested

# Template
```
{
    "ModEvolveBehaviour" : {
                            "TargetItemName" : "GEAR_GutDried",
                            "EvolveHours" : 120,
                            "IndoorsOnly" : true
                        }
}
```

# Parameters

> Note: cannot be used without a [Component](Basic-Information-about-Components.md).

## TargetItemName
*string*<br/>
Name of the item into which this item will. E.g. 'GEAR_GutDried'

## EvolveHours
*int*<br/>
How many in-game hours does this item take to evolve from 0% to 100%?

## IndoorsOnly
*bool*<br/>
Does this item only evolve when it is stored indoors?
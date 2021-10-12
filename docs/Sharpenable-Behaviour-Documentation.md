> Disclaimer: Not tested

# Template
```
{
    "ModSharpenableComponent" : {
                                "Audio" : "",
                                "MinutesMin" : 5,
                                "MinutesMax" : 20,
                                "ConditionMin" : 3.3,
                                "ConditionMax" : 9.9,
                                "Tools" : ["GEAR_SharpeningStone"]
                                }
}
```

# Parameters

> Note: cannot be used without a [[Component|Basic Information about Components]].

## Audio
*string*<br/>
The sound to play while sharpening. Leave empty for a sensible default.

## MinutesMin
*int*<br/>
How many in-game minutes does it take to sharpen this item at minimum skill.

## MinutesMax
*int*<br/>
How many in-game minutes does it take to sharpen this item at maximum skill.

## ConditionMin
*float*<br/>
How much condition is restored to this item at minimum skill.

## ConditionMax
*float*<br/>
How much condition is restored to this item at maximum skill.

## Tools
*Array of string text*<br/>
Which tools can be used to sharpen this item, e.g. 'GEAR_SharpeningStone'. Leave empty to make this sharpenable without tools.

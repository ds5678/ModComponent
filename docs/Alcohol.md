ModComponent introduces Alcohol as a game mechanic.

Food/drink items can contain alcohol (expressed as a percentage of their weight) and when consumed, that alcohol is consumed as well.

ModComponent tracks how much alhocol has been consumed and how fast it should be absorbed by the player character's "body". The speed is influenced by the food/drink item itself and the currently stored calories (less calories mean faster uptake). At the same time, the body is constantly breaking down alcohol, resulting in a simulated blood alcohol concentration.

## Effects

ModComponent tries to simulate some of the real-life effects of alcohol:

* Alcohol allows more of your warm blood to reach your skin.<br/>
This **helps preventing frostbite** but also results in an **increased risk for hypothermia**.

* Alcohol causes your body to lose more water, causing **faster dehydration**.

* Alcohol causes **increased fatigue**

* Starting at about 0.05%/0.5‰ blood alcohol concentration: **blurry vision**.

* Starting at about 0.1%/1‰ blood alcohol concentration: **staggering**.

* Effects reach their maximum levels at 0.25%/2.5‰ blood alcohol concentration.

## Console Commands

* `set_alcohol_permille`
* `set_alcohol_percent`
* `get_alcohol_permille`
* `get_alcohol_percent`
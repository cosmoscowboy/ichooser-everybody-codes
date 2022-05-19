# Keep Talking and Everybody Codes

A repository for showing how you write and think about code.
Used as a starting point for a good discussion about our job.


> I think this is a great idea, and I am curious to see how others (**even employees within iChoosr**) fared with completing the task. :thinking: My remarks to this readme page are quoted.


## Context

You probably came here because you have a second interview with us. In the second interview, we'd like to dive more into technology. We want to see how you write code, how you think and what you think is important when writing code. To do this we'd like to kickstart our discussion about this.

There are many ways to do this. Maybe you have open source projects that you'd like to share. Maybe you'd like to show some code from a previous project. Those would be fine, but maybe you don't have something like that (yet). Therefore, we have this repository: an exercise that can use as a starting point to write some code that we can use as a subject for our discussion.

You might wonder "Is this a _test_"?
The answer is very clear to us: **no**!
_It is something like a test_, but there are some important differences:


- It doesn't decide whether you get invited back (okay, unless you do something _really_ stupid). We already decided that you could come back when we sent you here. So you'll always get the chance to explain your choices.
- We want you to **timebox this to 2 to 4 hours**. It makes sense that you don't get to finish all in that time. Focus on showing what you think is important, and explain those choices to us.
- You can fail a 'test', but not this exercise, it's purely used as _input_ for our next talk.

## The exercise

The exercise consists of 3 parts in which you use the dataset you can find at [data/cameras-defb.csv](data/cameras-defb.csv).
You can chose a language and tech stack of your own choice (we are most familiar with C#, JavaScript, PHP, Java).


> I *eat, drink, sleep, repeat* C#, so this is my obvious choice for the task. :wink:


## CLI

Make a program or script that allows the use to search through a CLI a part of the camera _name_, for example:


> To run the CLI, open to the directory of the project 'Search', and type the following command:
`dotnet run Search --name [Name of camera]`


## API

Serve the data through a Web-API, such that a web application can fetch the data.


> Start a new instance of the project 'Search.WebApi' and you are directed to the Swagger UI. You can search on the name of the cameras, of leave the value empty to get all cameras.


## Web application

The first part consists of retrieving the data from the API from the previous step.
Render the data spread over the four columns given in [code/index.html](code/index.html).
The spreading of the data needs to follow the following rules based on the `number` of the camera:

1. If `number` is divisible by 3, then it should go in the first column.
2. If `number` is divisible by 5, then it should go in the second column.
3. If `number` is divisible by 3 and divisible by 5, then it should go in the third column.
4. If `number` is not divisible by 3 and is not  divisible by 5, then it should go in the last column.


> Start a new instance of the project 'Search.Web' and you are directed to the 'Index' page. I wasn't able to get the original [code/index.html](https://github.com/iChoosr-BVBA/everybody-codes/blob/master/code/index.htmlhtml) page working, as the 'ASP.NET Core Web App' already contains an Index.cshtml page (based on the project template). I did, however, use part of the styling and scripting by placing it in the 'Pages | Shared | _Layout.cshtml' page.


The second part consists of showing the camera locations as markers on a map. 

Show _all_ cameras in the `div` with id _map_ in the given [code/index.html](code/index.html).
You can use your preferred Map-tool, but if you don't have a preference, then we suggest that you look at using a combi of [Leaflet JavaScript library](https://leafletjs.com/examples/quick-start/) with the map pictures via a [free MapBox account](https://www.mapbox.com/studio/account/tokens/) (which uses OpenStreetMap). In that case, [the coordinates 52.0914 by 5.1115 will give you a centered view of Utrecht](https://www.openstreetmap.org/#map=14/52.0914/5.1115).

> * I was very keen to use the 'Leaflet JavaScript library', up until the point where I had to enter my credit card for a 'free' mapbox account. I didn't feal reassured after reading **"Get started for free | Only pay for what you use"**. Perhaps I can finish off the second part when I speak with the enthusiastic iChoosr techies, and can you one of their 'access tokens'.


## Wut? Open Source?!

Yes, this exercise is open source!

"_Aren't you afraid people will 'cheat'?_"
Well, the probability for that wouldn't be much higher that when we would _e-mail_ you an exercise.
We value being open en honest to eachother, and trust that you'll do this exercise by yourself.
Besides that, things will show when we actually discuss what you made...


> This was a nice introductory course to .NET 6.0 and the latest version of a ASP.NET Core Web App and Razor pages. In terms of my current positions, I have been working with .NET Core 2.1 WebApi apps, and 'old school' .NET Framework MVC 5.0 (and its Razor syntax). The latest Razor syntax is much cleaner the previous versions, I am very excited to learn more about it and hope to start using it.


"_Do you accept.... pull requests?_"
Of course!
But it isn't the primary goal, so rather focus on the exercise itself.
Should you have - after the exercise - some suggestions for how to improve, or would you like to correct a typo: keep them PR's coming!
Or open an issue if you have questions.

## About the title

Huh?

> Keep Talking and Everybody Codes

"_What's that?_"
Sorry, it's a pun related to [a cool game](http://www.keeptalkinggame.com/).


> Personally I prefer escape room games, and card games such as [Weerwolven van Wakkerdam](https://nl.wikipedia.org/wiki/Weerwolven_van_Wakkerdam). :fox_face:


## License and Copyright

See [LICENSE.txt](LICENSE.txt) for complete details.
In short, you may maintain a fork e.g. with a translation at your leasure, as long as you honor the terms of the licenses.
For merely changing (translating) the README this simply means you should (a) attribute the work to [the original repository from Infi](https://github.com/infi-nl/everybody-codes) and (b) publish your modified version under the same terms to be used by others.
_Sharing is caring!_ 🧡😊
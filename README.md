Bubble Interview Challenge

I chose to implement the Bubble Packing program in C# as a generic WinForms app w/in Visual Studio 2015 targeting the .NET Framework 4.5.2.

Binaries
The debug exe can be found at BubbleChallenge\BubbleApp\bin\Debug\BubbleApp.exe.

Build instructions
To build the app unzip the BubbleChallenge.zip file and crack open the BubbleChallenge\BubbleChallenge.sln file using Visual Studio 2015.  From the Build menu select Build BubbleApp or press Shift + F6.  Upon Successful build you can start the app by pressing Ctrl + F5.  Alternatively, you can simply run BubbleChallenge\BubbleApp\bin\Debug\BubbleApp.exe

Running the App
When the app starts you will be presented with a simple winforms app.  Click File-->Open and choose one of the pre-built BubbleDef.csv files or navigate you one of your own *.csv files if you have one.  (The BubbleDev.csv is defined as per the challenge description and BubbleDef2.csv extends the aformentioned with a couple more bubbles.)  Depending on your bubble def you may need to resize the app to view all the bubbles.  To render another bubble def simple repeat the steps and choose another file.

Web Refs:
Thanks to Matthias Shapiro for his writeup on Circle Packing located at:
http://matthiasshapiro.com/2015/02/09/circle-packing-algorithm-in-c-xaml/

As noted in the web ref above the heavy lifting is performed in the Circle.cs and CirclePacker.cs file. I pirated these files in addition to a Vector struct as a starting point.  I had to make adjustments Circle.cs and CirclePacker.cs to meet the challenge requiremnts and make them compatible with a WinForms app instead of in a WPF-Universal app.

Original files located here:
https://github.com/matthiasxc/CirclePack_CSharp/blob/master/CirclePacker_CSharp/CirclePacker_CSharp.Shared/CirclePacker/Circle.cs

https://github.com/matthiasxc/CirclePack_CSharp/blob/master/CirclePacker_CSharp/CirclePacker_CSharp.Shared/CirclePacker/CirclePacker.cs

https://github.com/matthiasxc/CirclePack_CSharp/blob/master/CirclePacker_CSharp/CirclePacker_CSharp.Shared/MathHelpers/Vector2.cs

How does it work?
When the app begins to read a BubbleDef file it will create a single CirclePacker object and start reading the chosen file line by line.  For each line in the Bubble def file the app instantiates a corresponding Circle object and adds to the CirclePacker's list of Circles.  After the entire file has been read the app commenses the process of packing the bubbles until tangent.  Packing of the Circles is performed using a variation of the circle packing theorem (also known as the Koebe-Andreev-Thurston theorem) which describes the possibile tangency relations between circles in the place whose interiors are disjoint (See https://en.wikipedia.org/wiki/Circle_packing_theorem for the gory details).  The CirclePacker iterates through it's list of circles repetitively until the circles reach tangency.   For every iteration the CirclePacker first sorts the list of circles by it's distance to the center of the drawing canvas in descending order into a new sorted list.  Next it cycles thru the new sorted list in search of colliding circles.  When it finds a collision the x and y cordinates of the colliding circles are adjusted.  Next the CirclePacker iterates again over the "adjusted " sorted list to perform the repositioning relative to it's position on the drawing canvas.   This process is repeated over and over until each circle's x and y coordinates no longer change.  At this point the Circles have reached tangency are ready for rendered on the drawing canvas.   The resulting bubbles are rendered as solid 2D circles in a random color.  Additionally, each circle will have it's corresponding name rendered.

Note
During development and debugging of the app I found it pretty cool call the render() function after each iteration to see how the algorithm slowly adjusts the postion of each circle as it reaches tangency.  I considered leaving it this way, but later decided against it slowed the app considerably. 





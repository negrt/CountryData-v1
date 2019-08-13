# CountryDataSearchTool-v1
A C# desktop application that reads a country data JSON file and could then displays a target country data on its graphical user interface. This application uses a Country Dynamic Link Library to help store country data to the CPUs RAM. The country Dynamic Link Library is in a seperate respository. I created this application in Visual Studios 2017 using C# and XAML.

# What I learned
* How to store C# objects on a list using C# generics
* How to create XAML graphical user interfaces
* How to deserialize JSON files
* How to bind user interface and business logic
* How to use a open file dialog object

# Images and Description
The application reads country dataset JSON file, stores it into ram using a List of countries. The applicaiton then has the ability to search through the List of countries object for a Target Country application uses a Country Dynamic Link Library to help store country data to the CPUs RAM. When the country target is found, information related to the country will be displayed on the interface. The country Dynamic Link Library is in a seperate respository. I created this application in Visual Studios 2017 using C# and XAML.

## Images
#### Application on Start Up
![Image of Country-v1 Start Up](https://github.com/negrt/cv/blob/master/images/countryAppMainWindow.PNG?raw=true)

#### Open Countries JSON File Button - Pre Selection
![Image of Country Open JSON File Button](https://github.com/negrt/cv/blob/master/images/countryAppOpenFileDialog(pre).PNG?raw=true)

#### Open Countries JSON File Button - Post Selection (Filepath displayed in textbox)
![Image of Country Open File Dialog](https://github.com/negrt/cv/blob/master/images/countryOpenFileDialog(post).PNG?raw=true)

#### Find Country by Name Button - Target Found - Singapore
![Image of Country Target Found - Singapore](https://github.com/negrt/cv/blob/master/images/countryAppTargetFoundSingapore.PNG?raw=true)

#### Find Country by Name Button - Target Found - Canada
![Image of Country Target Found - Canada](https://github.com/negrt/cv/blob/master/images/countryAppTargetFoundCanada.PNG?raw=true)

# Notice
This application uses a separate Dynamic Link Library. Download CountryDynamicLinkLibrary repository to run this application appropriately.

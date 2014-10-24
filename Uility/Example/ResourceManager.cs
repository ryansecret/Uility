// This code example demonstrates the ResourceManager() 

// constructor and ResourceManager.GetString() method.


using System;
using System.Resources;
using System.Reflection;
using System.Threading;
using System.Globalization;

/*
Perform the following steps to use this code example:

Main assembly:
1) In a main directory, create a file named "rmc.txt" that 
contains the following resource strings:

day=Friday
year=2006
holiday="Cinco de Mayo"

2) Use the resgen.exe tool to generate the "rmc.resources" 
resource file from the "rmc.txt" input file.

> resgen rmc.txt

Satellite Assembly:
3) Create a subdirectory of the main directory and name the 
subdirectory "es-MX", which is the culture name of the 
satellite assembly.

4) Create a file named "rmc.es-MX.txt" that contains the 
following resource strings:

day=Viernes
year=2006
holiday="Cinco de Mayo"

5) Use the resgen.exe tool to generate the "rmc.es-MX.resources" 
resource file from the "rmc.es-MX.txt" input file.

> resgen rmc.es-MX.txt

6) Use the al.exe tool to create a satellite assembly. If the 
base name of the application is "rmc", the satellite assembly 
name must be "rmc.resources.dll". Also, specify the culture, 
which is es-MX.

> al /embed:rmc.es-MX.resources /c:es-MX /out:rmc.resources.dll 

7) Assume the filename for this code example is "rmc.cs". Compile 
rmc.cs and embed the main assembly resource file, rmc.resources, in 
the executable assembly, rmc.exe:

>csc /res:rmc.resources rmc.cs

8) Execute rmc.exe, which obtains and displays the embedded 
resource strings.
*/


class Sample
{
    public static void Main()
    {
        string day;
        string year;
        string holiday;
        string celebrate = "{0} will occur on {1} in {2}.\n";

        // Create a resource manager. The GetExecutingAssembly() method

        // gets rmc.exe as an Assembly object.

        
        ResourceManager rm = new ResourceManager("rmc",
                                 Assembly.GetExecutingAssembly());

        Assembly assembly = Assembly.GetExecutingAssembly();
        var resources= assembly.GetManifestResourceNames();

        // Obtain resources using the current UI culture.

        Console.WriteLine("Obtain resources using the current UI culture.");

        // Get the resource strings for the day, year, and holiday 

        // using the current UI culture. Use those strings to 

        // display a message.

        
        day = rm.GetString("day");
        year = rm.GetString("year");
        holiday = rm.GetString("holiday");
        Console.WriteLine(celebrate, holiday, day, year);

        // Obtain the es-MX culture.

        CultureInfo ci = new CultureInfo("es-MX");
         // Get the resource strings for the day, year, and holiday 

        // using the specified culture. Use those strings to 

        // display a message. 


        // Obtain resources using the es-MX culture.

        Console.WriteLine("Obtain resources using the es-MX culture.");

        day = rm.GetString("day", ci);
        year = rm.GetString("year", ci);
        holiday = rm.GetString("holiday", ci);

        // ---------------------------------------------------------------

        // Alternatively, comment the preceding 3 code statements and 

        // uncomment the following 4 code statements:

        // ----------------------------------------------------------------


        // Set the current UI culture to "es-MX" (Spanish-Mexico).

        //    Thread.CurrentThread.CurrentUICulture = ci;


        // Get the resource strings for the day, year, and holiday 

        // using the current UI culture. Use those strings to 

        // display a message. 

        //    day  = rm.GetString("day");

        //    year = rm.GetString("year");

        //    holiday = rm.GetString("holiday");

        // ---------------------------------------------------------------


        // Regardless of the alternative that you choose, display a message 

        // using the retrieved resource strings.

        Console.WriteLine(celebrate, holiday, day, year);
    }
}

/*
This code example produces the following results:

>rmc
Obtain resources using the current UI culture.
"5th of May" will occur on Friday in 2006.

Obtain resources using the es-MX culture.
"Cinco de Mayo" will occur on Viernes in 2006.

*/


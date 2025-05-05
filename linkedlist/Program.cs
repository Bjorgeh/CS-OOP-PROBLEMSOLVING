using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Transactions;
using System.Text.Json;

//Creating objects
Console.WriteLine("Creating Objects...");
Gps g1 = new Gps("05.05.2025","123x321y231z","41C","12%");
Gps g2 = new Gps("06.05.2025","223x321y231z","12C","22%");
Gps g3 = new Gps("07.05.2025","323x321y231z","53C","32%");
Gps g4 = new Gps("08.05.2025","423x321y231z","24C","42%");
Gps g5 = new Gps("09.05.2025","523x321y231z","35C","52%");

//Linking objects
Console.WriteLine("Linking Objects...");
g1.nextObj = g2;
g1.prevObj = null; // No previous object for the first element

g2.nextObj = g3;
g2.prevObj = g1;

g3.nextObj = g4;
g3.prevObj = g2;

g4.nextObj = g5;
g4.prevObj = g3;

g5.nextObj = null; // No next object for the last element
g5.prevObj = g4;


// Gets all ids from a input object to the last linked object.
static void runThroughLinkedListForward(Gps obj)
{
    Gps current = obj; //Sets input object as current

    while(current is not null) //As long as the object is not null
    {
        Console.WriteLine(current.num); //print object id
        Console.WriteLine(current.allData);
        current = current.nextObj; //set current object to its next.

    }
}

//This removes a object form the linked list
g3.remove(); 

//Printing list forwards from given object
Console.WriteLine("\nRunning through list forwards...");
runThroughLinkedListForward(g1); 


// Gets all ids from a input object to the last linked object.
static void runThroughLinkedListBackwards(Gps obj)
{
    Gps current = obj; //Sets input object as current

    while(current is not null) //As long as the object is not null
    {
        Console.WriteLine(current.num); //print object id
        Console.WriteLine(current.allData);
        current = current.prevObj; //set current object to its next.

    }
}

// Printing list backwards from given object
Console.WriteLine("\nRunning through list backwards...");
runThroughLinkedListBackwards(g5);




// Class definition
class Gps
{
    /*
        Sets private variables for the Gps class.
    */
    private int aId;
    private string aDate;
    private string aLocation;
    private string aTemperature;
    private string aMoisture;

    private static int counter = 1; // Sets ID to objects starting from 1

    /*
        Links the objects together in a list.
    */
    protected Gps next;
    protected Gps prev;

    // Getters and Setters for the linked objects
    public Gps nextObj
    {
        set { next = value; }
        get { return next; }
    }

    public Gps prevObj
    {
        set { prev = value; }
        get { return prev; }
    }

    // Getters for the private variables
    public int num
    {
        get { return aId; }
    }

    //Get / Set for Date
    public string date
    {
        get { return aDate; }
        set {aDate = value;}
    }

    //Get / Set for Location
    public string location
    {
        get { return aLocation; }
        set { aLocation = value; }
    }

    //Get / Set for Temperature
    public string temperature
    {
        get { return aTemperature; }
        set { aTemperature = value; }
    }

    //Get / Set for moisture
    public string moisture
    {
        get { return aMoisture; }
        set { aMoisture = value; }
    }

    //remove object
    public void remove()
    {
            this.next.prev = this.prev;
            this.prev.next = this.next;

    }

    //This creates a new object readable by json -> to return a json string
    public object allJsonData()
    {
        return new 
        {
            moisture = this.moisture,
            date = this.date,
            location = this.location,
            temperature = this.temperature
        };
    }

    //Returns a Json string of the object
    public string allData
    {
        get
        {
            string jsonString = JsonSerializer.Serialize(this.allJsonData());
            return jsonString;

        }
    }

    //Constructor for the Gps class
    public Gps(string date = "null", string location = "null", string temp = "null", string moist = "null")
    {
        aId = counter;
        aDate = date;
        aLocation = location;
        aTemperature = temp;
        aMoisture = moist;

        counter++; // Increments the ID for the next object
    }
}

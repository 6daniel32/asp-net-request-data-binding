using System.Text.RegularExpressions;

namespace asp_net_request_data_binding.app.Types;

//Structures allow you to define complex types
//They are value-oriented and implement value equality
//No inheritance but you can still implement interfaces
//Mutable by default

//The struct will use heap or stack depending on where the struct is created.
//If created on a method (which uses the callstack), it will use the stack, 
//if created on a class, because the class uses the heap, and so will the struct

//Memory efficient when they rely on the stack, not the heap --> https://endjin.com/blog/2022/07/understanding-the-stack-and-heap-in-csharp-dotnet
//- The stack is the data structure that keeps track of the "call stack" 
//in C#. For instance, when you have a thread executing Program.cs, which executes function A,
//function A executes function B, they are added to the stack in that order and removed
//in reverse order (LIFO).
//- It also stores the disposable variables (the local variables) of the functions.

struct BuildingReference {

    public string ReferenceValue { get; }

    public BuildingReference(string referenceValue)
    {
        ReferenceValue = referenceValue;
    }

    public static bool TryParse(string? input, out BuildingReference result)
    {
        if(input == null || !Regex.IsMatch(input, @"^[rR]{1}[0-9]+$")) {
            result = default;
            return false;
        }

        result = new BuildingReference(input);
        return true;
    }
}
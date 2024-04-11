namespace asp_net_request_data_binding.app.Types;

//Value oriented + can use stack + implements vale equality
//Can't use inheritance but can implement interfaces
//Inmutable by default (properties are readonly by default,
//only modifiable by setters)
//Can use with expressions
/* public record struct Person(string Name, int Age);
 *
 * var john = new Person("John", 30);
 * var olderJohn = john with { Age = 31 }; */

record struct SeatNumber(int number) 
{
    public static bool TryParse(string? input, out SeatNumber result)
    {
        //Span = struct representing an arbitrary region of memory. Used for efficiency
        if(!int.TryParse(input.AsSpan(), out int intValue) || intValue < 1 || intValue > 59) {
            result = default;
            return false;
        }

        result = new SeatNumber(intValue);
        return true;
    }
}
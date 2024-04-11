using System.Text.RegularExpressions;

namespace asp_net_request_data_binding.app.Types;

// Using record class instead of class.
// This feature of the language is more convenient for this purpose.
// Think of records as the tool that the language gives you to encapsulate
// data. 
// Note: "record" and "record class" are synonyms. 
// Note2: records implement value equality
// Note3: records are immutable by default (properties are readonly by default,
//only modifiable by setters)
// Note4: records can use inheritance and implement interfaces
/* Note5: it can use the "with" keyword 
 * public record Person(string Name, int Age);
 *
 * var john = new Person("John", 30);
 * var olderJohn = john with { Age = 31 }; */
// Note6: records use the heap, like classes

record NationalInsuranceNumber(string nino) 
{
    public static bool TryParse(string? input, out NationalInsuranceNumber result)
    {
        if(input == null || !Regex.IsMatch(input, @"^(?!BG)(?!GB)(?!NK)(?!KN)(?!TN)(?!NT)(?!ZZ)(?:[A-CEGHJ-PR-TW-Z][A-CEGHJ-NPR-TW-Z])(?:\s*\d\s*){6}([A-D]|\s)$")) {
            result = default;
            return false;
        }

        result = new NationalInsuranceNumber(input.ToString());
        return true;
    }
}
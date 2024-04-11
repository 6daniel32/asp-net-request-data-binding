using asp_net_request_data_binding.app.Types;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () => "Hello World!");

/* 1. Binding of simple request variables. This parameters are primitive types and are
 * required in the route. */

//Integer binding
app.MapGet(
    "/required-route-number/{myNum}", 
    (int myNum) => "This is the number from your route:  " + myNum
);

//String binding
app.MapGet(
    "/required-route-string/{myString}", 
    (string myString) => "This is the string from your route:  " + myString
);

/* 2. Now we have an example with an optional route parameter and a 
 * route parameter with a default value */

//Optional parameter
app.MapGet(
    "/optional-route-number/{myNum?}", 
    delegate(int? myNum) {
        if(myNum.HasValue)
            return "This is the number from your route:  " + myNum;
        else
            return "No number was provided";
    }
);

//Default value for parameter
app.MapGet(
    "/default-route-string/{myString=Default}", 
    (string myString) => "This is the string from your route:  " + myString
);

/* 3. Some more examples with parameters from the query string */

//Required string from query string. Example: /required-string-in-query-string?myString=string
app.MapGet(
    "/required-string-in-query-string", 
    (string myString) => "This is the string from your query string:  " + myString
);

//Optional integer from query string
app.MapGet(
    "/optional-number-in-query-string", 
    delegate(int? myNum) {
        if(myNum.HasValue)
            return "This is the number from your query string:  " + myNum;
        else
            return "No number was provided";
    }
);

//Default value for boolean parameter from the query string
app.MapGet(
    "/default-bool-in-query-string", 
    (bool myBool = true) => "This is the boolean from your query string:  " + myBool
);

/* 4. Declaring the explicit source for each request parameter. 
 * I expect a mandatory integer in the route, a string with 
 * a default value in the query string and a bool with a default value in the header*/

app.MapGet(
    "/explicit-source/{myNum}", 
    (
        [FromRoute] int myNum, 
        [FromQuery] string myString = "Default",
        [FromHeader(Name = "HeaderParam")] bool headerParam = true
    ) => $"Route: {myNum}, Query: {myString}, Header: {headerParam}"
);

/* 5. Parsing primitive types into custom types */
// Using a class which implements TryParse.
// Primitive types in C# also implement this static function.
// Example: http://localhost:5124/load/0x23f
app.MapGet("/load/{pointer}", delegate(Pointer pointer) {
    return $"Received {pointer.Address}";
});

// Using a struct which implements TryParse. (explanation in declaration)
// Example: http://localhost:5124/building/r12345
app.MapGet("/building/{reference}", delegate(BuildingReference reference) {
    return $"Received {reference.ReferenceValue}";
});

// Using a record class which implements TryParse. (explanation in declaration)
// Example: http://localhost:5124/user/AB112233B
app.MapGet("/user/{nino}", delegate(NationalInsuranceNumber nino) {
    return $"Received {nino}";
});

// Using a record struct which implements TryParse. (best of both worlds)
// Example: http://localhost:5124/seat/4
app.MapGet("/seat/{seatNumber}", delegate(SeatNumber seatNumber) {
    return $"Received {seatNumber}";
});

// Using a readonly record struct which implements TryParse. 
// (prevents modifications of the original source)
// Example: http://localhost:5124/table/6
app.MapGet("/table/{tableNumber}", delegate(TableNumber tableNumber) {
    return $"Received {tableNumber}";
});

/* 6. Parsing request body (use Postman) */
app.MapPost("/patient/store" , ([FromBody] PatientData patientData) => $"Received {patientData}");

app.Run();

readonly record struct PatientData(int Id, string Name, string Disease);
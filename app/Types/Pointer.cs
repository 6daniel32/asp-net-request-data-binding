using System.Text.RegularExpressions;

namespace asp_net_request_data_binding.app.Types;

// Not the ideal way to implement it.
// You would do something similar in PHP if
// you wanted to have typed request variables (What
// about typescript types?).
// Uses the heap, which stores data that needs to outlive methods
// and is deleted by the GC when it's no longer needed. The heap is slower than the stack 
// because it uses dynamic memory allocation (usually, with a tree-based algorithm), 
//which is more complex than just pulling the top of the stack.
class Pointer 
{
    public string Address { get; }

    public Pointer(string address)
    {
        Address = address;
    }

    public static bool TryParse(string? input, out Pointer result)
    {
        if(input == null || !Regex.IsMatch(input, @"^0[xX]{1}[0-9A-Fa-f]+$")) {
            result = default;
            return false;
        }

        result = new Pointer(input);
        return true;
    }
}
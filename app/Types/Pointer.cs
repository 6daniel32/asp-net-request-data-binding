using System.Text.RegularExpressions;

namespace asp_net_request_data_binding.app.Types
{
    // Not the ideal way to implement it.
    // You would do something similar in PHP if
    // you wanted to have typed request variables (What
    // about typescript types?).
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
}
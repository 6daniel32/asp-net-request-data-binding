namespace asp_net_request_data_binding.app.Types;

//Same as record struct but the properties can't even be modified by setters
//The inmutability is stronger here

readonly record struct TableNumber(int number) 
{
    public static bool TryParse(string? input, out TableNumber result)
    {
        if(!int.TryParse(input.AsSpan(), out int intValue) || intValue < 1 || intValue > 12) {
            result = default;
            return false;
        }

        result = new TableNumber(intValue);
        return true;
    }
}
class KeyWord : Node{
    public static string[] Keywords = {"let", "in", "function"};
    private string _value;
    public string value => _value;
    public KeyWord(string value) => _value = value;
}
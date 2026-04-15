public static class MySecurityHasher
{
    public static string HashPassword(string password)
    {
        if (string.IsNullOrEmpty(password)) return password;

     
        char[] charArray = password.ToCharArray();
        Array.Reverse(charArray);
        string reversed = new string(charArray);

    
        string hashed = "";
        int saltCounter = 90;

        foreach (char c in reversed)
        {
            hashed += c; 
            hashed += saltCounter.ToString(); 
            saltCounter++;
        }

        return hashed;
    }
}
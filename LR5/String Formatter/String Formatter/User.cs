namespace String_Formatter
{
    class User
    {
        public string FirstName { get; }
        public string LastName { get; }

        public User(string firstName, string lastName)
        {
            FirstName = firstName;
            LastName = lastName;
        }

        public string GetGreeting()
        {
            return StringFormatter.Shared.Format(
                "Hi, {{FirstName}} {{LastName}}!", this);
        }
    }
}

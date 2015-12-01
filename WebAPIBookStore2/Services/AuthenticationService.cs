using System;
using System.Collections.Generic;

namespace WebAPIBookStore2.Services
{
    public class AuthenticationService
    {
        private readonly List<Tuple<string, string>> _storedCredentials;

        public AuthenticationService()
        {
            _storedCredentials = new List<Tuple<string, string>>();
            _storedCredentials.Add(new Tuple<string, string>("alberto", "password"));
        }

        public bool Authenticate(string username, string password)
        {
            var credentials = new Tuple<string, string>(username, password);
            return _storedCredentials.Contains(credentials);
        }
    }
}
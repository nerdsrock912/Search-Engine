using System.ComponentModel.DataAnnotations;

namespace SearchEngine
{
    /// <summary>
    /// The <see cref="EmailAddress"/> class is designed to hold the email address of a <see cref="Humanoid"/> instance.
    /// </summary>
    public class EmailAddress
    {
        /// <summary>
        /// Gets or sets the username of this instance.
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// Gets or sets the domain of this instance.
        /// </summary>
        public string Domain { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="EmailAddress"/> class using the specified email address.
        /// </summary>
        /// <param name="address">The string representation of an email address.</param>
        public EmailAddress(string address)
        {
            Username = address.Substring(0, address.IndexOf('@'));
            Domain = address.Substring(address.IndexOf('@') + 1, address.Length - Username.Length - 1);
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="EmailAddress"/> class with a username and domain as parameters.
        /// </summary>
        /// <param name="username">The user-defined part of an email address</param>
        /// <param name="domain">The domain of the company hosting the email address</param>
        public EmailAddress(string username, string domain)
        {
            Username = username;
            Domain = domain;
        }

        /// <summary>
        /// Determines if an email address string is valid.
        /// </summary>
        /// <param name="address">The string representation of an email address.</param>
        /// <param name="username">The username of the email address.</param>
        /// <param name="domain">The domain of the email address.</param>
        /// <returns>Returns true if the email address is valid. Otherwise, false.</returns>
        public static bool ParseEmail(string address, ref string username, ref string domain)
        {
            if (new EmailAddressAttribute().IsValid(address))
            {
                username = address.Substring(0, address.IndexOf('@'));
                domain = address.Substring(address.IndexOf('@') + 1, address.Length - username.Length - 1);
                return true;
            }
            return false;
        }

        /// <summary>
        /// Returns the string representation of the email address of this instance.
        /// </summary>
        /// <returns>Returns the string representation of the email address of this instance.</returns>
        public override string ToString() => Username + '@' + Domain;
    }
}

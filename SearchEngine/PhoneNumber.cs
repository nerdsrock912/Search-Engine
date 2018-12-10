using System.ComponentModel.DataAnnotations;

namespace SearchEngine
{
    /// <summary>
    /// The <see cref="PhoneNumber"/> is designed to hold the phone number of a <see cref="Humanoid"/> instance.
    /// </summary>
    public class PhoneNumber
    {
        /// <summary>
        /// Gets or sets the phone number of this instance.
        /// </summary>
        public string Number { get; set; }

        /// <summary>
        /// Initializes an instance of the <see cref="PhoneNumber"/> class using the specified phone number.
        /// </summary>
        /// <param name="number">The string representation of the phone number.</param>
        public PhoneNumber(string number)
        {
            Number = number;
        }

        /// <summary>
        /// Determines if a phone number string is valid.
        /// </summary>
        /// <param name="number">The string of the phone number to be validated.</param>
        /// <returns>Returns true if the phone number is valid. Otherwise, false.</returns>
        public static bool ParseNumber(string number) => new PhoneAttribute().IsValid(number);
    }
}

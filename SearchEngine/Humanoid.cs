using System;

namespace SearchEngine
{
    /// <summary>
    /// Holds the values of properties of a <see cref="Humanoid"/> instance.
    /// </summary>
    public enum HumanoidProp { NAME = 1, AGE, EMAIL, PHONE }

    /// <summary>
    /// The <see cref="Humanoid"/> class is designed to represent a human and its traits.
    /// </summary>
    public class Humanoid : IComparable<Humanoid>
    {
        /// <summary>
        /// Gets or sets the identification of this instance.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the age of this instance.
        /// </summary>
        public ushort Age { get; set; }

        /// <summary>
        /// Gets or sets the email address of this instance.
        /// </summary>
        public EmailAddress Email { get; set; }

        /// <summary>
        /// Gets or sets the phone number of this instance.
        /// </summary>
        public PhoneNumber Phone { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Humanoid"/> class.
        /// </summary>
        public Humanoid() {}

        /// <summary>
        /// Initializes a new instance of the <see cref="Humanoid"/> class using a specified name and age.
        /// </summary>
        /// <param name="name">The string representation of the identification of this instance.</param>
        /// <param name="age">The age of this instance.</param>
        public Humanoid(string name, ushort age)
        {
            Name = name;
            Age = age;
            SetEmail();
            SetPhone();
        }
        
        /// <summary>
        /// Initializes a new instance of the <see cref="Humanoid"/> class using a specified name, age, email address, and phone number.
        /// </summary>
        /// <param name="name">The name of the instance.</param>
        /// <param name="age">The age of the instance.</param>
        /// <param name="email">The email address of the instance.</param>
        /// <param name="phone">The phone number of the instance.</param>
        public Humanoid(string name, ushort age, string email, string phone)
        {
            Name = name;
            Age = age;
            Email = new EmailAddress(email);
            Phone = new PhoneNumber(phone);
        }

        /// <summary>
        /// Sets the name of this instance.
        /// </summary>
        public void SetName()
        {
            Console.Write("What is the humanoid's new name? ");
            Name = Console.ReadLine();
        }

        /// <summary>
        /// Sets the age of this instance.
        /// </summary>
        public void SetAge()
        {
            ushort age;
            Console.Write("What is the humanoid's new age? ");
            age = Convert.ToUInt16(Console.ReadLine());
            while (age < 0 || age > ushort.MaxValue)
            {
                Console.Write("Invalid age. Please try again: ");
                age = Convert.ToUInt16(Console.ReadLine());
            }
            Age = age;
        }

        /// <summary>
        /// Sets the email address of this instance.
        /// </summary>
        public void SetEmail()
        {
            bool formattedEmail = false;
            string username = "", domain = "", address = "";

            Console.Write("What is the humanoid's email address? ");

            address = Console.ReadLine();

            formattedEmail = EmailAddress.ParseEmail(address, ref username, ref domain);

            while (!formattedEmail)
            {
                Console.Write("Invalid email address. Please try again: ");
                address = Console.ReadLine();
                formattedEmail = EmailAddress.ParseEmail(address, ref username, ref domain);
            }

            Email = new EmailAddress(username, domain);
        }

        /// <summary>
        /// Sets the phone number of this instance.
        /// </summary>
        public void SetPhone()
        {
            bool validPhone = false;
            string number;

            Console.Write("What is the humanoid's phone number? ");

            number = Console.ReadLine();

            validPhone = PhoneNumber.ParseNumber(number);

            while (!validPhone)
            {
                Console.Write("Invalid phone number. Please try again: ");
                number = Console.ReadLine();
                validPhone = PhoneNumber.ParseNumber(number);
            }
            Phone = new PhoneNumber(number);
        }

        /// <summary>
        /// Determines the equality of the <see cref="Name"/> property of two <see cref="Humanoid"/> instances.
        /// </summary>
        /// <param name="other">The <see cref="Humanoid"/> instance being compared to this instance.</param>
        /// <returns>Returns 0 if equal or a nonzero number otherwise.</returns>
        public int CompareTo(Humanoid other)
        {
            return Name.CompareTo(other.Name);
        }
    }
}

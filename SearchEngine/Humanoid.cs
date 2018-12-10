using System;

namespace SearchEngine
{
    /// <summary>
    /// Holds the values of properties of a <see cref="Humanoid"/> instance.
    /// </summary>
    public enum HumanoidProp { NAME = 1, AGE, EMAIL, PHONE }

    /// <summary>
    /// 
    /// </summary>
    public class Humanoid : IComparable<Humanoid>
    {
        /// <summary>
        /// 
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public ushort Age { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public EmailAddress Email { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public PhoneNumber Phone { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Humanoid() {}

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="age"></param>
        public Humanoid(string name, ushort age)
        {
            Name = name;
            Age = age;
            SetEmail();
            SetPhone();
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="age"></param>
        /// <param name="email"></param>
        /// <param name="phone"></param>
        public Humanoid(string name, ushort age, string email, string phone)
        {
            Name = name;
            Age = age;
            Email = new EmailAddress(email);
            Phone = new PhoneNumber(phone);
        }

        /// <summary>
        /// 
        /// </summary>
        public void SetName()
        {
            Console.Write("What is the humanoid's new name? ");
            Name = Console.ReadLine();
        }

        /// <summary>
        /// 
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
        /// 
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
        /// 
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
        /// 
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public int CompareTo(Humanoid other)
        {
            return Name.CompareTo(other.Name);
        }
    }
}

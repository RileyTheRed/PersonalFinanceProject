using System;
using System.Text;
using System.Security.Cryptography;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security;

namespace PersonalFinanceProjectFinal.Utilities
{
    class Security
    {

        /// <summary>
        /// Returns true if two secure strings are equal
        /// 
        /// (( Found this function on StackOverFlow ))
        /// 
        /// SwDevMan81 - https://stackoverflow.com/questions/4502676/c-sharp-compare-two-securestrings-for-equality
        /// </summary>
        /// <param name="s1"></param>
        /// <param name="s2"></param>
        /// <returns></returns>
        public static bool SecureStringEqual(SecureString s1, SecureString s2)
        {
            if (s1 == null)
            {
                throw new ArgumentNullException("s1");
            }
            if (s2 == null)
            {
                throw new ArgumentNullException("s2");
            }

            if (s1.Length != s2.Length)
            {
                return false;
            }

            IntPtr bstr1 = IntPtr.Zero;
            IntPtr bstr2 = IntPtr.Zero;

            RuntimeHelpers.PrepareConstrainedRegions();

            try
            {
                bstr1 = Marshal.SecureStringToBSTR(s1);
                bstr2 = Marshal.SecureStringToBSTR(s2);

                unsafe
                {
                    for (Char* ptr1 = (Char*)bstr1.ToPointer(), ptr2 = (Char*)bstr2.ToPointer();
                        *ptr1 != 0 && *ptr2 != 0;
                         ++ptr1, ++ptr2)
                    {
                        if (*ptr1 != *ptr2)
                        {
                            return false;
                        }
                    }
                }

                return true;
            }
            finally
            {
                if (bstr1 != IntPtr.Zero)
                {
                    Marshal.ZeroFreeBSTR(bstr1);
                }

                if (bstr2 != IntPtr.Zero)
                {
                    Marshal.ZeroFreeBSTR(bstr2);
                }
            }
        }


        /// <summary>
        /// Returns a hash of a passed secure password. (not sure secure, pretty janky)
        /// </summary>
        /// <param name="password"></param>
        /// <returns></returns>
        public static string getPassHashSha256(SecureString password)
        {

            byte[] bytes = Encoding.UTF8.GetBytes(password.ToString());
            SHA256Managed hashstring = new SHA256Managed();
            byte[] hash = hashstring.ComputeHash(bytes);
            string hashString = string.Empty;
            foreach (byte x in hash)
            {
                hashString += String.Format("{0:x2}", x);
            }
            return hashString;
        }


        /// <summary>
        /// Returns the hash of the parameter
        /// 
        /// TODO : Change this to SecureString
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static string getHashSha256(string text)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(text);
            SHA256Managed hashstring = new SHA256Managed();
            byte[] hash = hashstring.ComputeHash(bytes);
            string hashString = string.Empty;
            foreach (byte x in hash)
            {
                hashString += String.Format("{0:x2}", x);
            }
            return hashString;
        }

    }
}

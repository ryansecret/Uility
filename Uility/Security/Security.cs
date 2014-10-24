using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Permissions;
using System.Security.Principal;
using System.Text;
using System.Threading;

namespace Uility.Security
{
   public  class Security
   {
       private WindowsIdentity identity = WindowsIdentity.GetAnonymous();
       private GenericIdentity genericIdentity = new GenericIdentity("dd");


       void TestPrincipla()
       {
           WindowsPrincipal windowsPrincipal = new WindowsPrincipal(identity);
           if (windowsPrincipal.IsInRole(0x220))
           {
                 
           }

           AppDomain domain = Thread.GetDomain();
           domain.SetPrincipalPolicy(PrincipalPolicy.WindowsPrincipal);

           windowsPrincipal = Thread.CurrentPrincipal as WindowsPrincipal;

            genericIdentity = new GenericIdentity("chen");
           string[] myRoles = {"ryan","richard"};
           GenericPrincipal genericPrincipal = new GenericPrincipal(genericIdentity, myRoles);
           Thread.CurrentPrincipal = genericPrincipal;
           PrincipalPermission permission = new PrincipalPermission("dd","ryan");
           permission.Demand();
           
       }
       void Crypt()
       {
           Aes aes = new AesCryptoServiceProvider();
          
       }

       private static void ShowIdentityPreferences(
       GenericIdentity genericIdentity)
       {
           // Retrieve the name of the generic identity object.
           string identityName = genericIdentity.Name;

           // Retrieve the authentication type of the generic identity object.
           string identityAuthenticationType =
               genericIdentity.AuthenticationType;

           Console.WriteLine("Name: " + identityName);
           Console.WriteLine("Type: " + identityAuthenticationType);

           // Verify that the user's identity has been authenticated
           // (was created with a valid name).
           if (genericIdentity.IsAuthenticated)
           {
               Console.WriteLine("The user's identity has been authenticated.");
           }
           else
           {
               Console.WriteLine("The user's identity has not been " +
                   "authenticated.");
           }
           Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~");
       }

       // Create generic identity based on values from the current
       // WindowsIdentity.
       private static GenericIdentity GetGenericIdentity()
       {
           // Get values from the current WindowsIdentity.
           WindowsIdentity windowsIdentity = WindowsIdentity.GetCurrent();

           // Construct a GenericIdentity object based on the current Windows
           // identity name and authentication type.
           string authenticationType = windowsIdentity.AuthenticationType;
           string userName = windowsIdentity.Name;
           GenericIdentity authenticatedGenericIdentity =
               new GenericIdentity(userName, authenticationType);

           return authenticatedGenericIdentity;
       }
   }
}

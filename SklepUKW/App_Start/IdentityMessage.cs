#region Assembly Microsoft.AspNet.Identity.Core, Version=2.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35
// C:\Users\natal\OneDrive\Pulpit\SklepInternetowyUKW-master\SklepInternetowyUKW-master\SklepUKW\packages\Microsoft.AspNet.Identity.Core.2.2.3\lib\net45\Microsoft.AspNet.Identity.Core.dll
#endregion

namespace Microsoft.AspNet.Identity
{
    //
    // Summary:
    //     Represents a message
    public class IdentityMessage
    {
        public IdentityMessage();

        //
        // Summary:
        //     Destination, i.e. To email, or SMS phone number
        public virtual string Destination { get; set; }
        //
        // Summary:
        //     Subject
        public virtual string Subject { get; set; }
        //
        // Summary:
        //     Message contents
        public virtual string Body { get; set; }
    }
}
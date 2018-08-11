using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Resources;
using System.Runtime.CompilerServices;

namespace MaxwellBiosTweaker.Properties
{
    [GeneratedCode("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
    [DebuggerNonUserCode]
    [CompilerGenerated]
    internal class Resources
    {
        private static ResourceManager resourceManager;
        private static CultureInfo cultureInfo;

        [EditorBrowsable(EditorBrowsableState.Advanced)]
        internal static ResourceManager GetResourceManager
        {
            get
            {
                if (object.ReferenceEquals(Resources.resourceManager, null))
                    Resources.resourceManager = new ResourceManager("Properties.Resources", typeof(Resources).Assembly);
                return Resources.resourceManager;
            }
        }

        [EditorBrowsable(EditorBrowsableState.Advanced)]
        internal static CultureInfo CultureInfo
        {
            get
            {
                return Resources.cultureInfo;
            }
            set
            {
                Resources.cultureInfo = value;
            }
        }

        internal static Bitmap GetNVidiaLogo
        {
            get
            {
                return (Bitmap)Resources.GetResourceManager.GetObject("nv_logo1", Resources.cultureInfo);
            }
        }

        internal Resources()
        {
        }
    }
}

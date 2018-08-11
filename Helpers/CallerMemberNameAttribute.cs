using System;

namespace MaxwellBiosTweaker.Helper
{
  [AttributeUsage(AttributeTargets.Parameter, AllowMultiple = false, Inherited = true)]
  public sealed class CallerMemberNameAttribute : Attribute
  {
  }
}

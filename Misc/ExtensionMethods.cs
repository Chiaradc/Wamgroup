using Elogic.Wamgroup.Sito2023.NetCore.Entities.WamgroupNews;
using Progress.Sitefinity.AspNetCore.ViewComponents;
using System.Reflection;

namespace Elogic.Wamgroup.Sito2023.NetCore.Misc
{
    public static class ExtensionMethods
    {

        public static void InitializeWithEntity<T> (this ViewComponentContext<T> context) where T : class
        {
            Type t = context.GetType();
            if (t.GetProperty(nameof(context.Entity), BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance) == null)
                throw new ArgumentOutOfRangeException("propName", string.Format("Property {0} was not found in Type {1}", nameof(context.Entity), context.GetType().FullName));
            t.InvokeMember(nameof(context.Entity), BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.SetProperty | BindingFlags.Instance, null, context, new object[] { new WamgroupNewsEntity() });
        }
    }
}

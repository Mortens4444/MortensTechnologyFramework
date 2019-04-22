using System.Reflection;
using System.Resources;
using System.Text;

namespace Mtf.Reflection
{
    public class ResourceInfo
    {
        public string ShowAssemblyResourceInfo(Assembly assembly)
        {
            var result = new StringBuilder();
            var manifestResourceNames = assembly.GetManifestResourceNames();
            foreach (var manifestResourceName in manifestResourceNames)
            {
                var set = new ResourceSet(assembly.GetManifestResourceStream(manifestResourceName));
                var id = set.GetEnumerator();
                while (id.MoveNext())
                {
                    result.AppendLine($"{manifestResourceName} - {id.Key} = {id.Value}");
                }
            }
            return result.ToString();
        }
    }
}
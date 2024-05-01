using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Assets.Scripts.Building.Util.Data;

namespace Assets.Scripts.Resources.Util
{
    internal class Data
    {
        public enum Resources
        {
            Gold
        }

        public static string TagToString(Resources value) => value switch
        {
            Resources.Gold => "Gold",
            _ => throw new ArgumentException("Can't match value")
        };

        public static Resources StringToTags(string value) => value switch
        {
            "Gold" => Resources.Gold,
            _ => throw new ArgumentException("Can't match value")
        };

        public static bool TryMatch(string value, out Resources resource)
        {
            resource = default;
            try
            {
                resource = StringToTags(value);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}

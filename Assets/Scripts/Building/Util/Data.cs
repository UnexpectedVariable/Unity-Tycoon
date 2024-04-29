using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Building.Util
{
    internal class Data
    {
        public enum Tags
        {
            BuildingSpot
        }

        public static string TagToString(Tags value) => value switch
        {
            Tags.BuildingSpot => "Building Spot",
            _ => throw new ArgumentException("Can't match value")
        };

        public static Tags StringToTags(string value) => value switch
        {
            "Building Spot" => Tags.BuildingSpot,
            _ => throw new ArgumentException("Can't match value")
        };

        public static bool TryMatch(string value, out Tags tag)
        {
            tag = default;
            try
            {
                tag = StringToTags(value); 
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}

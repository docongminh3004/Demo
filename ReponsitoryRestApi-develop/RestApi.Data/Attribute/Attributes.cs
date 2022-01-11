using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestApi.Data.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class NotEmpty : Attribute
    {

    }

    [AttributeUsage(AttributeTargets.Property)]
    public class PropertyName : Attribute
    {
        public string Name { get; set; }
        public PropertyName(string name)
        {
            this.Name = name;
        }
    }
    [AttributeUsage(AttributeTargets.Property)]
    public class propMaxLength : Attribute
    {
        public int Length { get; set; }
        public propMaxLength(int length)
        {
            this.Length = length;
        }
    }
    [AttributeUsage(AttributeTargets.Property)]
    public class CheckDate : Attribute
    {
    }
}

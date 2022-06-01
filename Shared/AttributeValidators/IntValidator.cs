using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.AttributeValidators
{
    public class IntRangeAttribute : ValidationAttribute
    {
        public int Minimum { get; set; }
        public int Maximum { get; set; }

        public IntRangeAttribute()
        {
            this.Minimum = 0;
            this.Maximum = int.MaxValue;
        }

        public override bool IsValid(object value)
        {
            if(value == null)
                return false;

            int Value = (int)value;

            return Minimum <= Value && Value <= Maximum;
        }
    }
}

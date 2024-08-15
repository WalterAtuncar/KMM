using System;
using System.ComponentModel.DataAnnotations;

namespace Data.Util
{
    public class UpperCaseAtribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            try
            {
                validationContext.ObjectType.GetProperty(validationContext.MemberName).SetValue(validationContext.ObjectInstance, value.ToString().ToUpper().Trim(), null);
            }
            catch (Exception)
            {
            }
            return null;
        }
    }
}

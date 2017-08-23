using FormPackage6.Core.DomainModel;
using FormPackage6.Core.DomainModel.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FormPackage6.Core.Extensions
{
    public static class CustomAttributeExtension
    {
        public static bool IsAliasOf(this IDomainModel instance, string alias)
        {
            var attrType = instance.GetType();
            var attr = (DocumentTypeAliasAttribute) attrType.GetCustomAttributes(typeof(DocumentTypeAliasAttribute), false).FirstOrDefault();

            bool isAlias = attr != null ? attr.Name.Equals(alias, StringComparison.CurrentCultureIgnoreCase) : false;

            return isAlias;
        }

        public static string GetAliasName(this IDomainModel instance)
        {
            var attrType = instance.GetType();
            var attr = (DocumentTypeAliasAttribute) attrType.GetCustomAttributes(typeof(DocumentTypeAliasAttribute), false).FirstOrDefault();

            string alias = attr?.Name;

            return alias;
        }
    }
}

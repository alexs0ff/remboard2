using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Features.Cruds
{
    public class CrudControllerDescriptor : ICrudControllerDescriptor
    {
        private readonly ICrudEntityDescriptor _entityDescriptor;

        private readonly AccessRuleMap _accessRuleMap;

        public CrudControllerDescriptor(ICrudEntityDescriptor entityDescriptor, AccessRuleMap accessRuleMap)
        {
            _entityDescriptor = entityDescriptor;
            _accessRuleMap = accessRuleMap;
        }

        public string EntityName => _entityDescriptor.EntityName;

        public ICrudEntityDescriptor EntityDescriptor => _entityDescriptor;

        public AccessRuleMap AccessRules => _accessRuleMap;
    }
}

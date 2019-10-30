using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Common.FeatureEntities;
using Entities;

namespace Common.Features
{
    public class AccessRuleMap
    {
        private static readonly IDictionary<AccessKind, HashSet<ProjectRoles>> _defaultRoles = new Dictionary<AccessKind, HashSet<ProjectRoles>>();

        static AccessRuleMap()
        {
            var allProjectRoles = new HashSet<ProjectRoles>();
            foreach (long projectRoleValue in Enum.GetValues(typeof(ProjectRoles)))
            {
                allProjectRoles.Add((ProjectRoles)projectRoleValue);
            }

            foreach (int accessKindValue in Enum.GetValues(typeof(AccessKind)))
            {
                _defaultRoles.Add((AccessKind)accessKindValue,allProjectRoles);
            }
        }

        private enum AccessKind:int
        {
            Read,
            Modify
        }

        private readonly IReadOnlyDictionary<AccessKind, HashSet<ProjectRoles>> _roles;

        public AccessRuleMap()
        {
            _roles = new ReadOnlyDictionary<AccessKind, HashSet<ProjectRoles>>(_defaultRoles);
        }

        public AccessRuleMap(ProjectRoles[] readRoles)
        {
            var innerDictionary = new Dictionary<AccessKind, HashSet<ProjectRoles>>();
            innerDictionary.Add(AccessKind.Read, readRoles.ToHashSet());

            _roles = new ReadOnlyDictionary<AccessKind, HashSet<ProjectRoles>>(innerDictionary);
        }

        public AccessRuleMap(ProjectRoles[] readRoles, ProjectRoles[] modifyRoles)
        {
            var innerDictionary = new Dictionary<AccessKind, HashSet<ProjectRoles>>();

            if (readRoles!=null && readRoles.Any())
            {
                innerDictionary.Add(AccessKind.Read, readRoles.ToHashSet());
            }
            else
            {
                innerDictionary.Add(AccessKind.Read, _defaultRoles[AccessKind.Read]);
            }

            if (modifyRoles!=null && modifyRoles.Any())
            {
                innerDictionary.Add(AccessKind.Modify, modifyRoles.ToHashSet());
            }
            else
            {
                innerDictionary.Add(AccessKind.Modify, _defaultRoles[AccessKind.Modify]);
            }
            

            _roles = new ReadOnlyDictionary<AccessKind, HashSet<ProjectRoles>>(innerDictionary);
        }

        public AccessRuleMap(bool allReadRoles)
        {
            var innerDictionary = new Dictionary<AccessKind, HashSet<ProjectRoles>>();
            if (allReadRoles)
            {
                innerDictionary.Add(AccessKind.Read, _defaultRoles[AccessKind.Read]);
            }

            _roles = new ReadOnlyDictionary<AccessKind, HashSet<ProjectRoles>>(innerDictionary);
        }

        private bool CanDo(AccessKind accessKind, params ProjectRoles[] roles)
        {
            if (roles==null)
            {
                return false;
            }

            if (!roles.Any())
            {
                return false;
            }
            if (!_roles.ContainsKey(accessKind))
            {
                return false;
            }

            return roles.Any(i => _roles[accessKind].Contains(i));
        }

        public bool CanRead(params ProjectRoles[] readRoles)
        {
            return CanDo(AccessKind.Read, readRoles);
        }

        public bool CanModify(params ProjectRoles[] modifyRoles)
        {
            return CanDo(AccessKind.Modify, modifyRoles);
        }
    }
}

using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace yamlist.Modules.IO.Json.Resolvers
{
    public class DisableContractResolver : DefaultContractResolver
    {
        readonly HashSet<Type> types;

        public DisableContractResolver(IEnumerable<Type> types)
        {
            if (types == null)
                throw new ArgumentNullException();
            this.types = new HashSet<Type>(types);
        }

        protected override JsonConverter ResolveContractConverter(Type objectType)
        {
            if (ContainsType(objectType))
                return null;
            return base.ResolveContractConverter(objectType);
        }

        private bool ContainsType(Type type)
        {
            return types.Contains(type);
        }
    }}
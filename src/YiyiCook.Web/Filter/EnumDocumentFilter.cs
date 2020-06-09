using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Tgnet.Club.Web.Filter
{
    class SwaggerAddEnumDescriptions : IDocumentFilter
    {
        public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
        {
            // add enum descriptions to result models
            foreach (var property in swaggerDoc.Components.Schemas.Where(x => x.Value?.Enum?.Count > 0))
            {
                IList<IOpenApiAny> propertyEnums = property.Value.Enum;
                if (propertyEnums != null && propertyEnums.Count > 0)
                {
                    property.Value.Description += DescribeEnum(propertyEnums, property.Key);
                }
            }

            // add enum descriptions to input parameters
            foreach (var pathItem in swaggerDoc.Paths)
            {
                DescribeEnumParameters(pathItem.Value.Operations, swaggerDoc, context.ApiDescriptions, pathItem.Key);
            }
        }

        private void DescribeEnumParameters(IDictionary<OperationType, OpenApiOperation> operations, OpenApiDocument swaggerDoc, IEnumerable<ApiDescription> apiDescriptions, string path)
        {
            path = path.Trim('/');
            if (operations != null)
            {
                var pathDescriptions = apiDescriptions.Where(a => a.RelativePath == path);
                foreach (var oper in operations)
                {
                    var operationDescription = pathDescriptions.FirstOrDefault(a => a.HttpMethod.Equals(oper.Key.ToString(), StringComparison.InvariantCultureIgnoreCase));
                    foreach (var param in oper.Value.Parameters)
                    {
                        var parameterDescription = operationDescription.ParameterDescriptions.FirstOrDefault(a => a.Name == param.Name);
                        if (parameterDescription != null && TryGetEnumType(parameterDescription.Type, out Type enumType))
                        {
                            var paramEnum = swaggerDoc.Components.Schemas.FirstOrDefault(x => x.Key == enumType.Name);
                            if (paramEnum.Value != null)
                            {
                                param.Description += DescribeEnum(paramEnum.Value.Enum, paramEnum.Key);
                            }
                        }
                    }
                }
            }
        }

        bool TryGetEnumType(Type type, out Type enumType)
        {
            if (type.IsEnum)
            {
                enumType = type;
                return true;
            }
            else if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>))
            {
                var underlyingType = Nullable.GetUnderlyingType(type);
                if (underlyingType != null && underlyingType.IsEnum == true)
                {
                    enumType = underlyingType;
                    return true;
                }
            }
            else
            {
                Type underlyingType = GetTypeIEnumerableType(type);
                if (underlyingType != null && underlyingType.IsEnum)
                {
                    enumType = underlyingType;
                    return true;
                }
                else
                {
                    var interfaces = type.GetInterfaces();
                    foreach (var interfaceType in interfaces)
                    {
                        underlyingType = GetTypeIEnumerableType(interfaceType);
                        if (underlyingType != null && underlyingType.IsEnum)
                        {
                            enumType = underlyingType;
                            return true;
                        }
                    }
                }
            }

            enumType = null;
            return false;
        }

        Type GetTypeIEnumerableType(Type type)
        {
            if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(IEnumerable<>))
            {
                var underlyingType = type.GetGenericArguments()[0];
                if (underlyingType.IsEnum)
                {
                    return underlyingType;
                }
            }

            return null;
        }

        private Type GetEnumTypeByName(string enumTypeName)
        {
            return AppDomain.CurrentDomain
                .GetAssemblies()
                .SelectMany(x => x.GetTypes())
                .FirstOrDefault(x => x.Name == enumTypeName);
        }

        private string DescribeEnum(IList<IOpenApiAny> enums, string proprtyTypeName)
        {
            List<string> enumDescriptions = new List<string>();
            var enumType = GetEnumTypeByName(proprtyTypeName);
            if (enumType == null)
                return null;

            foreach (var enumOption in enums)
            {
                int enumInt;
                if (enumOption is OpenApiString)
                {
                    enumInt = (int)Enum.Parse(enumType, ((OpenApiString)enumOption).Value);
                }
                else if (enumOption is OpenApiInteger)
                {
                    enumInt = ((OpenApiInteger)enumOption).Value;
                }
                else 
                {
                    break;
                }
                enumDescriptions.Add(string.Format("{0} = {1}", enumInt, Enum.GetName(enumType, enumInt)));
            }
            return string.Join(", ", enumDescriptions.ToArray());
        }
    }
    public class EnumSchemaFilter : ISchemaFilter
    {
        public void Apply(OpenApiSchema schema, SchemaFilterContext context)
        {
            if (context.Type.IsEnum)
            {
                var enumValues = schema.Enum.ToArray();
                var i = 0;
                schema.Enum.Clear();
                foreach (var n in Enum.GetNames(context.Type).ToList())
                {
                    schema.Enum.Add(new OpenApiString(n + $" = {((OpenApiPrimitive<byte>)enumValues[i]).Value}"));
                    i++;
                }
            }
        }
    }
    /// <summary>
    /// Add enum value descriptions to Swagger
    /// </summary>
    public class SwaggerEnumFilter : IDocumentFilter
    {
        public void Apply(Microsoft.OpenApi.Models.OpenApiDocument swaggerDoc, DocumentFilterContext context)
        //public void Apply(SwaggerDocument swaggerDoc, DocumentFilterContext context)
        {
            Dictionary<string, Type> dict = GetAllEnum();

            foreach (var item in swaggerDoc.Components.Schemas)
            //foreach (var item in swaggerDoc.Definitions)
            {
                var property = item.Value;
                var typeName = item.Key;
                Type itemType = null;
                if (property.Enum != null && property.Enum.Count > 0)
                {
                    if (dict.ContainsKey(typeName))
                    {
                        itemType = dict[typeName];
                    }
                    else
                    {
                        itemType = null;
                    }
                    List<OpenApiInteger> list = new List<OpenApiInteger>();
                    foreach (var val in property.Enum)
                    {
                        int enumInt;
                       
                        list.Add((OpenApiInteger)val);
                    }
                    property.Description += DescribeEnum(itemType, list);
                }
            }
        }
        private static Dictionary<string, Type> GetAllEnum()
        {
            Assembly ass = Assembly.Load("Tgnet.Club.Infrastructure");
            Type[] types = ass.GetTypes();
            Dictionary<string, Type> dict = new Dictionary<string, Type>();

            foreach (Type item in types)
            {
                if (item.IsEnum&& !dict.ContainsKey(item.Name))
                {
                    dict.Add(item.Name, item);
                }
            }
            return dict;
        }

        private static string DescribeEnum(Type type, List<OpenApiInteger> enums)
        {
            var enumDescriptions = new List<string>();
            foreach (var item in enums)
            {
                if (type == null) continue;
                var value = Enum.Parse(type, item.Value.ToString());
                var desc = GetDescription(type, value);

                if (string.IsNullOrEmpty(desc))
                    enumDescriptions.Add($"{item.Value.ToString()}:{Enum.GetName(type, value)}; ");
                else
                    enumDescriptions.Add($"{item.Value.ToString()}:{Enum.GetName(type, value)},{desc}; ");

            }
            return $"<br/>{Environment.NewLine}{string.Join("<br/>" + Environment.NewLine, enumDescriptions)}";
        }

        private static string GetDescription(Type t, object value)
        {
            foreach (MemberInfo mInfo in t.GetMembers())
            {
                if (mInfo.Name == t.GetEnumName(value))
                {
                    foreach (Attribute attr in Attribute.GetCustomAttributes(mInfo))
                    {
                        if (attr.GetType() == typeof(DescriptionAttribute))
                        {
                            return ((DescriptionAttribute)attr).Description;
                        }
                    }
                }
            }
            return string.Empty;
        }
    }
    public class SwaggerEnumFilter2 : IDocumentFilter
    {
        public void Apply(Microsoft.OpenApi.Models.OpenApiDocument swaggerDoc, DocumentFilterContext context)
        //public void Apply(SwaggerDocument swaggerDoc, DocumentFilterContext context)
        {
            Dictionary<string, Type> dict = GetAllEnum();

            foreach (var item in swaggerDoc.Components.Schemas)
            //foreach (var item in swaggerDoc.Definitions)
            {
                var property = item.Value;
                var typeName = item.Key;
                Type itemType = null;
                if (property.Enum != null && property.Enum.Count > 0)
                {
                    if (dict.ContainsKey(typeName))
                    {
                        itemType = dict[typeName];
                    }
                    else
                    {
                        itemType = null;
                    }
                    List<OpenApiInteger> list = new List<OpenApiInteger>();
                    foreach (var val in property.Enum)
                    {
                        list.Add((OpenApiInteger)val);
                    }
                    property.Description += DescribeEnum(itemType, list);
                }
            }
        }
        private static Dictionary<string, Type> GetAllEnum()
        {
            Assembly ass = Assembly.Load("Test.Model");
            Type[] types = ass.GetTypes();
            Dictionary<string, Type> dict = new Dictionary<string, Type>();

            foreach (Type item in types)
            {
                if (item.IsEnum)
                {
                    dict.Add(item.Name, item);
                }
            }
            return dict;
        }

        private static string DescribeEnum(Type type, List<OpenApiInteger> enums)
        {
            var enumDescriptions = new List<string>();
            foreach (var item in enums)
            {
                if (type == null) continue;
                var value = Enum.Parse(type, item.Value.ToString());
                var desc = GetDescription(type, value);

                if (string.IsNullOrEmpty(desc))
                    enumDescriptions.Add($"{item.Value.ToString()}:{Enum.GetName(type, value)}; ");
                else
                    enumDescriptions.Add($"{item.Value.ToString()}:{Enum.GetName(type, value)},{desc}; ");

            }
            return $"<br/>{Environment.NewLine}{string.Join("<br/>" + Environment.NewLine, enumDescriptions)}";
        }

        private static string GetDescription(Type t, object value)
        {
            foreach (MemberInfo mInfo in t.GetMembers())
            {
                if (mInfo.Name == t.GetEnumName(value))
                {
                    foreach (Attribute attr in Attribute.GetCustomAttributes(mInfo))
                    {
                        if (attr.GetType() == typeof(DescriptionAttribute))
                        {
                            return ((DescriptionAttribute)attr).Description;
                        }
                    }
                }
            }
            return string.Empty;
        }
    }
}

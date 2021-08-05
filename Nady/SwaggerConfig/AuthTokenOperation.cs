using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;

namespace Nady.SwaggerConfig
{
    public class AuthTokenOperation : IDocumentFilter
    {

        public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
        {
            swaggerDoc.Paths.Add("/token", new OpenApiPathItem
            {
                Operations = new Dictionary<OperationType, OpenApiOperation>
                {
                    {
                        OperationType.Post, new OpenApiOperation
                        {
                            RequestBody = new OpenApiRequestBody
                            {
                                Required = true,
                                Content = new Dictionary<string, OpenApiMediaType>
                                {
                                    {
                                        "application/x-www-form-urlencoded", new OpenApiMediaType()
                                        {
                                            Schema = new OpenApiSchema()
                                            {
                                               Type = "string",
                                               Format = "string"
                                            }
                                        } 
                                    } 
                                } 
                            },
                           Tags = new List<OpenApiTag>{ new OpenApiTag { Name = "Auth"} },
                           Parameters = new List<OpenApiParameter>
                           {
                                new OpenApiParameter
                                {
                                    Name = "grant_type",
                                    In = ParameterLocation.Header,
                                    Description = "access token",
                                    Required = true,
                                    Schema = new OpenApiSchema
                                    {
                                        Type = "string",
                                        Default = new OpenApiString("password")
                                    }
                                },
                                new OpenApiParameter
                                {
                                    Name = "username",
                                    In = ParameterLocation.Header,
                                    Description = "username",
                                    Required = false,
                                    Schema = new OpenApiSchema
                                    {
                                        Type = "string"
                                    }
                                },
                                new OpenApiParameter
                                {
                                    Name = "password",
                                    In = ParameterLocation.Header,
                                    Description = "password",
                                    Required = false,
                                    Schema = new OpenApiSchema
                                    {
                                        Type = "string"
                                    }
                                }
                            }

                        }

                    }
                },

            });
        }
    }
}

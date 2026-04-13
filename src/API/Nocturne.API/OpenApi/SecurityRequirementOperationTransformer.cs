using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.OpenApi;
using Microsoft.OpenApi;

namespace Nocturne.API.OpenApi;

/// <summary>
/// Attaches per-operation security requirements based on [Authorize] / [AllowAnonymous] attributes.
/// Nocturne endpoints get oauth2|bearer|instanceKey; Nightscout endpoints get apiSecret.
/// </summary>
public sealed class SecurityRequirementOperationTransformer : IOpenApiOperationTransformer
{
    public Task TransformAsync(
        OpenApiOperation operation,
        OpenApiOperationTransformerContext context,
        CancellationToken cancellationToken)
    {
        var actionDescriptor = context.Description.ActionDescriptor as ControllerActionDescriptor;
        if (actionDescriptor is null)
            return Task.CompletedTask;

        var methodInfo = actionDescriptor.MethodInfo;
        var controllerType = actionDescriptor.ControllerTypeInfo;

        var allowAnonymous =
            methodInfo.GetCustomAttributes(typeof(AllowAnonymousAttribute), inherit: true).Length > 0
            || controllerType.GetCustomAttributes(typeof(AllowAnonymousAttribute), inherit: true).Length > 0;

        if (allowAnonymous)
            return Task.CompletedTask;

        var hasAuthorize =
            methodInfo.GetCustomAttributes(typeof(AuthorizeAttribute), inherit: true).Length > 0
            || controllerType.GetCustomAttributes(typeof(AuthorizeAttribute), inherit: true).Length > 0;

        if (!hasAuthorize)
            return Task.CompletedTask;

        var document = context.Document;

        if (context.DocumentName == "nocturne")
        {
            // Each scheme is its own requirement entry → OR logic (any one suffices).
            operation.Security ??= [];
            operation.Security.Add(new OpenApiSecurityRequirement
            {
                [new OpenApiSecuritySchemeReference("oauth2", document)] = ["*"],
            });
            operation.Security.Add(new OpenApiSecurityRequirement
            {
                [new OpenApiSecuritySchemeReference("bearer", document)] = [],
            });
            operation.Security.Add(new OpenApiSecurityRequirement
            {
                [new OpenApiSecuritySchemeReference("instanceKey", document)] = [],
            });
        }
        else if (context.DocumentName == "nightscout")
        {
            operation.Security ??= [];
            operation.Security.Add(new OpenApiSecurityRequirement
            {
                [new OpenApiSecuritySchemeReference("apiSecret", document)] = [],
            });
        }

        return Task.CompletedTask;
    }
}

namespace BuildingBlock.Core.Exceptions.Enums;

public enum ExceptionStatusCode
{
    ResourceInvalid = 400,
    OperationUnauthorized = 401,
    ResourceAccessDenied = 403,
    ResourceNotFound = 404,
    ResourceDuplicate = 409,
    InternalError = 500
}
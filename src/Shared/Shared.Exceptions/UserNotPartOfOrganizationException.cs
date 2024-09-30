namespace Shared.Exceptions
{
    public class UserNotPartOfOrganizationException(int userId)
        : Exception($"User with ID {userId} is not part of any organization.");

}

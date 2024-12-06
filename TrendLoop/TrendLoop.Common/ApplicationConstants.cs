namespace TrendLoop.Common
{
    public static class ApplicationConstants
    {
        public static class Messages
        {
            public const string SuccessfullyAddedUserMessageTitle = "User Added Successfully";

            public const string SuccessfullyAddedUserMessageBody = "The user with email {0} has been successfully added.";

            public const string FailedAddingUserMessageTitle = "Failed to Add User";

            public const string FailedAddingUserMessageBody = "An error occurred while adding the user with email <strong>{email}</strong>. Please try again.";

            public const string UserNotFoundMessageTitle = "User To Delete Not Found";

            public const string UserNotFoundMessageBody = "No user with email <strong>{0}</strong> was found.";

            public const string SuccessfullyDeletedUserMessageTitle = "User Deleted Successfully";

            public const string SuccessfullyDeletedUserMessageBody = "The user with email {0} has been successfully deleted.";

            public const string FailedDeleteUserMessageTitle = "Failed to Delete User";

            public const string FailedDeletingUserMessageBody = "An error occurred while adding the user with email <strong>{0}</strong>. Please try again.";
        }
    }
}

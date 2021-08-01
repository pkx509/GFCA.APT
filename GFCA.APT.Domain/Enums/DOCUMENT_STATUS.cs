namespace GFCA.APT.Domain.Enums
{
    public struct DOCUMENT_STATUS
    {
        public const string Draft = @"C";
        public const string WaitForApproval = @"W";
        public const string Approved = @"A";
        public const string Return = @"R";
        public const string Delete = @"D";
    }
}

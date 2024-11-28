using TrendLoop.Services.Data.Interfaces;

namespace TrendLoop.Services.Data
{
    public class BaseService : IBaseService
    {
        public bool IsGuidValid(string? id, ref Guid parsedGuid)
        {
            // check if string is null, empty or whitespace
            if (String.IsNullOrWhiteSpace(id))
            {
                return false;
            }

            // try parse the Guid
            bool isGuidValid = Guid.TryParse(id, out parsedGuid);

            if (!isGuidValid)
            {
                return false;
            }

            return true;
        }
    }
}

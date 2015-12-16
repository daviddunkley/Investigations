namespace MyApiAcceptanceTests.Models
{
    public static class PublicationExtensions
    {
        public static void Merge(this Publication instanceA, Publication instanceB)
        {
            if (instanceA == null || instanceB == null)
            {
                return;
            }

            if (instanceB.AdditionalParameters != null)
            {
                instanceA.AdditionalParameters = instanceB.AdditionalParameters;
            }

            if (instanceB.ContentViewed != null)
            {
                instanceA.ContentViewed = instanceB.ContentViewed;
            }

            if (instanceB.Name != null)
            {
                instanceA.Name = instanceB.Name;
            }

            if (instanceB.PublicationId != null)
            {
                instanceA.PublicationId = instanceB.PublicationId;
            }

            if (instanceB.StartDate != null)
            {
                instanceA.StartDate = instanceB.StartDate;
            }

            if (instanceB.SendEmail != null)
            {
                instanceA.SendEmail = instanceB.SendEmail;
            }

            if (instanceB.Url != null)
            {
                instanceA.Url = instanceB.Url;
            }
        }
    }
}

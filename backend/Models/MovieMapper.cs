namespace backend.Models
{
    public static class MovieMapper
    {
        public static Movie MapToMovie(ExternalMovie externalMovie)
        {
            return new Movie
            {
                Title = externalMovie.title,
                ReleaseYear = externalMovie.year,
                Active = true,
                ExternalId = externalMovie.id
            };
        }
    }
}

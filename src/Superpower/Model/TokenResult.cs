namespace Superpower.Model
{
    public static class TokenResult
    {
        public static TokenResult<TTokenKind, T> Empty<TTokenKind, T>(TokenList<TTokenKind> remainder)
        {
            return new TokenResult<TTokenKind, T>(remainder, Position.Empty, null, null);
        }

        public static TokenResult<TTokenKind, T> Empty<TTokenKind, T>(TokenList<TTokenKind> remainder, string[] expectations)
        {
            return new TokenResult<TTokenKind, T>(remainder, Position.Empty, null, expectations);
        }

        public static TokenResult<TTokenKind, T> Empty<TTokenKind, T>(TokenList<TTokenKind> remainder, string errorMessage)
        {
            return new TokenResult<TTokenKind, T>(remainder, Position.Empty, errorMessage, null);
        }

        public static TokenResult<TTokenKind, T> Empty<TTokenKind, T>(TokenList<TTokenKind> remainder, Position errorPosition, string errorMessage)
        {
            return new TokenResult<TTokenKind, T>(remainder, errorPosition, errorMessage, null);
        }

        public static TokenResult<TTokenKind, T> Value<TTokenKind, T>(T value, TokenList<TTokenKind> location, TokenList<TTokenKind> remainder)
        {
            return new TokenResult<TTokenKind, T>(value, location, remainder);
        }

        public static TokenResult<TTokenKind,U> CastEmpty<TTokenKind, T, U>(TokenResult<TTokenKind, T> tokenResult)
        {
            return new TokenResult<TTokenKind, U>(tokenResult.Remainder, tokenResult.ErrorPosition, tokenResult.ErrorMessage, tokenResult.Expectations);
        }

        public static TokenResult<TTokenKind, T> CombineEmpty<TTokenKind, T>(TokenResult<TTokenKind, T> first, TokenResult<TTokenKind, T> second)
        {
            if (first.Remainder != second.Remainder)
                return second;

            var expectations = first.Expectations;
            if (expectations == null)
                expectations = second.Expectations;
            else if (second.Expectations != null)
            {
                expectations = new string[first.Expectations.Length + second.Expectations.Length];
                var i = 0;
                for (; i < first.Expectations.Length; ++i)
                    expectations[i] = first.Expectations[i];
                for (var j = 0; j < second.Expectations.Length; ++i, ++j)
                    expectations[i] = second.Expectations[j];
            }

            return new TokenResult<TTokenKind, T>(second.Remainder, second.ErrorPosition, first.ErrorMessage, expectations);
        }
    }
}
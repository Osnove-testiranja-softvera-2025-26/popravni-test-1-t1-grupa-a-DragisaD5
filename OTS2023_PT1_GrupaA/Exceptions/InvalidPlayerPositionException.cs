

using System;

namespace OTS2026_PT1_GrupaA.Exceptions
{
    public class InvalidPlayerPositionException: Exception
    {
        public InvalidPlayerPositionException()
        {

        }

        public InvalidPlayerPositionException(string message) : base(message)
        {

        }
    }
}

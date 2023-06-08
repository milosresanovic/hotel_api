﻿namespace project_hotel.Api.Core
{
    public interface ITokenStorage
    {
        void AddToken(string id);
        bool TokenExists(string id);
        void InvalidateToken(string id);
    }
}

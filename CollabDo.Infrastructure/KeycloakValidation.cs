﻿
using CollabDo.Application.Exceptions;
using System.Net.Http;

namespace CollabDo.Infrastructure
{
    public static class KeycloakValidation
    {
        public static void CreateUserValidation(HttpResponseMessage response)
        {
            if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
            {
                throw new ValidationException("Failed to create user in Keycloak.");
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.Conflict)
            {
                throw new ValidationException("User exists.");
            }
        }
    }
}
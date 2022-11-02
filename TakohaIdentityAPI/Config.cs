using System.Security.Claims;
using IdentityServer4.Models;
using IdentityServer4.Test;

namespace TakohaIdentityAPI;

public static class Config
{
    public static List<TestUser> GetUsers()
    {
        return new List<TestUser>
        {
            new TestUser
            {
                SubjectId = "fec0a4d6-5830-4eb8-8024-272bd5d6d2bb",
                Username = "Jon",
                Password = "jon123",
                Claims = new List<Claim>
                {
                    new Claim("given_name", "Jon"),
                    new Claim("family_name", "Doe"),
                    new Claim("role", "Administrator"),
                }
            },
            new TestUser
            {
                SubjectId = "c3b7f625-c07f-4d7d-9be1-ddff8ff93b4d",
                Username = "Steve",
                Password = "steve123",
                Claims = new List<Claim>
                {
                    new Claim("given_name", "Steve"),
                    new Claim("family_name", "Smith"),
                    new Claim("role", "Tour Manager"),
                }
            }
        };
    }
    public static IEnumerable<IdentityResource> IdentityResources =>
        new IdentityResource[]
        {
            new IdentityResources.OpenId(),
            new IdentityResources.Profile(),
            new IdentityResource("roles", new[]{"role"})
        };

    public static IEnumerable<ApiScope> ApiScopes =>
        new ApiScope[]
        {
            new ApiScope("takoha.api"),
            new ApiScope("takohaIdentity.api"),
        };
    
    internal static IEnumerable<ApiResource> GetApiResources()
    {
        return new ApiResource[] {
            new ApiResource("takohaapi", "takoha api", new[] { "role" })
            {
                Scopes = { "takoha.api" }
            }
        };
    }
    public static IEnumerable<Client> Clients =>
        new Client[]
        {
            new Client
            {
                ClientId = "takohaApi.client",
                ClientName = "TakohaAPI",
                AllowedGrantTypes = GrantTypes.ResourceOwnerPassword ,
                ClientSecrets = { new Secret("511536EF-F270-4058-80CA-1C89C192F69A".Sha256()) },
                AllowedScopes = { "takoha.api"}
            },
            new Client
            {
                ClientId = "adminspaclient",
                ClientName = "admin spa client",
                RequireConsent = false,
                AllowedGrantTypes = GrantTypes.HybridAndClientCredentials,
                ClientSecrets = { new Secret("49C1A7E1-0C79-4A89-A3D6-A37998FB86B0".Sha256()) },
                RedirectUris = { "https://localhost:5002/signin-oidc" },
                FrontChannelLogoutUri = "https://localhost:5002/signout-oidc",
                PostLogoutRedirectUris = { "https://localhost:5002/signout-callback-oidc" },
                AllowOfflineAccess = true,
                AllowedScopes = { "openid", "profile", "takoha.api", "takohaIdentity.api" }
            },
        };
}
using System.Linq;
using System.Collections.Generic;
using events_planner.Deserializers;
using events_planner.Models;
using events_planner.Constants.Services;

namespace events_planner.Services
{
    public interface IUserServices
    {
        string GenerateToken(ref User m_user);
        User CreateUser(UserCreationDeserializer userFromRequest);
        void MakeUser(User user);
        string ReadJwtTokenClaims(string bearerToken, JwtSelector extractor = JwtSelector.EMAIL);
        bool IsModeratorFor(int eventId, int userId);
        bool ShouldUpdateFromSSO(User user, YnovSSO ssoData, out List<string> properties);
        void UpdateUserFromSsoDate(User user, YnovSSO ssoData, List<string> properties);

        #region QUERIES

        /// <summary>
        /// Return a query that handle the inclusion of:
        /// - Role
        /// - Promotion
        /// - Jury Point
        /// </summary>
        /// <returns>IQueryable object</returns>
        IQueryable<User> AllForeignKeysQuery();

        /// <summary>
        /// Set a query that handle a like SQL search
        /// </summary>
        /// <param name="query">The query object as Ref</param>
        /// <param name="expression">The expression to looks for</param>
        void likeSearchQuery(ref IQueryable<User> query, string expression);

        /// <summary>
        /// Remove staff memebers of the current list
        /// </summary>
        /// <param name="query">The query object as Ref</param>
        void WithouStaffMembers(ref IQueryable<User> query);

        #endregion
    }
}
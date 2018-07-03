using BETSTS.Contract.Repository.Models;
using BETSTS.Contract.Repository.Models.User;
using Microsoft.EntityFrameworkCore;

namespace BETSTS.Repository
{
    public sealed partial class DbContext
    {
        #region Application

        public DbSet<RefreshTokenEntity> RefreshTokens { get; set; }

        #endregion Application

        #region User

        public DbSet<UserEntity> Users { get; set; }

        public DbSet<UserProfileEntity> UserProfiles { get; set; }

        #endregion User
        public DbSet<AmoutEntity> Amouts { get; set; }
        public DbSet<FootballTeamEntity> FootballTeams { get; set; }
        public DbSet<MatchEntity> Matches { get; set; }
        public DbSet<PriceConfigurationEntity> PriceConfigurations { get; set; }
        public DbSet<RuleConfigurationEntity> RuleConfigurations { get; set; }
        public DbSet<MatchTeamEntity> MatchTeams { get; set; }
        public DbSet<UserBetEntity> UserBets { get; set; }
        public DbSet<ExchangeEntity> Exchange { get; set; }

    }
}
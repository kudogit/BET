#region	License

//--------------------------------------------------
// <License>
//     <Copyright> 2018 © Top Nguyen </Copyright>
//     <Url> http://topnguyen.net/ </Url>
//     <Author> Top </Author>
//     <Project> Elate </Project>
//     <File>
//         <Name> BootstrapperService.cs </Name>
//         <Created> 19/04/2018 6:45:55 PM </Created>
//         <Key> ee899866-2a1d-4f4e-abf2-2bcb5c395eac </Key>
//     </File>
//     <Summary>
//         BootstrapperService.cs is a part of Elate
//     </Summary>
// <License>
//--------------------------------------------------

#endregion License

using System.Collections.Generic;
using BETSTS.Contract.Repository.Interfaces;
using BETSTS.Contract.Repository.Models;
using BETSTS.Contract.Repository.Models.User;
using BETSTS.Contract.Service;
using BETSTS.Core.Utils;
using Elect.DI.Attributes;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BETSTS.Service
{
    [ScopedDependency(ServiceType = typeof(IBootstrapperService))]
    public class BootstrapperService : Base.Service, IBootstrapperService
    {

        private readonly IRepository<UserEntity> _userRepo;

        private readonly IBootstrapper _bootstrapper;
        private readonly IRepository<FootballTeamEntity> _teamRepository;
        private readonly IRepository<PriceConfigurationEntity> _priceRepository;

        public BootstrapperService(IUnitOfWork unitOfWork,
            IBootstrapper bootstrapper
           ) : base(unitOfWork)
        {

            _userRepo = unitOfWork.GetRepository<UserEntity>();
            _teamRepository = unitOfWork.GetRepository<FootballTeamEntity>();
            _priceRepository = unitOfWork.GetRepository<PriceConfigurationEntity>();
            _bootstrapper = bootstrapper;
        }

        public async Task InitialAsync(CancellationToken cancellationToken = default)
        {
            await _bootstrapper.InitialAsync(cancellationToken).ConfigureAwait(true);
            await InitialAccountAsync(cancellationToken).ConfigureAwait(true);
            await InitialFootBallTeam(cancellationToken).ConfigureAwait(true);
        }

        public Task RebuildAsync(CancellationToken cancellationToken = default)
        {
            _bootstrapper.RebuildAsync(cancellationToken).Wait(cancellationToken);

            return Task.CompletedTask;
        }

        private Task InitialAccountAsync(CancellationToken cancellationToken = default)
        {
            var isHaveAnyUser = _userRepo.Get().Any();

            if (isHaveAnyUser)
            {
                return Task.CompletedTask;
            }

            var systemNow = SystemHelper.SystemTimeNow;

            var user1 = new UserEntity
            {
                UserName = "admin",
                Email = "liem.nguyen@saigontechnology.com",
                EmailConfirmedTime = systemNow,
                Phone = "",
                IsAdmin = 1,
                PhoneConfirmedTime = systemNow,
                PasswordHash = AuthenticationService.HashPassword("123456", systemNow),
                PasswordLastUpdatedTime = systemNow,
                Amout = new AmoutEntity { Total = 0,Sent = 0}
            };

            var user2 = new UserEntity
            {
                UserName = "tien",
                Email = "tien.vu@saigontechnology.com",
                EmailConfirmedTime = systemNow,
                Phone = "",
                IsAdmin = 0,
                PhoneConfirmedTime = systemNow,
                PasswordHash = AuthenticationService.HashPassword("123456", systemNow),
                PasswordLastUpdatedTime = systemNow,
                Amout = new AmoutEntity { Total = 0,Sent = 0}
            };

            _userRepo.Add(user1);
            _userRepo.Add(user2);

            cancellationToken.ThrowIfCancellationRequested();

            UnitOfWork.SaveChanges();

            return Task.CompletedTask;
        }
        public Task InitialFootBallTeam(CancellationToken cancellationToken = default)
        {
            var isHaveAnyTeam = _teamRepository.Get().Any();
            if (isHaveAnyTeam)
            {
                return Task.CompletedTask;
            }
            var listFootballTeam = new List<FootballTeamEntity>
            {
                new FootballTeamEntity(){Name = "Russia",Country = "Russia",Coach = "Stanislav Cherchesov"},
                new FootballTeamEntity(){Name = "Uruguay",Country = "Uruguay",Coach = "Oscar Tabárez"},
                new FootballTeamEntity(){Name = "Egypt",Country = "Egypt",Coach = "Héctor Cúper"},
                new FootballTeamEntity(){Name = "Saudi Arabia",Country = "Saudi Arabia",Coach = "Juan Antonio Pizzi"},
                new FootballTeamEntity(){Name = "Iran",Country = "Iran",Coach = "Carlos Queiroz"},
                new FootballTeamEntity(){Name = "Spain",Country = "Spain",Coach = "Fernando Hierro"},
                new FootballTeamEntity(){Name = "Portugal",Country = "Portugal",Coach = "Fernando Santos"},
                new FootballTeamEntity(){Name = "Morocco",Country = "Morocco",Coach = "Hervé Renard"},
                new FootballTeamEntity(){Name = "France",Country = "France",Coach = " Didier Deschamps"},
                new FootballTeamEntity(){Name = "Denmark",Country = "Denmark",Coach = "Åge Hareide"},
                new FootballTeamEntity(){Name = "Australia",Country = "Australia",Coach = "Bert van Marwijk"},
                new FootballTeamEntity(){Name = "Peru",Country = "Peru",Coach = "Ricardo Gareca"},
                new FootballTeamEntity(){Name = "Croatia",Country = "Croatia",Coach = "Zlatko Dalić"},
                new FootballTeamEntity(){Name = "Iceland",Country = "Iceland",Coach = "Heimir Hallgrímsson"},
                new FootballTeamEntity(){Name = "Argentina",Country = "Argentina",Coach = "Jorge Sampaoli"},
                new FootballTeamEntity(){Name = "Nigeria",Country = "Nigeria",Coach = "Gernot Rohr"},
                new FootballTeamEntity(){Name = "Serbia",Country = "Serbia",Coach = "Mladen Krstajić"},
                new FootballTeamEntity(){Name = "Switzerland",Country = "Switzerland",Coach = "Vladimir Petković"},
                new FootballTeamEntity(){Name = "Brazil",Country = "Brazil",Coach = "Tite"},
                new FootballTeamEntity(){Name = "Costa Rica",Country = "Costa Rica",Coach = "Óscar Ramírez"},
                new FootballTeamEntity(){Name = "Mexico",Country = "Mexico",Coach = "Juan Carlos Osorio"},
                new FootballTeamEntity(){Name = "Sweden",Country = "Sweden",Coach = "Janne Andersson"},
                new FootballTeamEntity(){Name = "Korea",Country = "Korea",Coach = "Shin Tae-yong"},
                new FootballTeamEntity(){Name = "Germany",Country = "Germany",Coach = "Joachim Löw"},
                new FootballTeamEntity(){Name = "Belgium ",Country = "Belgium ",Coach = "Roberto Martínez"},
                new FootballTeamEntity(){Name = "England",Country = "England",Coach = "Gareth Southgate"},
                new FootballTeamEntity(){Name = "Tunisia",Country = "Tunisia",Coach = "Nabil Maâloul"},
                new FootballTeamEntity(){Name = "Panama",Country = "Panama",Coach = "Hernán Darío Gómez"},
                new FootballTeamEntity(){Name = "Poland",Country = "Poland",Coach = "Adam Nawałka"},
                new FootballTeamEntity(){Name = "Senegal",Country = "Senegal",Coach = "Aliou Cissé"},
                new FootballTeamEntity(){Name = "Colombia",Country = "Colombia",Coach = "José Pékerman"},
                new FootballTeamEntity(){Name = "Japan",Country = "Japan",Coach = "Akira Nishino"},
            };
            foreach (var team in listFootballTeam)
            {
                _teamRepository.Add(team);
            }
            cancellationToken.ThrowIfCancellationRequested();
            UnitOfWork.SaveChanges();
            return Task.CompletedTask;
        }

        public Task InitialPriceConfig(CancellationToken cancellationToken = default)
        {
            var isHaveAnyPrice = _priceRepository.Get().Any();
            if (isHaveAnyPrice)
            {
                return Task.CompletedTask;
            }
            var listPrices = new List<PriceConfigurationEntity>()
            {
                new PriceConfigurationEntity(){Price = 10000},
                new PriceConfigurationEntity(){Price = 20000},
                new PriceConfigurationEntity(){Price = 30000},
                new PriceConfigurationEntity(){Price = 40000},
                new PriceConfigurationEntity(){Price = 50000}
            };
            return Task.CompletedTask;
        }

    }
}
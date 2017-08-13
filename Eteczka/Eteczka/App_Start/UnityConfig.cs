using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Practices.Unity;
using Eteczka.BE.Services;
using Eteczka.DB.DAO;
using System.Configuration;
using Eteczka.DB.Connection;
using Eteczka.BE.Utils;
using System.IO;
using Eteczka.DB.Mappers;

namespace Eteczka
{
    public class UnityConfig
    {
        #region Unity Container
        private static Lazy<IUnityContainer> container = new Lazy<IUnityContainer>(() =>
        {
            var container = new UnityContainer();
            return container;
        });

        /// <summary>
        /// Gets the configured Unity container.
        /// </summary>
        public static IUnityContainer GetConfiguredContainer()
        {
            return container.Value;
        }
        #endregion

        /// <summary>Registers the type mappings with the Unity container.</summary>
        /// <param name="container">The unity container to configure.</param>
        /// <remarks>There is no need to register concrete types such as controllers or API controllers (unless you want to 
        /// change the defaults), as Unity allows resolving a concrete type even if it was not previously registered.</remarks>
        public static void RegisterTypes(IUnityContainer container)
        {
            //container.RegisterType<IMessageDecoderExtensionsPoint, MessageDecoderExtensionsPoint>();
            container.RegisterType<IUsersService, UsersService>();
            container.RegisterType<IDbConnectionFactory, DbConnectionFactory>();
            container.RegisterType<IPlikiService, PlikiService>();
            container.RegisterType<IImportService, ImportService>();
            container.RegisterType<IPracownicyService, PracownicyService>();
            container.RegisterType<IStatystykiService, StatystykiService>();
            container.RegisterType<IMapowalnyDoPracownikDto, PracownikDtoMapper>();
            container.RegisterType<IMapowalnyDoPracownikDto, PracownikDtoMapper>();
            container.RegisterType<IConnection, Connection>();
            container.RegisterType<IEmailService, Eteczka.BE.Services.EmailService>();
            container.RegisterType<IPlikiMapper, PlikiMapper>();

            container.RegisterType<PlikiDAO, PlikiDAO>();
            container.RegisterType<UserDAO, UserDAO>();
            container.RegisterType<PracownikDAO, PracownikDAO>();
            container.RegisterType<KatLoginDAO, KatLoginDAO>();
            container.RegisterType<PlikiUtils, PlikiUtils>();
        }


        public static void RegisterStaticTypes(IUnityContainer container)
        {
            string user = ConfigurationManager.AppSettings["dbuser"];
            string password = ConfigurationManager.AppSettings["dbpassword"];

            string host = ConfigurationManager.AppSettings["dbhost"];
            string port = ConfigurationManager.AppSettings["dbport"];
            string name = ConfigurationManager.AppSettings["dbname"];

            IConnectionDetails connectionDetails = new ConnectionDetails(user, password, host, port, name);

            container.RegisterInstance<IConnectionDetails>(connectionDetails, new ContainerControlledLifetimeManager());
        }

        private static void LogStartup(bool success)
        {
            string eadRootName = ConfigurationManager.AppSettings["rootdir"];
            string configurationPath = Path.Combine(Environment.GetEnvironmentVariable(eadRootName), "logs", "eteczka.di.txt");
            string prefix = success ? "" : "NOT";

            if (File.Exists(configurationPath))
            {
                using (var tw = new StreamWriter(configurationPath, true))
                {
                    tw.WriteLine(prefix + "Built all dynamic instances: " + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString());
                    tw.Close();
                }
            }
            else
            {
                File.Create(configurationPath);
                using (var tw = new StreamWriter(configurationPath, true))
                {
                    tw.WriteLine(prefix + "Built all dynamic instances " + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString());
                    tw.Close();
                }
            }
        }
    }
}
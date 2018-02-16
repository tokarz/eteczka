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
using Eteczka.BE.Mappers;

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
            container.RegisterType<IKatLoginyService, KatLoginyService>();
            container.RegisterType<IDbConnectionFactory, DbConnectionFactory>();
            container.RegisterType<IPlikiService, PlikiService>();
            container.RegisterType<IImportService, ImportService>();
            container.RegisterType<IImportStateService, ImportStateService>();
            container.RegisterType<IPracownicyService, PracownicyService>();
            container.RegisterType<IStatystykiService, StatystykiService>();
            container.RegisterType<IMiejscePracyService, MiejscePracyService>();
            container.RegisterType<IEmailService, Eteczka.BE.Services.EmailService>();
            container.RegisterType<IFirmyService, FirmyService>();
            container.RegisterType<IKatWydzialMapper, KatWydzialMapper>();
            container.RegisterType<IRejonMapper, RejonMapper>();
            container.RegisterType<IRejonyService, RejonyService>();
            container.RegisterType<IWydzialyService, WydzialyService>();
            container.RegisterType<IPodWydzialService, PodWydzialService>();
            container.RegisterType<IKonto5Service, Konto5Service>();
            container.RegisterType<IKoszykService, KoszykService>();
            container.RegisterType<IKatDokumentyRodzajService, KatDokumentyRodzajService>();
            container.RegisterType<IRaportyPdfService, RaportyPdfService>();
            container.RegisterType<IRaportyExcellService, RaportyExcellService>();
            container.RegisterType<IExcellUtils, ExcellUtils>();
            


            container.RegisterType<IConnection, Connection>();
            container.RegisterType<IPlikiMapper, PlikiMapper>();
            container.RegisterType<IPracownikMapper, PracownikMapper>();
            container.RegisterType<IFirmyMapper, FirmyMapper>();
            container.RegisterType<IKatLoginyMapper, KatLoginyMapper>();
            container.RegisterType<IUprawnieniaMapper, UprawnieniaMapper>();
            container.RegisterType<IKatPodWydzialMapper, KatPodWydzialMapper>();
            container.RegisterType<IKatRodzajeDokumentowMapper, KatRodzajeDokumentowMapper>();

            container.RegisterType<IJsonToKatFirmyMapper, JsonToKatFirmyMapper>();
            container.RegisterType<IJsonToKatLokalMapper, JsonToKatLokalMapper>();
            container.RegisterType<IJsonToKatRejonyMapper, JsonToKatRejonyMapper>();
            container.RegisterType<IJsonToPracownikMapper, JsonToPracownikMapper>();
            container.RegisterType<IJsonToPracownikMapper, JsonToPracownikMapper>();
            container.RegisterType<IJsonToMiejscePracyMapper, JsonToMiejscePracyMapper>();
            container.RegisterType<IJsonToPodwydzialMapper, JsonToPodwydzialMapper>();
            container.RegisterType<IJsonToWydzialMapper, JsonToWydzialMapper>();
            container.RegisterType<IJsonToKonto5Mapper, JsonToKonto5Mapper>();
            container.RegisterType<IJsonToKatDokumentyRodzajMapper, JsonToKatDokumentyRodzajMapper>();

            container.RegisterType<IKatRodzajeDokumentowExcelMapper, KatRodzajeDokumentowExcelMapper>();
            container.RegisterType<IPodWydzialDtoMapper, PodWydzialDtoMapper>();
            container.RegisterType<IKatKonto5Mapper, KatKonto5Mapper>();

            container.RegisterType<IPracownikDAO, PracownikDAO>();
            container.RegisterType<IKoszykDAO, KoszykDAO>();
            container.RegisterType<IFirmyDAO, FirmyDAO>();
            container.RegisterType<IRejonyDAO, RejonyDAO>();

            container.RegisterType<PlikiDAO, PlikiDAO>();
            container.RegisterType<UserDAO, UserDAO>();
            container.RegisterType<KatLoginDAO, KatLoginDAO>();
            container.RegisterType<FirmyDAO, FirmyDAO>();
            container.RegisterType<MiejscePracyDAO, MiejscePracyDAO>();
            container.RegisterType<KatPodwydzialDAO, KatPodwydzialDAO>();
            container.RegisterType<KatWydzialDAO, KatWydzialDAO>();
            container.RegisterType<Konto5DAO, Konto5DAO>();
            container.RegisterType<FirmyDAO, FirmyDAO>();
            container.RegisterType<KatDokumentyRodzajDAO, KatDokumentyRodzajDAO>();
            container.RegisterType<Konto5DAO, Konto5DAO>();
            container.RegisterType<KatDokumentyRodzajDAO, KatDokumentyRodzajDAO>();

            container.RegisterType<PlikiUtils, PlikiUtils>();
            container.RegisterType<KatPodwydzialDAO, KatPodwydzialDAO>();
            container.RegisterType<IDirectoryWrapper, DirectoryWrapper>();
            container.RegisterType<IPdfUtils, PdfUtils>();
            container.RegisterType<IZipUtils, ZipUtils>();

        }

        public static void RegisterStaticTypes(IUnityContainer container)
        {
            string user = ConfigurationManager.AppSettings["dbuser"];
            string password = ConfigurationManager.AppSettings["dbpassword"];

            //TODO: logowanie admina - wstrzyknąć te dane!
            string admin = ConfigurationManager.AppSettings["dbadminuser"];
            string adminpassword = ConfigurationManager.AppSettings["dbpassword"];

            string host = ConfigurationManager.AppSettings["dbhost"];
            string port = ConfigurationManager.AppSettings["dbport"];
            string name = ConfigurationManager.AppSettings["dbname"];

            IConnectionDetails connectionDetails = new ConnectionDetails(user, password, host, port, name);
            IConnectionDetails adminDetails = new ConnectionDetails(user, password, host, port, name);

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
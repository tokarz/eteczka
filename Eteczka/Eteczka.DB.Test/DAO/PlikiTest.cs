
using Eteczka.DB.Connection;
using Eteczka.DB.DAO;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Eteczka.DB.Tests.DAO
{
    [TestFixture]
    public class PlikiTest
    {
        private PlikiDAO _Sut;
        private IDbConnectionFactory _ConnectionFactory;
        private IConnectionState _ConnectionState;
        private IConnection _Connection;

        [SetUp]

        public void Setup()
        {
            
        }
    }
}
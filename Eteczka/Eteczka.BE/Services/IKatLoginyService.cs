﻿using Eteczka.BE.DTO;
using Eteczka.Model.Entities;
using System.Collections.Generic;

namespace Eteczka.BE.Services
{
    public interface IKatLoginyService
    {
        KatLoginy GetUserByNameAndPassword(string name, string password);
        List<KatLoginyDetale> GetUserDetails(string identyfikator);
        List<KatLoginyDetale> GetAllUsersDetails();
        bool UsunFirmeUzytkownika(KatLoginy user, string firma);
    }
}

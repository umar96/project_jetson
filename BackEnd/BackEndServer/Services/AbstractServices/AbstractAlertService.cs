﻿using System.Collections.Generic;
using BackEndServer.Models.DBModels;
using BackEndServer.Models.ViewModels;

namespace BackEndServer.Services.AbstractServices
{
    public interface AbstractAlertService
    {
        bool SaveAlert(AlertDetails alertDetails);
        List<AlertDetails> GetAllAlertsForUser(int userId);
        SortedDictionary<int, List<AlertDetails>> GetAllAlertsByCameraIdForUser(int userId);
        bool DeleteAlert(int alertId);
        bool DisableAlert(AlertDisablingInformation alertDisablingInformation);
    }
}
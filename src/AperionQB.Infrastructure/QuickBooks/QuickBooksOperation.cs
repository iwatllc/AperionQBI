﻿using System;
using System.Collections.ObjectModel;
using AperionQB.Application.Interfaces;
using AperionQB.Domain.Entities.QuickBooks;
using Intuit.Ipp.Core;
using Intuit.Ipp.Data;
using Intuit.Ipp.QueryFilter;
using Intuit.Ipp.Security;
using Newtonsoft.Json;

namespace AperionQB.Infrastructure.QuickBooks
{
	public abstract class QuickBooksOperation
	{

        public OAuth2RequestValidator? validator { get;  }
        public ServiceContext serviceContext { get;  }
        public readonly IApplicationDbContext _context;
        public readonly IInfoHandler _handler;

        public QuickBooksOperation(IApplicationDbContext _context, IInfoHandler _handler)
	    {
            this._context = _context;
            IntuitInfo info = _handler.getIntuitInfo();
            if(info.AccessToken == null || info.RefreshToken == null)
            {
                info.AccessToken = "adsf";
                info.RefreshToken = "asdf";
                
            }
            validator = new OAuth2RequestValidator((string)info.AccessToken);
            serviceContext = new ServiceContext((string)info.RealmId, IntuitServicesType.QBO, validator);

            serviceContext.IppConfiguration.BaseUrl.Qbo = "https://quickbooks.api.intuit.com/";
            serviceContext.IppConfiguration.MinorVersion.Qbo = "23";
        }
	}
}


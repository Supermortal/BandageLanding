﻿using System;
using BandageLanding.Infrastructure.Abstract;
using BandageLanding.Infrastructure.Models;
using Supermortal.Common.PCL.Helpers;
using Supermortal.Common.Web.Helpers.Log;

namespace BandageLanding.Infrastructure.Concrete
{
    public class EFSessionService : ISessionService
    {

        private static readonly log4net.ILog Log = LogHelper.GetLogger
            (typeof(EFSessionService));

        private const int ExpirationMinutes = 240;

        private readonly ISessionRepository _sr;
        private readonly IUserRepository _ur;

        public EFSessionService(ISessionRepository sr, IUserRepository ur)
        {
            _sr = sr;
            _ur = ur;
        }

        public Session CreateSession(string userName)
        {
            try
            {

                var user = _ur.FindByUserName(userName);
                if (user == null)
                    return null;

                var session = new Session();
                session.Token = GenerateToken(user);
                session.Expiration = DateTime.UtcNow.AddMinutes(ExpirationMinutes);
                session.UserName = userName;

                _sr.Delete(_sr.FindByUserName(userName));
                _sr.Insert(session);
                //user.Session = session;
                //_ur.Update(user);

                return session;
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, ex);
                return null;
            }
        }

        public ApplicationUser GetUser(string token)
        {
            try
            {
                var session = _sr.Find(token);
                return session == null ? null : _ur.FindByUserName(session.UserName);
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, ex);
                return null;
            }
        }

        public bool? ValidateToken(string token)
        {
            try
            {
                var session = _sr.Find(token);
                if (session == null)
                    return false;

                return !session.IsExpired;
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, ex);
                return null;
            }
        }

        private static string GenerateToken(ApplicationUser user)
        {
            try
            {
                return Crypto.Hash(user.UserName + "#" + user.Email + "kd#009B@SSB" + DateTime.Now.ToString());
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, ex);
                return null;
            }
        }

    }
}
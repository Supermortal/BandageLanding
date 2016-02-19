using System;
using System.Collections.Generic;
using System.Web.Mvc;
using BandageLanding.Infrastructure.Abstract.Survey;
using BandageLanding.Infrastructure.Models.Survey;
using BandageLanding.Infrastructure.ViewModels;
using Supermortal.Common.Web.Helpers;
using Supermortal.Common.Web.Helpers.Log;

namespace BandageLanding.Controllers
{
    [Authorize]
    public class SurveyController : Controller
    {

        private static readonly log4net.ILog Log = LogHelper.GetLogger
            (typeof(SurveyController));

        private ISurveyService db;

        public SurveyController(ISurveyService ss)
        {
            db = ss;
        }

        //
        // GET: /Survey/

        public ActionResult Index()
        {
            try
            {
                return View(db.GetAllSurveys());
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, ex);
                return HttpNotFound();
            }
        }

        public ActionResult Create()
        {
            try
            {
                return View();
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, ex);
                return HttpNotFound();
            }
        }

        [HttpPost]
        public JsonResult Create(string surveyName, SurveyParameter[] surveyParameters)
        {
            try
            {
                db.CreateSurvey(surveyName, surveyParameters);
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, ex);
                return new JsonResult() { Data = new { Status = "ERROR", Message = ex.Message } };
            }

            return new JsonResult() { Data = new { Status = "OK" } };
        }

        public ActionResult Delete(int id)
        {
            try
            {
                db.DeleteSurvey(id);

                if (Request.UrlReferrer != null)
                    return Redirect(Request.UrlReferrer.AbsoluteUri);
                else
                    return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, ex);
                return HttpNotFound();
            }
        }

        public ActionResult Edit(long id = 0)
        {
            try
            {
                return View(id);
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, ex);
                return HttpNotFound();
            }
        }

        [HttpPost]
        public JsonResult GetSurveyForEdit(int id)
        {
            try
            {
                var survey = db.FindSurvey(id);  
                db.SortSurveyParameters(ref survey);
                return new JsonResult() { Data = new { Status = "OK", Data = new SurveyModelViewModel(survey) } };
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, ex);
                return new JsonResult() { Data = new { Status = "ERROR", Message = "An error has occured." } };
            }
        }

        [HttpPost]
        public JsonResult Edit(int id, string surveyName, List<SurveyParameter> surveyParameters)
        {
            try
            {
                db.EditSurvey(id, surveyName, surveyParameters);
                return new JsonResult() { Data = new { Status = "OK" } };
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, ex);
                return new JsonResult() { Data = new { Status = "Error", Message = ex.Message } };
            }

            return new JsonResult() { Data = new { Status = "Ok" } };
        }

        public ActionResult TakeASurvey()
        {
            try
            {
                var surveys = db.GetActiveSurveys();

                if (Request.UrlReferrer != null)
                    ViewBag.ReferrerUrl = Request.UrlReferrer.AbsoluteUri;

                return View(surveys);
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, ex);
                return HttpNotFound();
            }
        }

        [AllowAnonymous]
        public ActionResult Take(int id)
        {
            try
            {
                var survey = db.FindSurvey(id);

                if (!survey.Active)
                    return HttpNotFound();

                db.SortSurveyParameters(ref survey);

                ViewBag.ReferrerUrl = Request.UrlReferrer != null ? Request.UrlReferrer.AbsoluteUri : Url.Action("Index", "Home");

                return View(survey);
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, ex);
                return HttpNotFound();
            }
        }

        [HttpPost]
        [AllowAnonymous]
        public JsonResult Take(SurveyKeyValue[] keyValues, long id)
        {
            try
            {
                //TODO Allow Administrators to set a user id manually

                db.CreateSurveyInstance(keyValues, id, SessionHelper.Instance.IPAddress);

                return new JsonResult() { Data = new { Result = "OK" } };
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, ex);
                return new JsonResult() { Data = new { Result = "OK", Message = ex.Message } };
            }
        }

        public ActionResult Review(long id)
        {
            try
            {
                var userIds = db.GetDistinctUserIdsFromInstances(id);
                var userNames = new Dictionary<int, string>();
                //foreach (var userId in userIds)
                //{
                //    var user = context.UserProfiles.Find(userId);

                //    if (user != null)
                //        userNames.Add(userId, user.UserName);
                //}

                var surveyVM = db.CreateSurveyReviewViewModel(id, userNames);

                return View(surveyVM);
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, ex);
                return HttpNotFound();
            }
        }

        public ActionResult ReviewInstance(long surveyInstanceId, string userName, string surveyName, long surveyModelId)
        {
            try
            {
                var viewModel = db.CreateSurveyInstanceReviewViewModel(surveyInstanceId, userName, surveyName,
                                                                       surveyModelId);

                return View(viewModel);
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, ex);
                return HttpNotFound();
            }
        }

        public ActionResult ReviewAllInstances(long id)
        {
            try
            {
                var userIds = db.GetDistinctUserIdsFromInstances(id);
                var userNames = new Dictionary<int, string>();
                //foreach (var userId in userIds)
                //{
                //    var user = context.UserProfiles.Find(userId);

                //    if (user != null)
                //        userNames.Add(userId, user.UserName);
                //}

                var surveyVM = db.CreateSurveyReviewViewModel(id, userNames);

                return View(surveyVM);
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, ex);
                return HttpNotFound();
            }
        }

        public ActionResult Activate(long id)
        {
            try
            {
                db.ActivateSurvey(!db.FindSurvey(id).Active, id);

                return Redirect(Request.UrlReferrer.AbsoluteUri);
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, ex);
                return HttpNotFound();
            }
        }

        public ActionResult DeleteInstance(long id)
        {
            try
            {
                db.DeleteSurveyInstance(id);

                return Redirect(Request.UrlReferrer.AbsoluteUri);
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, ex);
                return HttpNotFound();
            }
        }

    }
}

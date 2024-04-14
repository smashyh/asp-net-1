using Infrastructure.Models.APIModels;
using Infrastructure.Enums;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Net;
using WebApp.Models;
using WebApp.Helpers;

namespace WebApp.Controllers;

public class NewsletterController(ApiCommunicator apiCommunicator) : Controller
{
    private readonly ApiCommunicator _apiCommunicator = apiCommunicator;

    [HttpPost]
    public async Task<IActionResult> Subscribe(NewsSubscriberViewModel viewModel)
    {
        if (ModelState.IsValid)
        {
            try
            {
                // just in case something goes wrong in this scope
                TempData["StatusMessage"] = "Something went wrong. Please try again later.";
                TempData["Successful"] = false;

                var subscriberModel = new NewsSubscriberModel();
                subscriberModel.Email = viewModel.Email;
                if (viewModel.DailyNewsletter) subscriberModel.Subscriptions    |= NewsletterSubscriptions.DailyNewsletter;
                if (viewModel.AdvertisingUpdates) subscriberModel.Subscriptions |= NewsletterSubscriptions.AdvertisingUpdates;
                if (viewModel.WeekInReview) subscriberModel.Subscriptions       |= NewsletterSubscriptions.WeekInReview;
                if (viewModel.EventUpdates) subscriberModel.Subscriptions       |= NewsletterSubscriptions.EventUpdates;
                if (viewModel.StartupsWeekly) subscriberModel.Subscriptions     |= NewsletterSubscriptions.StartupsWeekly;
                if (viewModel.Podcasts) subscriberModel.Subscriptions           |= NewsletterSubscriptions.Podcasts;

                if (subscriberModel.Subscriptions != NewsletterSubscriptions.Nothing)
                {
                    var response = await _apiCommunicator.PostAsync("/Newsletter/Subscribe", subscriberModel);

                    TempData["Successful"] = response.IsSuccessStatusCode;

                    if (response.IsSuccessStatusCode)
                    {
                        TempData["StatusMessage"] = "You are now subscribed to our newsletter!";

                    }
                    else
                    {
                        TempData["StatusMessage"] = response.StatusCode switch
                        {
                            HttpStatusCode.Conflict => "The provided e-mail address is already subscribed.",
                            HttpStatusCode.BadRequest => "The subscription form could not be validated. Please try again.",
                            _ => "Something went wrong. Please try again later.",
                        };
                    }
                }
                else
                {
                    TempData["StatusMessage"] = "You must check at least one checkbox to proceed.";
                }

                return RedirectToAction("Home", "Default", "newsletter");
            }
            catch (Exception ex) { Debug.WriteLine(ex.Message); }
        }
        else
        {
            // The only required field in this viewmodel is email. We have a separate
            // validation for the checkboxes.
            TempData["StatusMessage"] = $"Please enter a valid e-mail address.";
        }

        return RedirectToAction("Home", "Default", "newsletter");
    }
}
